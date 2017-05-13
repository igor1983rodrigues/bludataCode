using System.Collections.Generic;

namespace AcessoDados.BaseInterface
{
    public interface IBaseDaoInterface<T>
    {
        int Inserir(T model, out string mensagem, string strConexao);

        void Alterar(T model, out string mensagem, string strConexao);

        void Excluir(object obj, out string mensagem, string strConexao);

        T ObterPorChave(object parametros, string strConexao);

        IEnumerable<T> Obter(object parametros, string strConexao);

        IEnumerable<T> ObterTodos(string strConexao);
    }
}
