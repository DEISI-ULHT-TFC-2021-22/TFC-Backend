using Grpc.Core;
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
    public class LoginService : Login.LoginBase
    {
        public override Task<LoginAutorization> LoginRequest(LoginData pedido, ServerCallContext context)
        {
            int tipoUser = 0;

            // TODO
            // Validar os dados de login na BD
            Boolean DBResult = true;  // chamar storage procedure validar login com params (request.Login.Equals("abc") && request.Password.Equals("123"))

            Console.WriteLine("Recebido pedido de autenticação do utilizador " + pedido.Login);

            // para testar antes de ligar à BD
            DBResult = pedido.Login == "xpto" && pedido.Password == "123";

            if (DBResult)
            {
                Console.WriteLine("Autenticação do utilizador bem sucedida!");
            }
            else
            {
                Console.WriteLine("Autenticação invalida!");
            }

            // TODO
            // Atualizar o tipo de utilizador

            return Task.FromResult(new LoginAutorization { LoginEfetuado = DBResult, TipoUser = tipoUser });
        }

        public override Task<PasswordChanged> ChangePassword(NewLoginData pedido, ServerCallContext context)
        {
            int numMaxCarateres = 20;

            Console.WriteLine("Recebido pedido de alteraçãode password do utilizador " + pedido.Login);

            // Verifica se o tamanho da password é superior ao num max de caracteres definido em "numMaxCarateres"
            if (pedido.NewPass.Length > numMaxCarateres)
            {
                return Task.FromResult(new PasswordChanged { PassAlterada = false });
            }


            // para testar antes de ligar à BD
            Boolean resultado = pedido.OldPass == "123";


            // TODO
            // Verifica se a password antiga corresponde com a registada na BD
            if (resultado)
            {
                // TODO
                // Altera a password do utilizador na BD
                Console.WriteLine("Password do utilizador " + pedido.Login + " não foi alterada!");
                return Task.FromResult(new PasswordChanged { PassAlterada = true });
            }
            else
            {
                Console.WriteLine("Password do utilizador " + pedido.Login + " alterada!");
                return Task.FromResult(new PasswordChanged { PassAlterada = false });
            }
        }
    }
}
