using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace bludata.Models.Bo
{
    internal static class ClienteBo
    {
        public static string ListarCliente(this string alvoString, dynamic parametros)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(alvoString);

            Type t = parametros.GetType();
            if (t.GetProperty("CNPJ") != null && !string.IsNullOrEmpty(parametros.CNPJ))
                sb.AppendLine("and\tCliente.cnpj = @CNPJ");

            return sb.ToString();
        }

    }

    internal static class PessoaBo
    {
        public static string ListarPessoa(this string alvo, dynamic parametros)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(alvo);

            Type t = parametros.GetType();
            if (t.GetProperty("Codigo") != null
                && parametros.Codigo != null)
                sb.AppendLine("and\tPessoa.codigo = @Codigo");

            if (t.GetProperty("Nome") != null
                && parametros.Nome != null)
                sb.AppendLine("and\tPessoa.nome LIKE '%' + @Nome + '%'");

            if (t.GetProperty("Cliente") != null
                && parametros.Cliente != null)
                sb.AppendLine("and\tPessoa.Cliente = @Cliente");

            if (t.GetProperty("Cpf") != null
                && parametros.Cpf != null)
                sb.AppendLine("and\tPessoa.cpf = @Cpf");

            if (t.GetProperty("DataCadastro") != null
                && parametros.DataCadastro != null)
                sb.AppendLine("and\tPessoa.dt_cadastro between @DataCadastro and DATEADD(hour,24, @DataCadastro)");

            if (t.GetProperty("DataNascimento") != null
                && parametros.DataNascimento != null)
                sb.AppendLine("and\tPessoa.dt_nascimento between @DataNascimento and DATEADD(hour,24, @DataNascimento)");

            return sb.ToString();
        }
    }

    internal static class Generico
    {
        public static string StriptInserir<T>(this T model)
        {
            Type t = typeof(T);
            var atributo = t.GetCustomAttribute<TableAttribute>();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into {0}.{1}", atributo.Schema, atributo.Name);

            List<string> colunas = new List<string>();
            List<string> parametros = new List<string>();
            foreach (PropertyInfo p in t.GetProperties())
            {
                ColumnAttribute coluna = p.GetCustomAttribute<ColumnAttribute>();
                if (coluna != null)
                {
                    colunas.Add(coluna.Name);
                    parametros.Add(p.Name);
                }
            }
            sb.AppendFormat("({0})", string.Join(", ", colunas.ToArray()));
            sb.AppendFormat(" Values (@{0})", string.Join(", @", parametros.ToArray()));

            return sb.ToString();
        }

        public static string InserirEmMassa<T>(this T[] massa, bool includePK = false)
        {
            Type t = typeof(T);
            var atributo = t.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.TableAttribute>();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into {0}.{1}", atributo.Schema, atributo.Name);

            List<string> colunas = new List<string>();
            List<string> parametros = new List<string>();
            foreach (PropertyInfo p in t.GetProperties())
            {
                KeyAttribute pk = p.GetCustomAttribute<KeyAttribute>();
                if (pk != null && !includePK) continue;

                ColumnAttribute coluna = p.GetCustomAttribute<ColumnAttribute>();
                if (coluna != null)
                {
                    colunas.Add(coluna.Name);
                    parametros.Add(p.Name);
                }
            }
            sb.AppendFormat("({0})", string.Join(", ", colunas.ToArray()));
            sb.AppendFormat(" Values (@{0})", string.Join(", @", parametros.ToArray()));

            return sb.ToString();
        }
    }

    internal static class TelefoneBo
    {
        public static string ListarTelefone(this string alvo, dynamic parametros)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(alvo);

            Type t = parametros.GetType();
            if (t.GetProperty("Pessoa") != null
                && !string.IsNullOrEmpty(parametros.Pessoa as string))
                sb.AppendLine("and\tTelefone.pessoa = @Pessoa");

            return sb.ToString();
        }

        public static string ExcluirEmMassa(this int pessoa)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("delete");
            sb.AppendLine("from\ttelefone");
            sb.AppendFormat("where\tpessoa={0}", pessoa);
            return sb.ToString();
        }
    }
}