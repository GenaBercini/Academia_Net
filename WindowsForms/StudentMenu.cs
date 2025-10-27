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
    public partial class StudentMenu : Form
    {
        private Button botonActivo;
        private Color colorBase = Color.FromArgb(240, 240, 240);
        private Color colorActivo = Color.SteelBlue;
        public StudentMenu()
        {
            InitializeComponent();
        }
        //private void OpenOtherForm(Form secondaryForm)
        //{
        //    primaryPanel.Controls.Clear();
        //    secondaryForm.TopLevel = false;
        //    secondaryForm.FormBorderStyle = FormBorderStyle.None;
        //    secondaryForm.Dock = DockStyle.Fill;
        //    primaryPanel.Controls.Add(secondaryForm);
        //    secondaryForm.Show();
        //}

        private async void Logout(object sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            await authService.LogoutAsync();

            Application.Restart();
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
        private void inscribirButton_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Inscripcion a Materias");
            OpenOtherForm(new EnrollmentStudent());
        }
        private void materiasButton_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Materias Inscriptas");
            OpenOtherForm(new SubjectStudent());
        }
        private void profileButton_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Perfil del Estudiante");
            OpenOtherForm(new ProfileStudent());
        }
    }
}
