
namespace RecursosHumanos.Controller
{
    internal class AuditoriaExportDto
    {
        public int ID_Auditoria { get; set; }
        public string Tipo { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha_Movimiento { get; set; }
        public string IP_Equipo { get; set; }
        public string Nombre_Equipo { get; set; }
        public string Usuario_Responsable { get; set; }
        public string Detalle { get; set; }
        public string Estatus { get; set; }
    }
}