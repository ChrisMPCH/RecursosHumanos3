using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using RecursosHumanos.Model;

namespace RecursosHumanos.Data
{
    public static class EmpleadosDataAccess
    {
        public static List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();
            string query = "SELECT id, nombre, apellido, puesto, departamento, estatus FROM empleados WHERE estatus != 'Baja'";

            try
            {
                var db = PostgreSQLDataAccess.GetInstance();

                if (!db.Connect())
                {
                    throw new Exception("No se pudo conectar a la base de datos.");
                }

                DataTable result = db.ExecuteQuery_Reader(query);

                foreach (DataRow row in result.Rows)
                {
                    lista.Add(new Empleado
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Nombre = row["nombre"].ToString(),
                        Apellido = row["apellido"].ToString(),
                        Puesto = row["puesto"].ToString(),
                        Departamento = row["departamento"].ToString(),
                        Estatus = row["estatus"].ToString()
                    });
                }

                db.Disconnect();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener empleados", ex);
            }

            return lista;
        }
    }
}
