using bludata.Models.IDao;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace bludata.Areas.WebApi
{
    [RoutePrefix("api/uf")]
    public class ApiUfController: ApiBludataController
    {
        private IUfDao iUfDao;

        public ApiUfController(IUfDao iUfDao)
        {
            this.iUfDao = iUfDao;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var resposta = await Task.Run(() => iUfDao.ObterTodos(StrConexao));

                return Ok(resposta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}