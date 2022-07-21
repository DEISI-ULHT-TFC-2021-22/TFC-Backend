using Grpc.Core;
using Grpc.Net.Client;
using Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Services
{
    public class MobileService : Mobile.MobileBase
    {
        public override async Task<Resposta> AbrirCancela(Pedido pedido, ServerCallContext context)
        {
            GrpcChannel channelWeb = GrpcChannel.ForAddress(@"https://localhost:7203");
            string msgMobile = "";
            string msgWeb = "";
            Boolean podeEntrar = false;

            // Chamar serviço de disponibilidade
            var webService = new Web.WebClient(channelWeb);
            var disponibilidade = new ExisteDisponibilidade { DisponivelNovaEntrada = pedido.CancelaEntrada };
            var respostaDisponibilidade = webService.GetDisponibilidade(disponibilidade);

            // Verificar disponibilidade do serviço (ver se não está a processar outro pedido)
            if (!respostaDisponibilidade.Disponivel)
            {
                msgMobile = "Existe outro pedido em processamento. Tente novamente quando for o 1º da fila. Obrigado ";
            }
            else
            {
                // Se for para Entrar
                if (pedido.CancelaEntrada)
                {
                    // verifica se o parque está pago
                    if (!(Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spPermiteEntrada_EntradaValida '" + pedido.Login + "'"))))
                    {
                        msgMobile = "O estacionamento encontra-se para pagamento! Por favor regularize a situação para poder estacionar! ";
                        msgWeb = "O pagamento do estacionamento está por regularizar! ";
                    }

                    // verifica se não está a tentar entrar uma 2ª vez. Se já existe uma viatura dentro do parque associada ao seu utilizador!
                    if (Boolean.Parse(SQLAccess.ExecSQLReturnData(" exec spPermiteEntrada_JaEstaNoParque '" + pedido.Login + "'")))
                    {
                        msgMobile = "Já existe uma viatura dentro do parque associada ao seu utilizador! ";
                        msgWeb = "Já existe uma viatura dentro do parque associada a este utilizador! ";
                    }
                }

                // Pede matricula
                GrpcChannel channelMatriculas = GrpcChannel.ForAddress(@"http://localhost:7065");
                var matriculasService = new Matriculas.MatriculasClient(channelMatriculas);
                var getMatricula = new Camera { CameraEntrada = pedido.CancelaEntrada };
                var dadosMatricula = new DadosMatricula();

                var tempFile = @"\\localhost\Fotos\temp.tmp";
                var finalFile = tempFile;
                Boolean notDone = true;

                using (var matricula = matriculasService.GetMatricula(getMatricula))
                {
                    await using (var fileStream = File.OpenWrite(tempFile))
                    {
                        await foreach (var dados in matricula.ResponseStream.ReadAllAsync().ConfigureAwait(false))
                        {
                            if (!String.IsNullOrEmpty(dados.Matricula) && notDone)
                            {
                                finalFile = @"\\localhost\Fotos\" + dados.Matricula + $"_{DateTime.UtcNow.ToString("yyyyMMdd_HHmmss")}.jpg";
                                dadosMatricula = dados;
                                notDone = false;
                            }

                            if (dados.Foto.Length == dados.BlockSize)
                            {
                                fileStream.Write(dados.Foto.ToByteArray());
                            }
                            else
                            {
                                fileStream.Write(dados.Foto.ToByteArray(), 0, dados.BlockSize);
                            }
                        }
                    }
                }

                if (finalFile != tempFile)
                {
                    File.Move(tempFile, finalFile);
                }

                string fotoLocation = Path.GetFileName(finalFile);

                // verifica a matricula da viatura.
                if (Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spMatriculas_MatriculaValida '" + pedido.Login + "', '" + dadosMatricula.Matricula + "'")))
                {
                    // Se for para Entrar
                    if (pedido.CancelaEntrada)
                    {
                        msgMobile = "Acesso autorizado! Benvindo! ";
                        msgWeb = "Acesso autorizado! ";
                    }
                    else
                    {
                        msgMobile = "Boa viagem! ";
                        msgWeb = "Saida Autorizada! ";
                    }
                    podeEntrar = true;
                }
                else
                {
                    msgMobile = "Esta viatura não está associada ao seu utilizador! Fale com o segurança! ";
                    msgWeb = "Esta viatura não está associada a este utilizador! ";
                }

                // Enviar os dados para a plataforma web
                var dadosPorAutorizar = new Dados { CameraEntrada = pedido.CancelaEntrada, Matricula = dadosMatricula.Matricula, FotoLocation = fotoLocation, User = pedido.Login, MsgValidacao = msgWeb, PodeEntrar = podeEntrar };
                webService.SendCancelaRequest(dadosPorAutorizar);
            }
            return await Task.FromResult(new Resposta { Mensagem = msgMobile });
        }
    }
}
