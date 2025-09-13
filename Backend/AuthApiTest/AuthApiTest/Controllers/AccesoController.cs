using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using AuthApiTest.Custom;
using AuthApiTest.Models;
using AuthApiTest.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AuthApiTest.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AuthApiContext _context;
        private readonly Utilidades _utilidades;

        public AccesoController(AuthApiContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult>Register(UsuarioDTO objeto)
        {
            var modeloUsuario = new Usuario
            {
                Nombres = objeto.Nombres,
                Apellidos = objeto.Apellidos,
                Telefono = objeto.Telefono,
                Password = _utilidades.encriptarSHA256(objeto.Password),
                Email = objeto.Email,
                FechaNacimiento = objeto.FechaNacimiento,
                Direccion = objeto.Direccion,
                Estado = "A"

            };
            await _context.Usuarios.AddAsync(modeloUsuario);
            await _context.SaveChangesAsync();

            if(modeloUsuario.Id != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            }else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }

        }


        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _context.Usuarios.
                Where(u => u.Telefono == objeto.Telefono && u.Password == _utilidades.encriptarSHA256(objeto.Password)).FirstOrDefaultAsync();

            if (usuarioEncontrado == null) 
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = ""});
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado) });
            }
        }

        
    }
}
