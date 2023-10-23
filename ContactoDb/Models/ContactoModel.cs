namespace ContactoDb.Models
{
    public class ContactoModel
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string Clave { get; set; }
    }
}
