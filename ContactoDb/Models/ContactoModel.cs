using System.ComponentModel.DataAnnotations;


namespace ContactoDb.Models
{
    public class ContactoModel
    {
        public int IdContacto { get; set; }
        [Required(ErrorMessage="El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        [Required(ErrorMessage = "El campo Clave es obligatorio")]
        public string Clave { get; set; }
    }
}
