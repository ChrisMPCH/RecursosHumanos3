using OfficeOpenXml;
using RecursosHumanos.View;

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

            //Activa los estilos visuales modernos de Windows
            // Sin esta l�nea, los controles como botones,
            // cuadros de texto y pesta�as se ver�n con el estilo antiguo de Windows 95/98.
            Application.EnableVisualStyles();
            //Se usa para evitar que los controles usen el motor de renderizado de texto
            //antiguo de .NET 1.0, que era menos eficiente.
            Application.SetCompatibleTextRenderingDefault(false);

            //Inicia la aplicaci�n con la ventana de inicio

            frmLogin login_form = new frmLogin(); 
            if (login_form.ShowDialog() == DialogResult.OK) //El show dialog espera una respuesta
            {
                Application.Run(new MDIRecursosHumanos());
            }
        }
    }
}