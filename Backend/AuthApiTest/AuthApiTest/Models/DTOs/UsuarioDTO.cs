namespace AuthApiTest.Models.DTOs
{
    public class UsuarioDTO
    {
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public DateOnly FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string Password { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Estado { get; set; }

    }
}
