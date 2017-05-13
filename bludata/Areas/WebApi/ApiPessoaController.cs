using bludata.entity.Bludata;
using bludata.Models.IDao;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace bludata.Areas.WebApi
{
    [RoutePrefix("api/pessoa")]
    public class ApiPessoaController : ApiBludataController
    {
        private IPessoaDao iPessoaDao;
        private string _mensagem;
        private IClienteDao iClienteDao;
        private IRgDao iRgDao;
        private ITelefoneDao iTelefoneDao;

        public ApiPessoaController(IPessoaDao iPessoaDao, IClienteDao iClienteDao, IRgDao iRgDao, ITelefoneDao iTelefoneDao) : base()
        {
            this.iPessoaDao = iPessoaDao;
            this.iClienteDao = iClienteDao;
            this.iRgDao = iRgDao;
            this.iTelefoneDao = iTelefoneDao;
        }

        [HttpGet]
        [Route("pesquisar")]
        public async Task<IHttpActionResult> Pesquisar(int? Codigo = null, string Nome = null, int? Cliente = null, string Cpf = null, string DataNascimento = null, string DataCadastro = null)
        {
            try
            {
                var resposta = await Task.Run(() => iPessoaDao.ListarPessoa(new
                {
                    Codigo,
                    Nome,
                    Cliente,
                    Cpf,
                    DataCadastro = !string.IsNullOrEmpty(DataCadastro) ? (DateTime?)DateTime.Parse(DataCadastro) : null,
                    DataNascimento = !string.IsNullOrEmpty(DataNascimento) ? (DateTime?)DateTime.Parse(DataNascimento) : null
                }, StrConexao));

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var resposta = await Task.Run(() => iPessoaDao.ObterTodos(StrConexao));

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]JObject value)
        {
            try
            {
                _mensagem = null;
                Pessoa pessoa = value.ToObject<Pessoa>();

                int cpfCadastrado = await Task.Run(() => iPessoaDao.ListarPessoa(new
                {
                    Cpf = pessoa.PessoaCpf,
                    Cliente = pessoa.ClienteCodigo
                }, StrConexao).Count());

                if (cpfCadastrado > 0)
                    throw new Exception("Já existe uma pessoa cadastrada neste cliente com este CPF.");

                Rg rg = null;
                if (value["rg"] != null) rg = value["rg"].ToObject<Rg>();

                pessoa.Cliente = await Task.Run(() => iClienteDao.ObterPorChave(pessoa.ClienteCodigo, StrConexao));

                int comparaDatas = pessoa.PessoaDataNascimento.AddYears(18).CompareTo(DateTime.Now);

                bool regraPRBool = pessoa.Cliente.UfCodigo == "PR" && comparaDatas < 1,
                    regraSCBool = pessoa.Cliente.UfCodigo == "SC" && rg != null && rg.RgNumero > 0;

                if (!regraPRBool && !regraSCBool)
                {
                    if (pessoa.Cliente.UfCodigo == "PR") throw new Exception("Este cliente só permite cadastro de pessoas acima dos 18 anos.");
                    else throw new Exception("Este cliente só permite cadastro de pessoas com RG informado.");
                }

                pessoa.PessoaDataCadastro = DateTime.Now;
                pessoa.PessoaCodigo = await Task.Run(() => iPessoaDao.Inserir(pessoa, out _mensagem, StrConexao));
                if (pessoa.PessoaCodigo == 0)
                    throw new Exception(_mensagem);

                if (rg != null)
                {
                    _mensagem = null;
                    rg.PessoaCodigo = pessoa.PessoaCodigo;
                    await Task.Run(() => iRgDao.InserirRg(rg, out _mensagem, StrConexao));
                    if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);
                }

                List<Telefone> telefones = value["telefones"].ToObject<List<Telefone>>();
                await Task.Run(() => iTelefoneDao.InserirEmMassa(telefones.Select(t => new Telefone()
                {
                    PessoaCodigo = pessoa.PessoaCodigo,
                    TelefoneDdd = t.TelefoneDdd,
                    TelefoneNumero = t.TelefoneNumero,
                    TipoTelefoneCodigo = t.TipoTelefoneCodigo
                }).ToArray(), StrConexao, out _mensagem));
                if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);

                return Ok(new
                {
                    Id = pessoa.PessoaCodigo,
                    Mensagem = "Cadastro salvo com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put([FromBody]JObject value)
        {
            try
            {
                Pessoa pessoa = value.ToObject<Pessoa>();
                //pessoa = await Task.Run(() =>
                //{
                //    Pessoa pessoaTemp = iPessoaDao.ObterPorChave(pessoa.PessoaCodigo, StrConexao);
                //    pessoa.PessoaDataCadastro = pessoaTemp.PessoaDataCadastro;

                //    return pessoa;
                //});

                Rg rg = null;
                if (value["rg"] != null) rg = value["rg"].ToObject<Rg>();

                pessoa.Cliente = await Task.Run(() => iClienteDao.ObterPorChave(pessoa.ClienteCodigo, StrConexao));

                int comparaDatas = pessoa.PessoaDataNascimento.AddYears(18).CompareTo(DateTime.Now);

                bool regraPRBool = pessoa.Cliente.UfCodigo == "PR" && comparaDatas < 1,
                    regraSCBool = pessoa.Cliente.UfCodigo == "SC" && rg != null && rg.RgNumero > 0;

                if (!regraPRBool && !regraSCBool)
                {
                    if (pessoa.Cliente.UfCodigo == "PR") throw new Exception("Este cliente só permite cadastro de pessoas acima dos 18 anos.");
                    else throw new Exception("Este cliente só permite cadastro de pessoas com RG informado.");
                }

                _mensagem = null;
                await Task.Run(() => iPessoaDao.Alterar(pessoa, out _mensagem, StrConexao));
                if (!string.IsNullOrEmpty(_mensagem))
                    throw new Exception(_mensagem);

                if (rg != null)
                {
                    _mensagem = null;
                    await Task.Run(() => iRgDao.Alterar(rg, out _mensagem, StrConexao));
                    if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);
                }

                List<Telefone> telefones = value["telefones"].ToObject<List<Telefone>>();

                await Task.Run(() => iTelefoneDao.ExcluirEmMassa(pessoa.PessoaCodigo, StrConexao, out _mensagem));
                if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);

                await Task.Run(() => iTelefoneDao.InserirEmMassa(telefones.Select(t => new Telefone()
                {
                    PessoaCodigo = pessoa.PessoaCodigo,
                    TelefoneDdd = t.TelefoneDdd,
                    TelefoneNumero = t.TelefoneNumero,
                    TipoTelefoneCodigo = t.TipoTelefoneCodigo
                }).ToArray(), StrConexao, out _mensagem));
                if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);

                return Ok(new
                {
                    Mensagem = "Cadastro alterado com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                Pessoa pessoa = await Task.Run(() => iPessoaDao.ObterPorChave(id, StrConexao));

                var telefones = await Task.Run(() => iTelefoneDao.ListarTelefone(new { Pessoa = pessoa.PessoaCodigo }, StrConexao));
                await Task.Run(() =>
                {
                    foreach (var item in telefones)
                    {
                        iTelefoneDao.Excluir(item.TelefoneCodigo, out _mensagem, StrConexao);
                        if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);
                    }
                });

                Rg rg = await Task.Run(() => iRgDao.ObterPorChave(pessoa.PessoaCodigo, StrConexao));
                if (rg != null)
                {
                    await Task.Run(() => iRgDao.Excluir(pessoa.PessoaCodigo, out _mensagem, StrConexao));
                    if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);
                }

                await Task.Run(() => iPessoaDao.Excluir(pessoa.PessoaCodigo, out _mensagem, StrConexao));
                if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);

                return Ok(new { Mensagem = "Registro excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
