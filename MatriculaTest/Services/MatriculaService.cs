using Google.Protobuf;
using Grpc.Core;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriculaTest.Services
{
    public class MatriculaService : Matriculas.MatriculasBase
    {
        public override async Task GetMatricula(Camera request, IServerStreamWriter<DadosMatricula> responseStream, ServerCallContext context)
        {
            //if (true dados validos)
            if (request.CameraEntrada)
            {
                // Retira dados pela camera de entrada
                //return Task.FromResult(new DadosMatricula { Matricula = "AA-00-00", Foto = null });
//                await responseStream.WriteAsync(new DadosMatricula { Matricula = "AA-00-00", Foto = null });
            }
            else
            {
                // Retira dados pela camera de saida
                //return Task.FromResult(new DadosMatricula { Matricula = "00-00-AA", Foto = null });
//                await responseStream.WriteAsync(new DadosMatricula { Matricula = "00-00-AA", Foto = null });
            }



            const int ChunkSize = 1024 * 32; // 32 KB

            //Sending file
            await responseStream.WriteAsync(new DadosMatricula
            {
                Matricula = "AA-00-AA"
            });

            var buffer = new byte[ChunkSize];
            await using var readStream = File.OpenRead(@"\Fotos\foto1.jpg");

            while (true)
            {
                var count = await readStream.ReadAsync(buffer);
                if (count == 0)
                {
                    break;
                }

                Console.WriteLine("Sending file data chunk of length " + count);
                await responseStream.WriteAsync(new DadosMatricula
                {
                    Foto = UnsafeByteOperations.UnsafeWrap(buffer.AsMemory(0, count))
                });
            }

            //Complete request
            //await responseStream.CompleteAsync();
        }
    }
}
