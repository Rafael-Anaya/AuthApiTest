using System;
using System.Collections.Generic;

namespace AuthApiTest.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Direccion { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateOnly FechaCreacion { get; set; }

    public DateOnly? FechaModificacion { get; set; }
}
