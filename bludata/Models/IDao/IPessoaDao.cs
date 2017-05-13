using AcessoDados.BaseInterface;
using bludata.entity.Bludata;
using System.Collections.Generic;

namespace bludata.Models.IDao
{
    public interface IPessoaDao : IBaseDaoInterface<Pessoa>
    {
        IEnumerable<Pessoa> ListarPessoa(object parametros, string strConexao);
    }
}
