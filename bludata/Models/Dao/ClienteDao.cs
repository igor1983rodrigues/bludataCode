using System;
using AcessoDados.BaseRepository;
using bludata.entity.Bludata;
using bludata.Models.IDao;
using bludata.Models.Resources;
using bludata.Models.Bo;
using System.Collections.Generic;

namespace bludata.Models.Dao
{
    public class ClienteDao : BaseDaoRepository<Cliente>, IClienteDao
    {
        public IEnumerable<Cliente> ListarCliente(object parametros, string strConexao)
        {
            string sqlString = ClienteSql.ListarCliente.ListarCliente(parametros);

            return ExecuteQuery<Cliente, Uf, Cliente>(sqlString, parametros, strConexao, (cliente, uf) =>
            {
                cliente.Uf = uf;
                return cliente;
            }, new string[] { "UfCodigo" });
        }
    }
}