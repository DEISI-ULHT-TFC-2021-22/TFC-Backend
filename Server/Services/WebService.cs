using Grpc.Core;
using System.Data;
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
    public class WebService : Web.WebBase
    {
        public override async Task GetAllUsers(Nothing pedido, IServerStreamWriter<DadosUser> responseStream, ServerCallContext context)
        {
            var dadosUser = new DadosUser();

            DataTable dtUsers = SQLAccess.ExecSqlDt("exec spUtilizadores_ListarTodos");

            foreach (DataRow row in dtUsers.Rows)
            {
                dadosUser.Nome = row[0].ToString();
                dadosUser.Login = row[1].ToString();
                dadosUser.ContaAtivada = Boolean.Parse(row[2].ToString());
                dadosUser.TipoUser = row[3].ToString();
                await responseStream.WriteAsync(dadosUser).ConfigureAwait(false);
            }
        }

        public override async Task GetAllMatriculas(User pedido, IServerStreamWriter<ListaMatriculas> responseStream, ServerCallContext context)
        {
            var matriculas = new ListaMatriculas();
            DataTable dtMatriculas = (SQLAccess.ExecSqlDt("exec spMatriculas_ListarTodas '" + pedido.Login + "'"));

            foreach (DataRow row in dtMatriculas.Rows)
            {
                matriculas.Matricula = row[0].ToString();
                await responseStream.WriteAsync(matriculas).ConfigureAwait(false);
            }
        }

        public override Task<Nothing> InsertUser(DadosUserInsert pedido, ServerCallContext context)
        {
            Console.WriteLine("Utilizador Inserido: " + Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spUtilizadores_Inserir '" + pedido.TipoUser + "', '" + pedido.Nome + "', '" + pedido.Login + "', '" + pedido.Password + "', '" + pedido.ContaAtivada + "'")));

            return Task.FromResult(new Nothing());
        }

        public override Task<Nothing> EditUser(DadosUser pedido, ServerCallContext context)
        {
            Console.WriteLine("Utilizador Alterado: " + Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spUtilizadores_Alterar '" + pedido.TipoUser + "', '" + pedido.Nome + "', '" + pedido.Login + "', '" + pedido.ContaAtivada + "'")));

            return Task.FromResult(new Nothing());
        }

        public override Task<DadosUser> SearchUser(User pedido, ServerCallContext context)
        {
            DataTable dtUsers = SQLAccess.ExecSqlDt("exec spUtilizadores_Procurar '" + pedido.Login + "'");
            DataRow row = dtUsers.Rows[0];
            var dadosUser = new DadosUser();

            try
            {
                dadosUser.Nome = row[0].ToString();
                dadosUser.Login = row[1].ToString();
                dadosUser.ContaAtivada = Boolean.Parse(row[2].ToString());
                dadosUser.TipoUser = row[3].ToString();
            }
            catch (Exception)
            {

            }

            return Task.FromResult(dadosUser);
        }

        public override Task<DadosUser> SearchMatricula(ListaMatriculas matricula, ServerCallContext context)
        {
            DataTable dtUsers = SQLAccess.ExecSqlDt("exec spMatriculas_ProcurarProprietario '" + matricula.Matricula + "'");
            DataRow row = dtUsers.Rows[0];
            var dadosUser = new DadosUser();

            try
            {
                dadosUser.Nome = row[0].ToString();
                dadosUser.Login = row[1].ToString();
                dadosUser.ContaAtivada = Boolean.Parse(row[2].ToString());
                dadosUser.TipoUser = row[3].ToString();
            }
            catch (Exception)
            {

            }

            return Task.FromResult(dadosUser);
        }

        public override Task<Nothing> InsertMatricula(DadosAuto pedido, ServerCallContext context)
        {
            Console.WriteLine("Matricula Inserida: " + Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spMatriculas_Inserir '" + pedido.Login + "', '" + pedido.Matricula + "'")));

            return Task.FromResult(new Nothing());
        }

        public override Task<Nothing> EditMatricula(DadosAuto pedido, ServerCallContext context)
        {
            Console.WriteLine("Matricula Alterada: " + Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spMatriculas_Alterar '" + pedido.Login + "', '" + pedido.Matricula + "'")));

            return Task.FromResult(new Nothing());
        }

        public override Task<Nothing> InsertEntryPermition(DadosPermEntrada pedido, ServerCallContext context)
        {
            Console.WriteLine("Permição de Entrada Inserida: " + Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spPermiteEntrada_Inserir '" + pedido.Login + "', '" + pedido.PagoDesde + "', '" + pedido.PagoAte + "', '" + pedido.ParqueGratis + "'")));

            return Task.FromResult(new Nothing());
        }

        public override Task<Nothing> EditEntryPermition(DadosPermEntrada pedido, ServerCallContext context)
        {
            Console.WriteLine("Permição de Entrada Alterada: " + Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spPermiteEntrada_Alterar '" + pedido.Login + "', '" + pedido.PagoDesde + "', '" + pedido.PagoAte + "', '" + pedido.ParqueGratis + "'")));

            return Task.FromResult(new Nothing());
        }

        public override Task<DadosPermEntrada> GetEntryPermitionData(User pedido, ServerCallContext context)
        {
            DataTable dtPermEntry = SQLAccess.ExecSqlDt("exec spPermiteEntrada_Info '" + pedido.Login + "'");
            DataRow row = dtPermEntry.Rows[0];
            var dadosPerm = new DadosPermEntrada();

            dadosPerm.Login = pedido.Login;
            dadosPerm.PagoDesde = row[0].ToString();
            dadosPerm.PagoAte = row[1].ToString();
            dadosPerm.ParqueGratis = Boolean.Parse(row[2].ToString());

            return Task.FromResult(dadosPerm);
        }

        public override Task<Nothing> RegisterEntry(DadosAuto pedido, ServerCallContext context)
        {
            Console.WriteLine("Registo de Entrada Inserido: " + Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spRegistos_Entrada '" + pedido.Login + "', '" + pedido.Matricula + "'")));

            return Task.FromResult(new Nothing());
        }

        public override Task<Nothing> RegisterExit(DadosAuto pedido, ServerCallContext context)
        {
            Console.WriteLine("Registo de Saida Inserido: " + Boolean.Parse(SQLAccess.ExecSQLReturnData("exec spRegistos_Saida '" + pedido.Login + "', '" + pedido.Matricula + "'")));

            return Task.FromResult(new Nothing());
        }

        public override Task<MatriculaValida> PlateRevalidation(DadosAuto pedido, ServerCallContext context)
        {
            return Task.FromResult(new MatriculaValida { Resposta = SQLAccess.ExecSQLReturnData("exec spMatriculas_MatriculaValida '" + pedido.Login + "', '" + pedido.Matricula + "'") });
        }
    }
}
