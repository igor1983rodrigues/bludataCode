using bludata.Models.IDao;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bludata.Controllers
{
    public class PrincipalController : Controller
    {
        private IClienteDao iClienteDao;

        public PrincipalController(IClienteDao iClienteDao) : base()
        {
            this.iClienteDao = iClienteDao;
        }
        // GET: Principal
        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies["appBludata"];
            if (cookie != null && cookie.Value != null)
            {
                ViewBag.Cliente = cookie["Cliente"];
                return View("Principal");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login(int? id = null)
        {
            if ((id ?? 0) > 0)
            {
                HttpCookie cookie = new HttpCookie("appBludata");
                cookie.Values.Add("Cliente", id.Value.ToString());
                cookie.Expires = DateTime.Now.AddHours(8D);
                Response.Cookies.Add(cookie);

                return Redirect("~/");
            }
            else
            {
                int nrClientesQtd = iClienteDao.ObterTodos("BludataConnection").Count();
                if (nrClientesQtd > 0)
                    return View();
                else
                    return RedirectToAction("Cadastrar", "Cliente");
            }
        }
    }
}
