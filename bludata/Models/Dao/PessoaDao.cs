using AcessoDados.BaseRepository;
using bludata.Models.IDao;
using bludata.entity.Bludata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bludata.Models.Resources;
using bludata.Models.Bo;

namespace bludata.Models.Dao
{
    public class PessoaDao : BaseDaoRepository<Pessoa>, IPessoaDao
    {
        public IEnumerable<Pessoa> ListarPessoa(object parametros, string strConexao)
        {
            string sqlString = PessoaSql.ListarPessoa.ListarPessoa(parametros);
            return ExecuteQuery<Pessoa, Uf, Cliente, Uf, Rg, Uf, Pessoa>(sqlString, parametros, strConexao, (pessoa, ufPessoa, cliente, ufCliente, rg, ufRg) =>
            {
                if (rg != null)
                {
                    rg.Uf = ufRg;
                    pessoa.Rg = rg;
                }
                cliente.Uf = ufCliente;
                pessoa.Cliente = cliente;
                pessoa.Uf = ufPessoa;
                return pessoa;
            }, new string[] { "UfCodigo", "ClienteCodigo", "UfCodigo", "PessoaCodigo", "UfCodigo" }, System.Data.CommandType.Text);
        }
    }
}