using System.Web.Http;

namespace bludata.Areas.WebApi
{
    public class ApiBludataController:ApiController
    {
        private string _strConexao;

        public ApiBludataController():base()
        {
            this.StrConexao = "BludataConnection";
        }

        protected string StrConexao
        {
            get
            {
                return _strConexao;
            }

            set
            {
                _strConexao = value;
            }
        }
    }
}