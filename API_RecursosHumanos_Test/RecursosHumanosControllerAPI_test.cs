using Microsoft.AspNetCore.Mvc;
using RecursosHumanosCore.Controller;
using Microsoft.AspNetCore.Mvc;
using RecursosHumanosCore.Model;


namespace API_RecursosHumanos_Test
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursosHumanosControllerAPI_test: ControllerBase
    {
        
            private readonly UsuariosController _usuariosController;
            private readonly ILogger<RecursosHumanosControllerAPI_test> _logger;

            public RecursosHumanosControllerAPI_test(UsuariosController usuariosController, ILogger<RecursosHumanosControllerAPI_test> logger)
            {
                _usuariosController = usuariosController;
                _logger = logger;
            }

            /// <summary>
            /// Obtiene todos los usuarios activos (con sus roles y datos personales)
            /// </summary>
            /// <returns>Lista de usuarios</returns>
            [HttpGet("list_usuarios")]
            public IActionResult GetUsuarios()
            {
                try
                {
                    var usuarios = _usuariosController.ObtenerUsuarios();
                    return Ok(usuarios);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener usuarios");
                    return StatusCode(500, "Error interno del servidor: " + ex.Message);
                }
            }

            /// <summary>
            /// Actualiza el nombre de usuario de un usuario específico
            /// </summary>
        [HttpPut("actualizar_usuario")]
        public IActionResult ActualizarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                bool exito = _usuariosController.ActualizarUsuario(usuario);

                if (exito)
                    return Ok(new { mensaje = "Usuario actualizado correctamente." });
                else
                    return BadRequest(new { mensaje = "No se pudo actualizar el usuario." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar usuario");
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
