
using API.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class TeacherMenu : Form
    {
        private Button botonActivo;
        private Color colorBase = Color.FromArgb(240, 240, 240);
        private Color colorActivo = Color.SteelBlue;
        public TeacherMenu()
        {
            InitializeComponent();
        }

        private void OpenOtherForm(Form secondaryForm)
        {
            primaryPanel.Controls.Clear();
            secondaryForm.TopLevel = false;
            secondaryForm.FormBorderStyle = FormBorderStyle.None;
            secondaryForm.Dock = DockStyle.Fill;
            primaryPanel.Controls.Add(secondaryForm);
            secondaryForm.Show();
        }

        private void ActivarBoton(Button boton, string titulo)
        {
            if (botonActivo != null)
            {
                botonActivo.BackColor = colorBase;
                botonActivo.ForeColor = Color.Black;
            }

            boton.BackColor = colorActivo;
            boton.ForeColor = Color.White;
            botonActivo = boton;

            this.Text = titulo;
        }

        private async void Logout(object sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            await authService.LogoutAsync();

            Application.Restart();
        }

        private async void notasButton_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Notas de Usuarios");

            int profesorId = 0;
            try
            {
                var auth = AuthServiceProvider.Instance;
                var current = await auth.GetCurrentUserAsync();
                if (current != null)
                    profesorId = current.Id;
            }
            catch
            {
               
            }

            OpenOtherForm(new NotasList(profesorId));
        }

        private async void btnProfile_Click(object sender, EventArgs e)
        {
            var auth = AuthServiceProvider.Instance;
            var current = await auth.GetCurrentUserAsync();
            if (current == null)
            {
                MessageBox.Show("No se pudo determinar el usuario actual.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ActivarBoton((Button)sender, "Profile Profesor");
            OpenOtherForm(new UserProfile(current.Id));
        }
    }
}
