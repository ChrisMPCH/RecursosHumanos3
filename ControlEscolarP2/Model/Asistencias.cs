namespace RecursosHumanos.Models
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public DateTime FechaAsistencia { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public bool HorarioCompleto { get; set; }
        public string Observaciones { get; set; }
        public int HorasExtra { get; set; }
        public int IdEmpleado { get; set; }
        public short Estatus { get; set; }
    }
}
