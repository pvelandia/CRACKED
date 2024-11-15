using CRACKED.Dtos;
using CRACKED.Repositories;
using CRACKED.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRACKED.Controllers
{ //vistas trae clases
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        private readonly ProductService _productService;

        public UserController()
        {
            _productService = new ProductService();
        }
     
        public ActionResult Productos()
        {
            TipoProductListDto tiposDeProducto = _productService.ObtenerTiposDeProducto();
            return View(tiposDeProducto); // Pasa ProductListDto a la vista
        }

        public ActionResult Cookiecakes()
        {

            ProductService productService = new ProductService();

            // Filtrar solo los productos de tipo 1 (Cookiecakes)
            ProductListDto productList = productService.ObtenerProductosPorTipo(2);

            return View(productList);


        }

        public ActionResult Detalle_Cookiecakes()
        {
            ProductService productService = new ProductService();

            // Filtrar solo los productos de tipo 1 (Cookiecakes)
            ProductListDto productList = productService.ObtenerProductosPorId(4);

            return View(productList);
        }

        public ActionResult Detalle_Cookiecakes_Oreo()
        {
            ProductService productService = new ProductService();

            // Filtrar solo los productos de tipo 1 (Cookiecakes)
            ProductListDto productList = productService.ObtenerProductosPorId(5);

            return View(productList);
        }
        public ActionResult Detalle_Cookiecakes_Chipcake()
        {
            ProductService productService = new ProductService();

            // Filtrar solo los productos de tipo 1 (Cookiecakes)
            ProductListDto productList = productService.ObtenerProductosPorId(6);

            return View(productList);
        }

        public ActionResult Galletas()
        {
            ProductService productService = new ProductService();

            // Filtrar solo los productos de tipo 1 (Cookiecakes)
            ProductListDto productList = productService.ObtenerProductosPorTipo(1);

            return View(productList);
       
        }
        public ActionResult Detalle_Galletas()
        {
            ProductService productService = new ProductService();

            // Filtrar solo los productos de tipo 1 (Cookiecakes)
            ProductListDto productList = productService.ObtenerProductosPorId(1);

            return View(productList); 
        }

        public ActionResult Detalle_Galletas_CookieNute()
        {
            ProductService productService = new ProductService();

            // Filtrar solo los productos de tipo 1 (Cookiecakes)
            ProductListDto productList = productService.ObtenerProductosPorId(2);

            return View(productList);
        }

        public ActionResult Detalle_Galletas_Rainbow()
        {
            ProductService productService = new ProductService();

            // Filtrar solo los productos de tipo 1 (Cookiecakes)
            ProductListDto productList = productService.ObtenerProductosPorId(3);

            return View(productList);
        }
        public ActionResult IndexAdmin()
        {
            return View();
        }
        public ActionResult IndexDomiciliario()
        {
            return View();
        }
        public ActionResult RegistroUsuarios()
        {
            UserDto usuario = new UserDto();
            return View(usuario);
        }
        public ActionResult ListaUsuarios()
        {
            UserService userService = new UserService();
            UserListDto usersList = userService.ListarUsuarios();
            return View(usersList);
        }

        [HttpPost]
        public ActionResult RegistroUsuarios(UserDto usuario)
        {
            try
            {
                UserService userSevice = new UserService();
                usuario = userSevice.RegistroUsuarios(usuario);

                if (usuario.Respuesta == true)
                {
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.ErrorMessage = usuario.Mensaje;
                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                usuario.Mensaje = ex.Message;
                return View(usuario);
            }

        }

        [HttpGet]

        public ActionResult inicioSesion()
        {
            UserDto user = new UserDto();

            return View(user);
        }

      
        [HttpPost]
        public ActionResult inicioSesion(UserDto user)
        {
            UserService userService = new UserService();
            UserDto userLogin = userService.LoginUser(user); // Llamar a inicioSesion para obtener el UserDto completo

            if (userLogin.IdUser != 0)  // Verifica que el inicio de sesión fue exitoso
            {
                // Almacenar el usuario en la sesión
                Session["UserLogged"] = userLogin;

                


                // Verificar el rol y redirigir a la vista correspondiente
                if (userLogin.IdRol == 1)
                {
                    // Redirigir a la vista para rol de usuario regular
                    return RedirectToAction("Index", "Home");
                }
                else if (userLogin.IdRol == 2)
                {
                    // Redirigir a la vista para administrador
                    ViewData["Layout"] = "_Layout - Admin.cshtml";
                    return RedirectToAction("IndexAdmin", "User");
                }
                else if (userLogin.IdRol == 3)
                {
                    // Redirigir a la vista para domiciliario
                    return RedirectToAction("IndexDomiciliario", "User");
                }
                else
                {
                    // Redirigir a una vista genérica si el rol no coincide con ninguno de los casos anteriores
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // Si las credenciales son incorrectas, mostrar mensaje de error
                ViewBag.ErrorMessage = "Credenciales incorrectas.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            // Eliminar los datos del usuario de la sesión
            Session["UserLogged"] = null;
            return RedirectToAction("Index", "Home");  // Redirigir al inicio después de cerrar sesión
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
        [HttpPost]
        public ActionResult Cookiecakes(UserDto model, int quantity, string selectionInput)
        {
            
            if (model == null)
            {
                return ViewBag.ErrorMessage("El modelo es nulo.");
            }

            // Aquí iría tu lógica para manejar el carrito

            if (quantity <= 0)
            {
                return ViewBag.ErrorMessage("La cantidad debe ser mayor a cero.");
            }

            // Supongamos que todo está bien y quieres redirigir
            // Por ejemplo, después de agregar al carrito
            return RedirectToAction("Index");
        }

    }
}

