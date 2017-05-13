﻿using AcessoDados.BaseInterface;
using bludata.entity.Bludata;

namespace bludata.Models.IDao
{
    public interface IRgDao : IBaseDaoInterface<Rg>
    {
        int InserirRg(Rg model, out string mensagem, string strConexao);
    }
}
