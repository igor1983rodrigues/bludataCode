using System;
using System.Collections.Generic;
using AcessoDados.BaseRepository;
using bludata.entity.Bludata;
using bludata.Models.IDao;
using bludata.Models.Bo;
using bludata.Models.Resources;
using System.Data;

namespace bludata.Models.Dao
{
    public class TelefoneDao : BaseDaoRepository<Telefone>, ITelefoneDao
    {
        public void ExcluirEmMassa(int pessoaCodigo, string strConexao, out string mensagem)
        {
            mensagem = null;
            string comandoString = pessoaCodigo.ExcluirEmMassa();
            ExecuteCommand(comandoString, null, strConexao, out mensagem);
        }

        public void InserirEmMassa(Telefone[] parametros, string strConexao, out string mensagem)
        {
            mensagem = null;
            if (parametros.Length > 0)
            {
                string comandoString = parametros.InserirEmMassa();
                ExecuteCommand(comandoString, parametros, strConexao, out mensagem);
            }
        }

        public IEnumerable<Telefone> ListarTelefone(object parametros, string strConexao)
        {
            string sqlString = TelefoneSql.ListarTelefone.ListarTelefone(parametros);

            return ExecuteQuery<Telefone, TipoTelefone, Telefone>(sqlString, parametros, strConexao, (telefone, tipoTelefone) =>
            {
                telefone.TipoTelefone = tipoTelefone;
                return telefone;
            }, new string[] { "TipoTelefoneCodigo" }, CommandType.Text);
        }
    }
}