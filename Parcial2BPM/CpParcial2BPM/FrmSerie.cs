using CadParcial2BPM;
using ClnParcial2BPM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpParcial2BPM
{
    public partial class FrmSerie : Form
    {
        bool esNuevo = false;
        public FrmSerie()
        {
            InitializeComponent();
        }
        private void listar()
        {
            var serie = SerieCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = serie;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["titulo"].HeaderText = "Título";
            dgvLista.Columns["sinopsis"].HeaderText = "Sinopsis";
            dgvLista.Columns["director"].HeaderText = "Director";
            dgvLista.Columns["duracion"].HeaderText = "Duración en segundos";
            dgvLista.Columns["fechaEstreno"].HeaderText = "Fecha de Estreno";
            btnEditar.Enabled = serie.Count > 0;
            btnEliminar.Enabled = serie.Count > 0;
            if (serie.Count > 0) dgvLista.Rows[0].Cells["titulo"].Selected = true;
        }

        private void FrmSerie_Load(object sender, EventArgs e)
        {
            Size = new Size(830, 338);
            listar();
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            Size = new Size(830, 462);
            esNuevo = true;
            txtTituloName.Focus();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Size = new Size(830, 462);
            esNuevo = false;

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var serie = SerieCln.get(id);
            txtTituloName.Text = serie.titulo;
            txtSinopsis.Text = serie.sinopsis;
            txtDirector.Text = serie.director;
            txtDuracion.Text = serie.duracion.ToString();
            dtpFechaEstreno.Value = serie.fechaEstreno;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(830, 338);
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }
        private bool validar()
        {
            bool esValido = true;
            erpTituloName.SetError(txtTituloName, "");
            erpSinopsis.SetError(txtSinopsis, "");
            erpDirector.SetError(txtDirector, "");
            erpDuracion.SetError(txtDuracion, "");
            erpFechaEstreno.SetError(dtpFechaEstreno, "");
            if (string.IsNullOrEmpty(txtTituloName.Text))
            {
                esValido = false;
                erpTituloName.SetError(txtTituloName, "El campo Titulo es obligatorio");
            }
            if (string.IsNullOrEmpty(txtSinopsis.Text))
            {
                esValido = false;
                erpSinopsis.SetError(txtSinopsis, "El campo Sinopsis es obligatorio");
            }
            if (string.IsNullOrEmpty(txtDirector.Text))
            {
                esValido = false;
                erpDirector.SetError(txtDirector, "El campo Director es obligatorio");
            }
            if (string.IsNullOrEmpty(txtDuracion.Text))
            {
                esValido = false;
                erpDuracion.SetError(txtDuracion, "El campo Duración es obligatorio");
            }
            if (string.IsNullOrEmpty(dtpFechaEstreno.Text))
            {
                esValido = false;
                erpFechaEstreno.SetError(dtpFechaEstreno, "El campo Fecha de Estreno es obligatorio");
            }
            return esValido;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var serie = new Serie();
            serie.titulo = txtTituloName.Text.Trim();
            serie.sinopsis = txtSinopsis.Text.Trim();
            serie.director = txtDirector.Text;
            serie.duracion = int.Parse(txtDuracion.Text);
            serie.fechaEstreno = dtpFechaEstreno.Value;
            if (esNuevo)
            {
                serie.estado = 1;
                SerieCln.insertar(serie);
            }
            else
            {
                int index = dgvLista.CurrentCell.RowIndex;
                serie.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                SerieCln.actualizar(serie);
            }
            listar();
            btnCancelar.PerformClick();
            MessageBox.Show("Serie guardada correctamente", "::: 2do Parcial Practico - Mensaje::: ",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void limpiar()
        {
            txtTituloName.Text = string.Empty;
            txtSinopsis.Text = string.Empty;
            txtDirector.Text = string.Empty;
            txtDuracion.Text = string.Empty;
            dtpFechaEstreno.Value = DateTime.Now;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            {
                int index = dgvLista.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                string titulo = dgvLista.Rows[index].Cells["titulo"].Value.ToString();
                DialogResult dialog = MessageBox.Show($"¿Está seguro que desea eliminar la serie {titulo}?",
                    "::: 2do Parcial Practico  - Mensaje :::", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    SerieCln.eliminar(id, "SIS457");
                    listar();
                    MessageBox.Show("Serie eliminada correctamente", "::: 2do Parcial Practico  - Mensaje :::",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }


    }
}
