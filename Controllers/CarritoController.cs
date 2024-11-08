using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRACKED.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        public ActionResult  Carrito()
        {
            return View();
        }
        public ActionResult DatosEntrega()
        {
            return View();
        }

    }
}