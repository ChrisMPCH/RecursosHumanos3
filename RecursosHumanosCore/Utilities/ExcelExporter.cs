using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RecursosHumanosCore.Utilities
{
    public static class ExcelExporter
    {
        public static bool ExportToExcel<T>(
            IEnumerable<T> data,
            string rutaArchivo,
            string nombreHoja,
            Func<T, bool> filtro)
        {
            if (data == null || !data.Any())
                return false;

            // Aplicar filtro si se proporciona
            var datosFiltrados = filtro != null ? data.Where(filtro).ToList() : data.ToList();

            if (!datosFiltrados.Any())
                return false;

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(nombreHoja);
                    
                    // Obtener propiedades del tipo
                    var propiedades = typeof(T).GetProperties();

                    // Encabezados
                    for (int col = 0; col < propiedades.Length; col++)
                    {
                        var headerCell = worksheet.Cells[2, col + 1];
                        headerCell.Value = propiedades[col].Name;
                        headerCell.Style.Font.Bold = true;
                        headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerCell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        headerCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        
                        // Aplicar formato específico según el tipo de columna
                        if (propiedades[col].Name == "Genero")
                        {
                            headerCell.Value = "Género";
                        }
                        else if (propiedades[col].Name.Contains("Fecha"))
                        {
                            headerCell.Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                    }

                    // Datos
                    for (int row = 0; row < datosFiltrados.Count; row++)
                    {
                        var item = datosFiltrados[row];
                        for (int col = 0; col < propiedades.Length; col++)
                        {
                            var cell = worksheet.Cells[row + 3, col + 1];
                            var valor = propiedades[col].GetValue(item, null);
                            
                            // Aplicar formato específico según el tipo de columna
                            if (propiedades[col].Name == "Genero")
                            {
                                cell.Value = valor?.ToString() ?? "No especificado";
                            }
                            else if (propiedades[col].Name.Contains("Fecha"))
                            {
                                if (valor is DateTime fecha)
                                {
                                    cell.Value = fecha;
                                    cell.Style.Numberformat.Format = "dd/mm/yyyy";
                                }
                            }
                            else
                            {
                                cell.Value = valor;
                            }

                            // Aplicar bordes a todas las celdas
                            cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    // Ajustar ancho de columnas
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Guardar el archivo
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

        // Método sobrecargado para tipos anónimos
        public static bool ExportToExcel(
            IEnumerable<object> data,
            string rutaArchivo,
            string nombreHoja,
            Func<object, bool> filtro = null)
        {
            if (data == null || !data.Any())
                return false;

            // Aplicar filtro si se proporciona
            var datosFiltrados = filtro != null ? data.Where(filtro).ToList() : data.ToList();

            if (!datosFiltrados.Any())
                return false;

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(nombreHoja);
                    
                    // Obtener propiedades del primer elemento
                    var propiedades = datosFiltrados.First().GetType().GetProperties();

                    // Encabezados
                    for (int col = 0; col < propiedades.Length; col++)
                    {
                        var headerCell = worksheet.Cells[2, col + 1];
                        headerCell.Value = propiedades[col].Name;
                        headerCell.Style.Font.Bold = true;
                        headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerCell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        headerCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        
                        // Aplicar formato específico según el tipo de columna
                        if (propiedades[col].Name == "Genero")
                        {
                            headerCell.Value = "Género";
                        }
                        else if (propiedades[col].Name.Contains("Fecha"))
                        {
                            headerCell.Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                    }

                    // Datos
                    for (int row = 0; row < datosFiltrados.Count; row++)
                    {
                        var item = datosFiltrados[row];
                        for (int col = 0; col < propiedades.Length; col++)
                        {
                            var cell = worksheet.Cells[row + 3, col + 1];
                            var valor = propiedades[col].GetValue(item, null);
                            
                            // Aplicar formato específico según el tipo de columna
                            if (propiedades[col].Name == "Genero")
                            {
                                cell.Value = valor?.ToString() ?? "No especificado";
                            }
                            else if (propiedades[col].Name.Contains("Fecha"))
                            {
                                if (valor is DateTime fecha)
                                {
                                    cell.Value = fecha;
                                    cell.Style.Numberformat.Format = "dd/mm/yyyy";
                                }
                            }
                            else
                            {
                                cell.Value = valor;
                            }

                            // Aplicar bordes a todas las celdas
                            cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    // Ajustar ancho de columnas
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Guardar el archivo
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
