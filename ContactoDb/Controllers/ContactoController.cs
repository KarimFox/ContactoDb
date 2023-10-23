using Microsoft.AspNetCore.Mvc;
using ContactoDb.Datos;
using ContactoDb.Models;
using ContactoDb_ACL.Datos;

namespace ContactoDb.Controllers
{
    public class ContactoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        ContactosDatos contactosDatos = new ContactosDatos();
        public IActionResult Listar()
        {
            var lista = contactosDatos.Listar();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ContactoModel model)
        {
            var respuesta = contactosDatos.GuardarContacto(model);
            if (respuesta)
            
                return RedirectToAction("Listar");
            
            return View();
        }
    }
}
