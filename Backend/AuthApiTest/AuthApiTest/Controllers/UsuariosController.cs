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

    [Route("v1/users")]
    [ApiController]

    public class UsuariosController : Controller
    {
        private readonly AuthApiContext _context;
        private readonly Utilidades _utilidades;
        public UsuariosController(AuthApiContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;


        }

        [HttpGet]
        [Route("")]
        
        public async Task<IActionResult> UserList()
        {
            var users = await _context.Usuarios.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = users });
        }

        [HttpPost]
        [Route("")]
        [Authorize]

        public async Task<IActionResult> Register(UsuarioDTO objeto)
        {
            var usuario = new Usuario
            {
                Nombres = objeto.Nombres,
                Apellidos = objeto.Apellidos,
                Telefono = objeto.Telefono,
                Password = _utilidades.encriptarSHA256(objeto.Password),
                Email = objeto.Email,
                FechaNacimiento = objeto.FechaNacimiento,
                Direccion = objeto.Direccion,
                Estado = string.IsNullOrEmpty(objeto.Estado) ? "A" : objeto.Estado

            };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            if (usuario.Id != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, value = usuario });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }

        }

        // GET: api/v1/users/{id_user}
        [HttpGet]
        [Route("{id_user}")]
        [Authorize]

        public async Task<IActionResult> GetUser(int id_user)
        {
            var usuario = await _context.Usuarios.FindAsync(id_user);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });

            return Ok(new { value = usuario });
        }

        [HttpPut]
        [Route("{id_user}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id_user, UsuarioDTO objeto)
        {
            var usuario = await _context.Usuarios.FindAsync(id_user);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });

            usuario.Nombres = objeto.Nombres;
            usuario.Apellidos = objeto.Apellidos;
            usuario.Telefono = objeto.Telefono;
            usuario.Email = objeto.Email;
            usuario.FechaNacimiento = objeto.FechaNacimiento;
            usuario.Direccion = objeto.Direccion;

            // Solo actualizar la contraseña si viene en el DTO
            if (!string.IsNullOrEmpty(objeto.Password))
            {
                usuario.Password = _utilidades.encriptarSHA256(objeto.Password);
            }

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { isSuccess = true, value = usuario });
        }

        
        [HttpDelete]
        [Route("{id_user}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id_user)
        {
            var usuario = await _context.Usuarios.FindAsync(id_user);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado" });

            usuario.Estado = "I";

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                isSuccess = true,
                message = "Usuario desactivado correctamente",
                value = usuario
            });
        }




    }



}
