using OfficeOpenXml;
using RecursosHumanos.View;
using RecursosHumanosCore.Data;
using System.Configuration; // Para ConfigurationManager


namespace RecursosHumanos
{
    internal static class Program
    {
        /// <summary>
        /// STA (Single-Threaded Apartment) es un modelo de subprocesos (threads) 
        /// usado en COM (Component Object Model)
        /// Clipboard.SetText() (portapapeles)
        ///OpenFileDialog / SaveFileDialog
        /// </summary>

        [STAThread]
        static void Main()
        {
            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Asignar la cadena de conexión aquí:
            PostgreSQLDataAccess.ConnectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

            frmLogin login_form = new frmLogin();
            if (login_form.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MDIRecursosHumanos());
            }
        }

}
}