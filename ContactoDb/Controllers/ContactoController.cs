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
            if(!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = contactosDatos.GuardarContacto(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else 
            {

                return View(); 
            }
        }
        [HttpGet]
        public IActionResult Editar(int IdContacto) 
        {
            ContactoModel contacto = contactosDatos.ObtenerContacto(IdContacto);
            return View(contacto);
        }
        [HttpPost]
        public IActionResult Editar(ContactoModel model)
        {
            var result = contactosDatos.EditarContacto(model);
            if (result)
            {
                return RedirectToAction("Listar");
            }
            else
            {
            return View();
            }
        }
        [HttpGet]
        public IActionResult Eliminar(int IdContacto)
        {
            var contact = contactosDatos.ObtenerContacto(IdContacto);
            return View(contact);//uwu
        }

        [HttpPost]
        public IActionResult Eliminar(ContactoModel contact)
        {
            var result = contactosDatos.EliminarContacto(contact);
            if (result)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
    }
}
