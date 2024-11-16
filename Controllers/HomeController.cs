using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRACKED.Dtos;
namespace CRACKED.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Obtener el usuario de la sesión
            var userLogin = Session["UserLogged"] as UserDto;

            // Si el usuario está autenticado, pasa el objeto completo a ViewBag
            if (userLogin != null)
            {
                ViewBag.User = userLogin;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}