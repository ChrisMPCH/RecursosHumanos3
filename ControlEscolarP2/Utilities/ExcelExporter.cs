using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RecursosHumanos.Utilities
{
    public static class ExcelExporter
    {
        public static bool ExportToExcel<T>(
    List<T> data,
    string rutaArchivo,
    string nombreHoja,
    Func<T, bool> filtro = null
)
        {
            if (data == null || !data.Any())
                return false;

            // Aplicar filtro si se proporciona
            var datosFiltrados = filtro != null ? data.Where(filtro).ToList() : data;

            if (!datosFiltrados.Any())
                return false;

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(nombreHoja);
                    var propiedades = typeof(T).GetProperties();

                    // Encabezados
                    for (int col = 0; col < propiedades.Length; col++)
                    {
                        worksheet.Cells[2, col + 1].Value = propiedades[col].Name;
                        worksheet.Cells[2, col + 1].Style.Font.Bold = true;
                        worksheet.Cells[2, col + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, col + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells[2, col + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }

                    // Datos
                    for (int row = 0; row < datosFiltrados.Count; row++)
                    {
                        var item = datosFiltrados[row];
                        for (int col = 0; col < propiedades.Length; col++)
                        {
                            var valor = propiedades[col].GetValue(item, null);
                            worksheet.Cells[row + 3, col + 1].Value = valor;
                        }
                    }

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    var fileInfo = new FileInfo(rutaArchivo);
                    package.SaveAs(fileInfo);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al exportar a Excel: {ex.Message}");
                return false;
            }
        }
    }
}
