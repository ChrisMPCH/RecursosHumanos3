using RecursosHumanosCore.Model;

public class Empleado
{
    public int Id_Empleado { get; set; }
    public int Id_Persona { get; set; }
    public DateTime Fecha_Ingreso { get; set; }
    public DateTime? Fecha_Baja { get; set; }
    public int Id_Departamento { get; set; }
    public int Id_Puesto { get; set; }
    public string Matricula { get; set; }
    public string Motivo { get; set; }
    public short Estatus { get; set; }

    // Propiedades adicionales para los detalles
    public Persona DatosPersonales { get; set; }
    public string Nombre { get; set; }
    public string Departamento { get; set; } // Ahora propiedad pública
    public string Puesto { get; set; } // Ahora propiedad pública
    public string EstatusTexto { get; set; } // Para mostrar "Activo" o "Inactivo"

    public Empleado()
    {
        Matricula = string.Empty;
        Motivo = string.Empty;
        Fecha_Ingreso = DateTime.Now;
        Estatus = 1;
        DatosPersonales = new Persona();
        Nombre = string.Empty;
        Departamento = string.Empty;
        Puesto = string.Empty;
        EstatusTexto = "Activo"; // Establecer valor predeterminado
    }

    // Constructor con parámetros
    public Empleado(string matricula, string motivo, Persona datosPersonales)
    {
        Matricula = matricula;
        Motivo = motivo;
        Fecha_Ingreso = DateTime.Now;
        Estatus = 1;
        DatosPersonales = datosPersonales;

        // Aquí se asigna el nombre del empleado concatenando los valores de la clase Persona
        Nombre = $"{datosPersonales.Nombre} {datosPersonales.Ap_Paterno} {datosPersonales.Ap_Materno}";

        Departamento = string.Empty;
        Puesto = string.Empty;
        EstatusTexto = "Activo";
    }
}
