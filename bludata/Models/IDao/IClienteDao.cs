using AcessoDados.BaseInterface;
using bludata.entity.Bludata;
using System.Collections.Generic;

namespace bludata.Models.IDao
{
    public interface IClienteDao : IBaseDaoInterface<Cliente>
    {
        IEnumerable<Cliente> ListarCliente(object parametros, string strConexao);
    }
}
