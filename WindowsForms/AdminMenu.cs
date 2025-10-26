using API.Clients;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class AdminMenu : Form
    {
        private Button botonActivo; 
        private Color colorBase = Color.FromArgb(240, 240, 240); 
        private Color colorActivo = Color.SteelBlue; 

        public AdminMenu()
        {
            InitializeComponent();
            this.Text = "Menú principal"; 
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

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Listado de Usuarios");
            OpenOtherForm(new UserList());
        }

        private void BtnMaterias_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Listado de Materias");
            OpenOtherForm(new SubjectList());
        }

        private void BtnPlanes_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Listado de Planes");
            OpenOtherForm(new PlanesList());
        }

        private void BtnEspecialidades_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Listado de Especialidades");
            OpenOtherForm(new EspecialidadesList());
        }

        private void BtnCursos_Click(object sender, EventArgs e)
        {
            ActivarBoton((Button)sender, "Listado de Cursos");
            OpenOtherForm(new CursosList());
        }

        private async void Logout(object sender, EventArgs e)
        {
            var authService = AuthServiceProvider.Instance;
            await authService.LogoutAsync();
            Application.Restart();
        }
    }
}
