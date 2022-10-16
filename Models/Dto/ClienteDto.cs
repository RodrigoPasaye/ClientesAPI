using System.ComponentModel.DataAnnotations;

namespace ClientesAPI.Models.Dto {
    public class ClienteDto {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
