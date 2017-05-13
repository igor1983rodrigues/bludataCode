using System.Web.Mvc;

namespace bludata.Controllers
{
    public class PessoaController : Controller
    {
        public ActionResult Index()
        {
            return View("Listar");
        }

        public ActionResult Novo()
        {
            return View();
        }
    }
}