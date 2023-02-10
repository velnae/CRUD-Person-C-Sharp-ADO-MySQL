using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using appdeskperson.BusinessLogic;

namespace appdeskperson.PresentationLayer
{
    public partial class FrmPerson : Form
    {
        private DataTable dtListado;
        private string Accion = string.Empty;

        struct ValorAccion
        {
            public const string NUEVO = "N";
            public const string MODIFICAR = "M";
            public const string ELIMINAR = "E";
            public const string GUARDAR = "G";
            public const string DESHACER = "D";
            public const string DEFAULT = "";
        }

        private void Exito()
        {
            MessageBox.Show(this, "Se guardaron los datos correctamente",
                                "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ListarCliente();
            Accion = string.Empty;
        }

        public FrmPerson()
        {
            InitializeComponent();
        }

        private void FrmPerson_Load(object sender, EventArgs e)
        {
            try
            {
                ListarCliente();
                AccionMantenimiento(ValorAccion.DEFAULT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void ListarCliente()
        {
            dtListado = BL_Person.ListPerson(txtSearch.Text.Trim());
            dgvPerson.DataSource = dtListado;

            if (dtListado.Rows.Count == 0)
            {
            }
        }


        private void AccionMantenimiento(string strAccion)
        {
            switch (strAccion)
            {
                case ValorAccion.NUEVO:
                    btnNewPerson.Enabled = false;
                    btnEditPerson.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    txtSearch.ReadOnly = true;
                    txtSearch.ResetText();
                    btnSearch.Enabled = false;
                    dgvPerson.Enabled = false;


                    txtDni.Enabled = true;
                    txtFirstName.Enabled = true;
                    txtSurName.Enabled = true;
                    dateBirthDate.Enabled = true;


                    txtIdPerson.ResetText();
                    txtDni.ResetText();
                    txtFirstName.ResetText();
                    txtSurName.ResetText();
                    dateBirthDate.ResetText();

                    break;
                case ValorAccion.MODIFICAR:
                    btnNewPerson.Enabled = false;
                    btnEditPerson.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    txtSearch.ReadOnly = true;
                    txtSearch.ResetText();
                    btnSearch.Enabled = false;
                    dgvPerson.Enabled = false;


                    txtDni.Enabled = true;
                    txtFirstName.Enabled = true;
                    txtSurName.Enabled = true;
                    dateBirthDate.Enabled = true;

                    break;

                case ValorAccion.DESHACER:
                    btnNewPerson.Enabled = true;
                    btnEditPerson.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    
                    txtSearch.ReadOnly = false;
                    txtSearch.ResetText();
                    btnSearch.Enabled = true;
                    dgvPerson.Enabled = true;


                    txtIdPerson.ResetText();
                    txtDni.ResetText();
                    txtFirstName.ResetText();
                    txtSurName.ResetText();
                    dateBirthDate.ResetText();

                    txtDni.Enabled = false;
                    txtFirstName.Enabled = false;
                    txtSurName.Enabled = false;
                    dateBirthDate.Enabled = false;

                    break;
                default:
                    btnNewPerson.Enabled = true;
                    btnEditPerson.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    
                    txtSearch.ReadOnly = false;
                    txtSearch.ResetText();
                    btnSearch.Enabled = true;
                    dgvPerson.Enabled = true;
                    break;
            }           
        }


        private void btnNewPerson_Click(object sender, EventArgs e)
        {
            Accion = ValorAccion.NUEVO;
            AccionMantenimiento(Accion);
            txtDni.Focus();
        } 

        private void btnEditPerson_Click(object sender, EventArgs e)
        {
            if (dgvPerson.Rows.Count == 0)
                return;

            Accion = ValorAccion.MODIFICAR;
            AccionMantenimiento(Accion);
            txtDni.Focus();


            Int32 selectedRowCount = dgvPerson.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount == 1)
            {
                txtIdPerson.Text = dgvPerson.SelectedRows[0].Cells[0].Value.ToString();
                txtDni.Text = dgvPerson.SelectedRows[0].Cells[1].Value.ToString();
                txtFirstName.Text = dgvPerson.SelectedRows[0].Cells[2].Value.ToString();
                txtSurName.Text = dgvPerson.SelectedRows[0].Cells[3].Value.ToString();
                string date = dgvPerson.SelectedRows[0].Cells[4].Value.ToString();
                dateBirthDate.Value = Convert.ToDateTime(date);

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Accion = ValorAccion.DESHACER;
            AccionMantenimiento(Accion);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = "¿Está seguro de guardar el registro?";

                if (MessageBox.Show(this, mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BE_Person Person = new BE_Person();
                    Person.idPerson = txtIdPerson.Text;
                    Person.dni = txtDni.Text.Trim();
                    Person.firstName = txtFirstName.Text.Trim();
                    Person.surName = txtSurName.Text.Trim();
                    Person.birthDate = (DateTime)dateBirthDate.Value;


                    if (Accion == ValorAccion.NUEVO)
                    {
                        Person.idPerson = BL_Person.GenerateIdPerson();
                        if (BL_Person.InsertPerson(Person))
                        {
                            Exito();
                        }
                    }

                    if (Accion == ValorAccion.MODIFICAR)
                    {
                        if (BL_Person.UpdatePerson(Person))
                        {
                            Exito();
                        }
                    }


                    AccionMantenimiento(ValorAccion.DESHACER);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPerson.Rows.Count == 0)
                return;

            string mensaje = "¿Está seguró de eliminar el registro?";
            if (MessageBox.Show(this, mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Int32 selectedRowCount = dgvPerson.Rows.GetRowCount(DataGridViewElementStates.Selected);
                    if (selectedRowCount == 1)
                    {
                        txtIdPerson.Text = dgvPerson.SelectedRows[0].Cells[0].Value.ToString();
                    }

                    if (BL_Person.DeletePerson(txtIdPerson.Text.Trim()))
                    Exito();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ListarCliente();
        }
    }
}
