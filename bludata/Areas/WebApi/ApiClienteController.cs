using bludata.entity.Bludata;
using bludata.Models.IDao;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace bludata.Areas.WebApi
{
    [RoutePrefix("api/cliente")]
    public class ApiClienteController : ApiBludataController
    {
        private IClienteDao iClienteDao;
        private string _mensagem;

        public ApiClienteController(IClienteDao iClienteDao)
        {
            this.iClienteDao = iClienteDao;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var resposta = await Task.Run(() => iClienteDao.ObterTodos(StrConexao));

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{Codigo:int}")]
        public async Task<IHttpActionResult> Get(int Codigo)
        {
            try
            {
                var resposta = await Task.Run(() => iClienteDao.ObterPorChave(Codigo, StrConexao));

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("login/{crypt}")]
        public async Task<IHttpActionResult> DoLogin(string crypt)
        {
            try
            {
                byte[] codificado = Convert.FromBase64String(crypt);
                string cnpj = System.Text.Encoding.ASCII.GetString(codificado);

                var resposta = await Task.Run(() =>
                {
                    var cliente = iClienteDao.ListarCliente(new { CNPJ = cnpj }, StrConexao).FirstOrDefault();
                    if (cliente == null) throw new Exception("Não foi encontrado nenhum cliente com este CNPJ");
                    return cliente;
                });

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]JObject parametros)
        {
            try
            {
                Cliente cliente = parametros.ToObject<Cliente>();
                int id = await Task.Run(() => iClienteDao.Inserir(cliente, out _mensagem, StrConexao));
                if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);
                return Ok(new { Id = id, mensagem = "Salvo com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put([FromBody]JObject parametros)
        {
            try
            {
                Cliente cliente = parametros.ToObject<Cliente>();
                await Task.Run(() => iClienteDao.Alterar(cliente, out _mensagem, StrConexao));
                if (!string.IsNullOrEmpty(_mensagem)) throw new Exception(_mensagem);
                return Ok(new { mensagem = "Atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}