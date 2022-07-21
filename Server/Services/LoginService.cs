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
        private readonly int numMaxCarateres = 64;

        public override Task<LoginAutorization> LoginRequest(LoginData pedido, ServerCallContext context)
        {
            Boolean loginAutorizado = false;

            Console.WriteLine("Recebido pedido de autenticação do utilizador " + pedido.Login);

            // Obter tipo de utilizador
            string tipoUser = SQLAccess.ExecSQLReturnData("exec spUtilizadores_GetTipoUtilizador '" + pedido.Login + "'");

            // Verifica se o tamanho da password é superior ao num max de caracteres definido em "numMaxCarateres"
            if (pedido.Password.Length > numMaxCarateres)
            {
                return Task.FromResult(new LoginAutorization { LoginEfetuado = loginAutorizado, TipoUser = tipoUser });
            }

            // Validar os dados de login na BD
            if (Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spUtilizadores_LoginValido '" + pedido.Login + "', '" + pedido.Password + "'")))
            {
                if (Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spUtilizadores_ContaAtivada '" + pedido.Login + "'")))
                {
                    loginAutorizado = true;
                    Console.WriteLine("Autenticação do utilizador bem sucedida!");
                }
                else
                {
                    Console.WriteLine("A sua conta encontra-se desativada!");
                }
            }
            else
            {
                Console.WriteLine("Autenticação invalida!");
            }

            return Task.FromResult(new LoginAutorization { LoginEfetuado = loginAutorizado, TipoUser = tipoUser });
        }

        public override Task<PasswordChanged> ChangePassword(NewLoginData pedido, ServerCallContext context)
        {
            Console.WriteLine("Recebido pedido de alteraçãode password do utilizador " + pedido.Login);

            // Verifica se o tamanho da password é superior ao num max de caracteres definido em "numMaxCarateres"
            if (pedido.NewPass.Length > numMaxCarateres)
            {
                return Task.FromResult(new PasswordChanged { PassAlterada = false });
            }

            // Verifica se a password antiga corresponde com a registada na BD
            if (Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spUtilizadores_LoginValido '" + pedido.Login + "', '" + pedido.OldPass + "'")))
            {
                // Altera a password do utilizador na BD
                if (Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spUtilizadores_AlterarPassword '" + pedido.Login + "', '" + pedido.NewPass + "'")))
                {
                    Console.WriteLine("Password do utilizador " + pedido.Login + " alterada!");
                    return Task.FromResult(new PasswordChanged { PassAlterada = true });
                }
                else
                {
                    Console.WriteLine("Password do utilizador " + pedido.Login + " não foi alterada!");
                    return Task.FromResult(new PasswordChanged { PassAlterada = false });
                }
            }
            else
            {
                Console.WriteLine("Password do utilizador " + pedido.Login + " não foi alterada!");
                return Task.FromResult(new PasswordChanged { PassAlterada = false });
            }
        }
    }
}
