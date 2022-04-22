using Grpc.Core;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTest.Services
{
    public class MatriculaService : Matriculas.MatriculasBase
    {
        public override Task<DadosRecebidos> SetMatricula(Dados request, ServerCallContext context)
        {
            //if (true dados validos)
            if (request.CameraEntrada)
            {
                // Recebe Dados
                bool cameraEntrada = request.CameraEntrada;
                string matricula = request.Matricula;
                string fotoLocation = request.FotoLocation;
                string user = request.User;
                Console.WriteLine(cameraEntrada);
                Console.WriteLine(matricula);
                Console.WriteLine(fotoLocation);
                Console.WriteLine(user);

                return Task.FromResult(new DadosRecebidos { Recebidos = true });
            }
            return Task.FromResult(new DadosRecebidos { Recebidos = false });
        }
    }
}
