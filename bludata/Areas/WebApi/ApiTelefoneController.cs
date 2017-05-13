using bludata.Models.IDao;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace bludata.Areas.WebApi
{
    [RoutePrefix("api/telefone")]
    public class ApiTelefoneController : ApiBludataController
    {
        private ITelefoneDao iTelefoneDao;

        public ApiTelefoneController(ITelefoneDao iTelefoneDao)
        {
            this.iTelefoneDao = iTelefoneDao;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var resposta = await Task.Run(() => iTelefoneDao.ObterTodos(StrConexao));

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("pesquisar/{Pessoa}")]
        public async Task<IHttpActionResult> Pesquisar(string Pessoa)
        {
            try
            {
                var resposta = await Task.Run(() => iTelefoneDao.ListarTelefone(new {
                    Pessoa
                }, StrConexao));

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}