
namespace API_RecursosHumanos_Test
{
    internal class DiasTrabajadosDTO
    {
        public string Matricula { get; set; }
        public string NombreEmpleado { get; set; }
        public string EstatusEmpleado { get; set; }
        public string EstatusContrato { get; set; }
        public int DiasTrabajados { get; set; }
        public double Salario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}