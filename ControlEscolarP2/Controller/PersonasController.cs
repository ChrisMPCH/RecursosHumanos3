using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursosHumanos.Data;
using RecursosHumanos.Model;
using NLog;
using System;
using System.Collections.Generic;
using RecursosHumanos.Utilities;

namespace RecursosHumanos.Controller
{
    public class PersonasController
    {
        private readonly PersonasDataAccess _personasAccess;
        private static readonly Logger _logger = LoggingManager.GetLogger("RecursosHumanos.Controller.PersonasController");

        public PersonasController()
        {
            _personasAccess = new PersonasDataAccess();
        }
        /// <summary>
        /// Registra una nueva persona en la base de datos.
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        public (bool exito, string mensaje, int idPersona) RegistrarPersona(Persona persona)
        {
            try
            {
                if (_personasAccess.ExisteRFC(persona.RFC))
                    return (false, "El RFC ya está registrado.", -2);

                if (_personasAccess.ExisteCURP(persona.CURP))
                    return (false, "La CURP ya está registrada.", -3);

                if (_personasAccess.ExisteTelefono(persona.Telefono))
                    return (false, "El teléfono ya está registrado.", -4);

                if (_personasAccess.ExisteEmail(persona.Email))
                    return (false, "El correo ya está registrado.", -5);

                int idPersona = _personasAccess.InsertarPersona(persona);

                if (idPersona <= 0)
                    return (false, "Error al registrar la persona en la base de datos.", -1);

                return (true, "Persona registrada correctamente.", idPersona);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar persona");
                return (false, "Error inesperado. Contacte al administrador.", -99);
            }
        }
        /// <summary>
        /// Elimina el registro de una persona si no tiene relaciones en la base de datos.
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns></returns>
        public bool CancelarRegistroPersona(int idPersona)
        {
            try
            {
                return _personasAccess.EliminarPersonaSiNoTieneRelaciones(idPersona);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al cancelar registro de persona");
                return false;
            }
        }

    }
}
