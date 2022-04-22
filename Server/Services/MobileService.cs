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
            GrpcChannel channel = GrpcChannel.ForAddress(@"https://localhost:7064");
            string mensagem = "";

            // Chamar serviço de disponibilidade
            var webService = new Matriculas.MatriculasClient(channel);
            var pedido1 = new ExisteDisponibilidade { DisponivelNovaEntrada = pedido.CancelaEntrada };
            var respostaDisponibilidade = webService.GetDisponibilidade(pedido1);

            // Verificar disponibilidade do serviço (ver se não está a processar outro pedido)
            if (!respostaDisponibilidade.Disponivel)
            {
                mensagem = "Existe outro pedido em processamento. Tente novamente quando for o 1º da fila. Obrigado";
            }
            else
            {
                // Se for para Entrar
                if (pedido.CancelaEntrada)
                {

                    // TODO
                    // verifica se o parque está pago

                    if (false/* parque por pagar */)
                    {
                        mensagem = "O estacionamento encontra-se para pagamento! Por favor regularize a situação para poder estacionar!";
                    }

                    // TODO
                    // verifica se não está a tentar entrar uma 2ª vez

                    if (true /* Já existe uma viatura dentro do parque associada ao seu utilizador! */)
                    {
                        mensagem = "Já existe uma viatura dentro do parque associada ao seu utilizador!";
                    }
                }

                //




                // Se for para Entrar
                if (pedido.CancelaEntrada)
                {
                    // TODO
                    // verifica a matricula da viatura.
                    if (true /* matricula valida */)
                    //if (reply.Matricula.Equals("AA-00-00"))
                    {
                        mensagem = "Acesso autorizado! Benvindo!";
                    }
                    else
                    {
                        mensagem = "Esta viatura não está associada ao seu utilizador! Fale com o segurança!";
                    }
                }
                else
                {
                    // TODO
                    // verifica a matricula da viatura.
                    if (true /* matricula valida */)
                    {
                        mensagem = "Boa viagem!";
                    }
                    else
                    {
                        mensagem = "Esta viatura não está associada ao seu utilizador! Fale com o segurança!";
                    }
                }
            }

            return await Task.FromResult(new Resposta { Mensagem = "Já existe uma viatura dentro do parque associada ao seu utilizador!" });
        }

        public override async Task<EnterParkReply> EnterParkRequest(EnterPark request, ServerCallContext context)
        {
            // TODO
            // 1º Verificar disponibilidade do serviço (ver se não está a processar outro pedido)


            // TODO
            // verifica se o parque está pago
            if (false/* parque por pagar */)
            {
                return await Task.FromResult(new EnterParkReply { Message = "O estacionamento encontra-se para pagamento! Por favor regularize a situação para poder estacionar!" });
            }

            var channel = GrpcChannel.ForAddress(@"https://localhost:7158");
            var client = new Matriculas.MatriculasClient(channel);
            var input = new Camera{ CameraEntrada = true };
            //var reply = await Task.FromResult(client.GetMatricula(input));
            //            DadosMatricula dados = new DadosMatricula(reply);
            //            Console.WriteLine(reply.Matricula);

            string matricula = "";
/*
            //Get dados da matricula do stream
            string path = @"\Fotos\imagem1.jpg";
            await using var writeStream = File.Create(path);

            using (var call = client.GetMatricula(input))//.ResponseStream.ReadAllAsync());
            {
                if (await call.ResponseStream.ReadAllAsync != null)
                {
                    await File.WriteAllTextAsync(path, call.Metadata.ToString());
                }
                if (message.Data != null)
                {
                    await writeStream.WriteAsync(message.Data.Memory);
                }
            }
*/
            /*
            //Get dados da matricula do stream
            using (var call = client.GetMatricula(input))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var reply = call.ResponseStream.Current;
                    matricula = reply.Matricula;
                }
            }
            */

            var channel1 = GrpcChannel.ForAddress(@"https://localhost:7064");
            var client1 = new Matriculas.MatriculasClient(channel1);
            var input1 = new Dados { CameraEntrada = true, FotoLocation = "Não sei onde está!", Matricula = matricula, User = "userXPTO" };

            var reply1 = await Task.FromResult(client1.SetMatricula(input1));
            Console.WriteLine(reply1.Recebidos);


            //reply = await client.EnterParkRequestAsync(input);

            //            dados.Matricula = reply.matricula;
            //            dados.Foto = reply.foto;

            // TODO
            // verifica se não está a tentar entrar uma 2ª vez
            if (true /* Já existe uma viatura dentro do parque associada ao seu utilizador! */)
            {
                return await Task.FromResult(new EnterParkReply { Message = "Já existe uma viatura dentro do parque associada ao seu utilizador!" });
            }
        }
    }
}