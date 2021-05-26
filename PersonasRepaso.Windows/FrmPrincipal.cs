using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PersonasRepaso.Datos;
using PersonasRepaso.Entidades;

namespace PersonasRepaso.Windows
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private RepositorioPersonas repositorio;
        private List<Persona> lista;
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            repositorio = new RepositorioPersonas();

            InicializarGrilla();
        }

        private void InicializarGrilla()
        {
            lista = repositorio.GetLista();
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            DatosDataGridView.Rows.Clear();
            foreach (var persona in lista)
            {
                DataGridViewRow r = ConstruirFila();
                SetearFila(r, persona);
                AgregarFila(r);
            }
        }

        private void AgregarFila(DataGridViewRow r)
        {
            DatosDataGridView.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Persona persona)
        {
            r.Cells[colApellido.Index].Value = persona.Apellido;
            r.Cells[colNombres.Index].Value = persona.Nombres;
            r.Cells[colDni.Index].Value = persona.DNI;
            r.Cells[colFechaNac.Index].Value = persona.FechaDeNacimiento.ToShortDateString();
            r.Cells[colEdad.Index].Value = persona.InformarEdad();

            r.Tag = persona;
        }

        private DataGridViewRow ConstruirFila()
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(DatosDataGridView);
            return r;
        }

        private void SalirToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            FrmPersonaAE frm = new FrmPersonaAE()
            {
                Text = "Agregar Persona"
            };
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.OK)
            {
                //TODO: Guardar los datos
                try
                {
                    Persona persona = frm.GetPersona();
                    repositorio.Agregar(persona);
                    DataGridViewRow r = ConstruirFila();
                    SetearFila(r,persona);
                    AgregarFila(r);
                    MessageBox.Show("Registro agregado", "Mensaje", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private void BorrarToolStripButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count==0)
            {
                return;
            }

            DataGridViewRow r = DatosDataGridView.SelectedRows[0];
            Persona personaBorrar =(Persona) r.Tag;
            DialogResult dr = MessageBox.Show($"¿Desea borrar a {personaBorrar.ToString()}?",
                "Confirmar Baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (dr==DialogResult.No)
            {
                return;
            }

            try
            {
                repositorio.Borrar(personaBorrar);
                DatosDataGridView.Rows.Remove(r);
                MessageBox.Show("Registro Borrado", "Mensaje");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void EditarToolStripButton_Click(object sender, EventArgs e)
        {
            if (DatosDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow r = DatosDataGridView.SelectedRows[0];
            Persona personaEditar = (Persona)r.Tag;
            FrmPersonaAE frm = new FrmPersonaAE {Text = "Modificar Datos"};
            frm.SetPersona(personaEditar);
            DialogResult dr = frm.ShowDialog(this);
            if (dr==DialogResult.Cancel)
            {
                return;
            }

            try
            {
                repositorio.Editar(personaEditar);
                SetearFila(r,personaEditar);
                MessageBox.Show("Registro Editado", "Mensaje");

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }

        }

        private void OrdenarToolStripButton_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetListaOrdenada();
            MostrarDatosEnGrilla();
        }

        private void FiltrarToolStripButton_Click(object sender, EventArgs e)
        {
            lista = repositorio.GetListaFiltrada(Sexo.Femenino);
            MostrarDatosEnGrilla();
        }

        private void ActualizarToolStripButton_Click(object sender, EventArgs e)
        {
            InicializarGrilla();
        }
    }
}
