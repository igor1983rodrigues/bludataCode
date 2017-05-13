using bludata.Models.IDao;
using System.Linq;
using System.Web.Mvc;

namespace bludata.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteDao iClienteDao;

        public ClienteController(IClienteDao iClienteDao) :base()
        {
            this.iClienteDao = iClienteDao;
        }
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {

            if (iClienteDao.ObterTodos("BludataConnection").Count() > 0)
                return RedirectToAction("Index", "Principal");
            else
                return View();
        }
    }
}