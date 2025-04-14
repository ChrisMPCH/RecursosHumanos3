using Guna.UI2.WinForms;
using RecursosHumanos.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace RecursosHumanos.Utilities
{
    internal class Formas
    {
        public static void CloseOtherForms(Form currentForm)
        {
            List<Form> formsToClose = new List<Form>();
            foreach (Form childForm in Application.OpenForms)
            {
                if (childForm != currentForm)
                {
                    formsToClose.Add(childForm);
                }
            }
            foreach (Form form in formsToClose)
            {
                form.Close();
            }

        }

        public static void ConfigurarEstiloDataGridView(Guna2DataGridView dataGridViewStyle)
        {
            // Colores neutros
            dataGridViewStyle.BackgroundColor = Color.LightGray;
            dataGridViewStyle.GridColor = Color.DarkGray;
            dataGridViewStyle.DefaultCellStyle.BackColor = Color.White;
            dataGridViewStyle.DefaultCellStyle.ForeColor = Color.Black;

            // Tipografía más grande
            dataGridViewStyle.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            dataGridViewStyle.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            // Ajustar columnas para que se expandan con la ventana
            dataGridViewStyle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStyle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Color de los encabezados
            dataGridViewStyle.EnableHeadersVisualStyles = false;
            dataGridViewStyle.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridViewStyle.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Aplicar formato a la columna "Check" si existe
            foreach (DataGridViewColumn col in dataGridViewStyle.Columns)
            {
                if (col.Name.ToLower() == "check")
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Ancho fijo basado en el contenido
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // El resto de columnas se expanden dinámicamente
                    col.ReadOnly = true;  // Hacer que no sea editable
                }
            }
        }

        public static void ConfigurarTextBox(Guna2TextBox txtBox, string textoOriginal)
        {
            // Establece el texto original como placeholder al inicio
            txtBox.Text = textoOriginal;
            Color colorPlaceholder = Color.FromArgb(125, 137, 149);
            txtBox.ForeColor = colorPlaceholder;

            // Evento que se ejecuta cuando el usuario hace clic en el TextBox (obtiene el foco)
            txtBox.Enter += (s, e) =>
            {
                // Si el texto actual es el placeholder, lo borra para que el usuario pueda escribir
                if (txtBox.Text == textoOriginal)
                {
                    txtBox.Text = "";
                    txtBox.ForeColor = Color.Black; // Cambia el color al de texto normal
                }
            };

            // Evento que se ejecuta cuando el usuario deja de seleccionar el TextBox (pierde el foco)
            txtBox.Leave += (s, e) =>
            {
                // Si el usuario no escribió nada (o solo espacios en blanco), se restaura el placeholder
                if (string.IsNullOrWhiteSpace(txtBox.Text))
                {
                    txtBox.Text = textoOriginal;
                    txtBox.ForeColor = colorPlaceholder;
                }
            };
        }

        // Método para abrir un formulario dentro del panel
        public static void abrirPanelForm(Form childForm, Guna2GradientPanel pnlCambiante)
        {
            pnlCambiante.Controls.Clear();  // Limpia el contenido anterior
            childForm.TopLevel = false;  // Indica que es un formulario embebido
            childForm.Dock = DockStyle.Fill;  // Ajusta al tamaño del panel
            pnlCambiante.Controls.Add(childForm);  // Lo agrega al panel
            pnlCambiante.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public static void abrirChildForm(Form formChild)
        {
            if (MDIRecursosHumanos.formActivada != null)
            {
                MDIRecursosHumanos.formActivada.Close();
            }

            MDIRecursosHumanos.formActivada = formChild;

            formChild.TopLevel = false;
            formChild.FormBorderStyle = FormBorderStyle.None;
            formChild.Dock = DockStyle.Fill;
            formChild.BringToFront();
            formChild.Show();
        }

        public static void limpiarPanel(Panel panel)
        {
            panel.Controls.Clear(); // Elimina todos los controles del panel
            panel.Refresh(); // Refresca el panel para actualizar la vista
        }
    }
}
