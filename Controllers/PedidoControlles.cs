using CRACKED.Dtos;
using CRACKED.Repositories;
using CRACKED.Services;
using CRACKED.Models;
using System;
using System.Linq;
using System.Web.Mvc;


namespace CRACKED.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoService _pedidoService;
        private readonly PedidoRepository _pedidoRepository;

        // Constructor donde se inyectan el servicio y el repositorio
        public PedidoController(PedidoService pedidoService, PedidoRepository pedidoRepository)
        {
            _pedidoService = pedidoService;
            _pedidoRepository = pedidoRepository;
        }

        // Método para cargar las ciudades por departamento
        //public JsonResult GetCiudadesPorDepartamento(int departamentoId)
        //{
        //    var ciudades = _pedidoRepository.GetCiudades(departamentoId);
        //    return Json(ciudades.Select(c => new { c.idCiudad, c.nombre }), JsonRequestBehavior.AllowGet);
        //}

        //// Método para generar un pedido

        //public ActionResult DatosEntrega(PedidoDto pedidoDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var userId = (int)Session["UserLogged"]; // Obtener el ID del usuario logueado


        //                PedidoDto pedidoDtos = _pedidoService.ObtenerDatosEntrega(userId);  // Devuelve un solo objeto

        //                // Aquí ya no es necesario usar First(), ya que 'pedidoDto' es un solo objeto
        //                var pedido = pedidoDtos;
        //                decimal subtotal = pedido.Subtotal;  // Accediendo correctamente a la propiedad Subtotal
        //                decimal costoEnvio = pedido.ValorDomicilio;
        //                decimal total = pedido.TotalPedido;

        //                // Procesar otros datos y devolver la vista
        //                ViewBag.Subtotal = subtotal;
        //                ViewBag.CostoEnvio = costoEnvio;
        //                ViewBag.Total = total;

        //                // Obtener los departamentos y ciudades
        //                var departamentos = _pedidoService.ObtenerDepartamentos();
        //                var ciudades = _pedidoService.ObtenerCiudades(pedidoDto.IdDepartamento);

        //                ViewBag.Departamentos = new SelectList(departamentos, "Id", "Nombre");
        //                ViewBag.Ciudades = new SelectList(ciudades, "Id", "Nombre");

        //                return View("DatosEntrega", pedido);

        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("", $"Error al generar el pedido: {ex.Message}");
        //        }
        //    }

        //    return View("DatosEntrega", pedidoDto);
        //}



        //// Acción para mostrar la confirmación del pedido
        //public ActionResult ConfirmacionPedido()
        //        {
        //            var pedidoId = TempData["PedidoId"];
        //            if (pedidoId == null)
        //            {
        //                return RedirectToAction("DatosEntrega");
        //            }
        //            ViewBag.PedidoId = pedidoId;
        //            return View();
        //        }
        //    }
    }
}
        //}
