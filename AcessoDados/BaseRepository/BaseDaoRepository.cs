using AcessoDados.BaseInterface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AcessoDados.BaseRepository
{
    public class BaseDaoRepository<T> : IBaseDaoInterface<T>
    {
        protected DbConnection ObterConexao(string strConexao)
        {
            string strConnectionString = ConfigurationManager.ConnectionStrings[strConexao].ConnectionString;
            return new SqlConnection(strConnectionString);
        }

        public virtual void Alterar(T model, out string mensagem, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    conn.Open();
                    int id = conn.Update(model);
                    if (id == 0) throw new Exception("Erro ao atualizar os dados. Entre em contato com o administrador.");
                    mensagem = null;
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual void Excluir(object obj, out string mensagem, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    conn.Open();
                    int id = conn.Delete<T>(obj);
                    if (id == 0) throw new Exception("Erro ao excluir os dados. Entre em contato com o administrador.");
                    mensagem = null;
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual int Inserir(T model, out string mensagem, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    conn.Open();
                    mensagem = null;
                    int id = conn.Insert(model) ?? 0;
                    if (id == 0) throw new Exception("Erro ao inserir os dados. Entre em contato com o administrador.");
                    return id;
                }
                catch (Exception ex)
                {
                    mensagem = ex.Message;
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<T> Obter(object parametros, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    conn.Open();
                    return conn.GetList<T>(parametros);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public T ObterPorChave(object parametros, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    conn.Open();
                    return conn.Get<T>(parametros);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<T> ObterTodos(string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    conn.Open();
                    return conn.GetList<T>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual void ExecuteCommand(string sqlString, object objectParams, string strConexao, out string mensagem)
        {
            using (var conn = ObterConexao(strConexao))
            {
                //IDbTransaction transaction = null;
                try
                {
                    conn.Open();
                    //transaction = conn.BeginTransaction();

                    conn.Execute(sqlString, objectParams, commandType: CommandType.Text);
                    //transaction.Commit();

                    mensagem = null;
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    mensagem = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TCampo> ExecuteQuery<TCampo>(string sqlString, object objectParams, string strConexao, CommandType type)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.Query<TCampo>(sqlString, objectParams, commandType: type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<T> ExecuteQuery(string sqlString, object objectParams, string strConexao, CommandType type)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.Query<T>(sqlString, objectParams, commandType: type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<T> ExecuteQuery(object objectParams, string strConexao)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.GetList<T>(objectParams);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, TReturn>(string sqlString, object objectParams, string strConexao, Func<T1, T2, TReturn> map, string[] splitOn, CommandType type = CommandType.Text)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.Query<T1, T2, TReturn>(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, TReturn>(string sqlString, object objectParams, string strConexao, Func<T1, T2, T3, TReturn> map, string[] splitOn, CommandType type = CommandType.Text)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, TReturn>(string sqlString, object objectParams, string strConexao, Func<T1, T2, T3, T4, TReturn> map, string[] splitOn, CommandType type = CommandType.Text)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, TReturn>(string sqlString, object objectParams, string strConexao, Func<T1, T2, T3, T4, T5, TReturn> map, string[] splitOn, CommandType type = CommandType.Text)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, T6, TReturn>(string sqlString, object objectParams, string strConexao, Func<T1, T2, T3, T4, T5, T6, TReturn> map, string[] splitOn, CommandType type = CommandType.Text)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public virtual IEnumerable<TReturn> ExecuteQuery<T1, T2, T3, T4, T5, T6, T7, TReturn>(string sqlString, object objectParams, string strConexao, Func<T1, T2, T3, T4, T5, T6, T7, TReturn> map, string[] splitOn, CommandType type = CommandType.Text)
        {
            using (var conn = ObterConexao(strConexao))
            {
                try
                {
                    return conn.Query(sqlString, map, objectParams, null, true, string.Join(", ", splitOn), null, type);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
