using System.Collections.Generic;
using AcessoDados.BaseInterface;
using bludata.entity.Bludata;

namespace bludata.Models.IDao
{
    public interface ITelefoneDao : IBaseDaoInterface<Telefone>
    {
        void InserirEmMassa(Telefone[] parametros, string strConexao, out string mensagem);
        void ExcluirEmMassa(int pessoaCodigo, string strConexao, out string _mensagem);
        IEnumerable<Telefone> ListarTelefone(object parametros, string strConexao);
    }
}
