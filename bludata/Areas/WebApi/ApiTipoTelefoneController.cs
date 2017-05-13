using bludata.Models.IDao;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace bludata.Areas.WebApi
{
    [RoutePrefix("api/tipotelefone")]
    public class ApiTipoTelefoneController:ApiBludataController
    {
        private ITipoTelefoneDao iTipoTelefoneDao;

        public ApiTipoTelefoneController(ITipoTelefoneDao iTipoTelefoneDao)
        {
            this.iTipoTelefoneDao = iTipoTelefoneDao;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var resposta = await Task.Run(() => iTipoTelefoneDao.ObterTodos(StrConexao));

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}