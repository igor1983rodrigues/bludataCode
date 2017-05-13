using AcessoDados.BaseRepository;
using bludata.entity.Bludata;
using bludata.Models.Bo;
using bludata.Models.IDao;
using System;

namespace bludata.Models.Dao
{
    public class RgDao : BaseDaoRepository<Rg>, IRgDao
    {
        public int InserirRg(Rg model, out string mensagem, string strConexao)
        {
            mensagem = null;
            int id = 0;
            string sqlString = model.StriptInserir();
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    conn.Open();

                    ExecuteCommand(sqlString, model, strConexao, out mensagem);

                    if (!string.IsNullOrEmpty(mensagem))
                        id = model.PessoaCodigo;

                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }

                return id;
            }
        }
    }
}