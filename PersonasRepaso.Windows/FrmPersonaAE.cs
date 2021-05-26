using System;
using System.Windows.Forms;
using PersonasRepaso.Entidades;

namespace PersonasRepaso.Windows
{
    public partial class FrmPersonaAE : Form
    {
        public FrmPersonaAE()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private Persona persona;
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (persona==null)
                {
                    persona = new Persona();
                }

                persona.Apellido = ApellidoTextBox.Text;
                persona.Nombres = NombresTextBox.Text;
                persona.DNI = DniTextBox.Text;
                if (FemeninoRadioButton.Checked)
                {
                    persona.Sexo = Sexo.Femenino;
                }
                else
                {
                    persona.Sexo = Sexo.Masculino;
                }

                persona.FechaDeNacimiento = FechaNacDateTimePicker.Value;

                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            return valido;
        }

        public Persona GetPersona()
        {
            return persona;
        }

        public void SetPersona(Persona personaEditar)
        {
            persona = personaEditar;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (persona != null)
            {
                ApellidoTextBox.Text = persona.Apellido;
                NombresTextBox.Text = persona.Nombres;
                DniTextBox.Text = persona.DNI;
                FechaNacDateTimePicker.Value = persona.FechaDeNacimiento;
                if (persona.Sexo == Sexo.Femenino)
                {
                    FemeninoRadioButton.Checked = true;
                }
                else
                {
                    MasculinoRadioButton.Checked = true;
                }

                DniTextBox.Enabled = false;
            }
        }
    }
}
