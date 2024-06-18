using System.ComponentModel.DataAnnotations;

namespace apiCargueClientes.Models
{
    public class Cliente
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int edad { get; set; }
        public string email { get; set; }
        public int telefono { get; set; }
        public string direccion { get; set; }
        public int documento { get; set; }
        public string tipo_documento { get; set; }
    }

}
