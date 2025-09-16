using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using AuthApiTest.Custom;
using AuthApiTest.Models;
using AuthApiTest.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AuthApiTest.Controllers
{
    [Route("api/v1/auth")]
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
        [Route("login")]

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
