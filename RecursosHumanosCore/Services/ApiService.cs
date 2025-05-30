using RecursosHumanosCore.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        // Obtener la URL desde el archivo  configuración
        private readonly string _baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] ?? throw new InvalidOperationException("La clave 'ApiBaseUrl' no está configurada en AppSettings.");

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<List<NominaDto>> ObtenerNominasPorEmpleadoAsync(string matricula)
        {
            if (string.IsNullOrEmpty(matricula))
                throw new ArgumentException("La matrícula es requerida", nameof(matricula));

            string url = $"{_baseUrl}nominas/empleado/{matricula}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var nominas = await response.Content.ReadFromJsonAsync<List<NominaDto>>();
                    return nominas ?? new List<NominaDto>();
                }
                else
                {
                    // Manejar errores, opcionalmente lanzar excepción o devolver lista vacía
                    return new List<NominaDto>();
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error o manejarlo según convenga
                throw new ApplicationException("Error al llamar a la API de nóminas", ex);
            }
        }
    }
}
    
