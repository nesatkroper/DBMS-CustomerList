using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormCustomer
{
    public partial class FormCustomerExample1 : Form
    {
        public FormCustomerExample1()
        {
            InitializeComponent();
        }

        Consts consts = new Consts();
        SqlConnection conn;
        DataTable dtList = new DataTable();
        BindingSource bsList = new BindingSource();

        void LoadData_()
        {
            string sql = "select * from Customer_TB;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dgvData.Rows.Clear();
            while (dr.Read()) dgvData.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
            dr.Close();
            //BindControls_();
        }

        bool Dovalidation_()
        {
            bool result = true;
            if (txtName.Text == "")
            {
                epName.SetError(txtName, "Required");
                result = false;
            }
            if (txtCom.Text == "")
            {
                epCom.SetError(txtCom, "Required");
                result = false;
            }
            return result;
        }

        void BindControls_()
        {
            bsList.DataSource = dtList;
            txtId.DataBindings.Clear();
            txtId.DataBindings.Add(new Binding("Text", bsList, "cus_id"));
            txtName.DataBindings.Clear();
            txtName.DataBindings.Add(new Binding("Text", bsList, "cus_name"));
            txtCom.DataBindings.Clear();
            txtCom.DataBindings.Add(new Binding("Text", bsList, "cus_company"));
            txtPhone.DataBindings.Clear();
            txtPhone.DataBindings.Add(new Binding("Text", bsList, "cus_phone"));
            txtEmail.DataBindings.Clear();
            txtEmail.DataBindings.Add(new Binding("Text", bsList, "cus_email"));
            txtAddress.DataBindings.Clear();
            txtAddress.DataBindings.Add(new Binding("Text", bsList, "cus_address"));

        }

        private void FormCustomerExample1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(consts.connString);
            conn.Open();
            LoadData_();

            bsList.AddNew();
            btnSave.Enabled = !btnSave.Enabled;
            btnCancel.Enabled = !btnCancel.Enabled;
            btnUpdate.Enabled = !btnUpdate.Enabled;
            btnEdit.Enabled = !btnEdit.Enabled;
            btnDelete.Enabled = !btnDelete.Enabled;
            txtName.ReadOnly = !txtName.ReadOnly;
            txtCom.ReadOnly = !txtCom.ReadOnly;
            txtPhone.ReadOnly = !txtPhone.ReadOnly;
            txtEmail.ReadOnly = !txtEmail.ReadOnly;
            txtAddress.ReadOnly = !txtAddress.ReadOnly;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bsList.MoveFirst();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            bsList.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bsList.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bsList.MoveLast();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bsList.AddNew();
            btnNew.Enabled = !btnNew.Enabled;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = !btnSave.Enabled;
            btnCancel.Enabled = !btnCancel.Enabled;
            txtName.ReadOnly = !txtName.ReadOnly;
            txtCom.ReadOnly = !txtCom.ReadOnly;
            txtPhone.ReadOnly = !txtPhone.ReadOnly;
            txtEmail.ReadOnly = !txtEmail.ReadOnly;
            txtAddress.ReadOnly = !txtAddress.ReadOnly;
            txtId.ReadOnly = !txtId.ReadOnly;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Dovalidation_()) return;
                string sql = "insert into Customer_TB (cus_name, cus_company, cus_phone, cus_email, cus_address) values (@name, @com, @phone, @email, @add);";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@com", txtCom.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@add", txtAddress.Text);
                cmd.ExecuteNonQuery();
                LoadData_();
                //conn.Close();

                btnNew.Enabled = !btnNew.Enabled;
                btnSave.Enabled = !btnSave.Enabled;
                btnEdit.Enabled = !btnEdit.Enabled;
                btnDelete.Enabled = !btnDelete.Enabled;
                btnCancel.Enabled = !btnCancel.Enabled;
                txtName.ReadOnly = !txtName.ReadOnly;
                txtCom.ReadOnly = !txtCom.ReadOnly;
                txtPhone.ReadOnly = !txtPhone.ReadOnly;
                txtEmail.ReadOnly = !txtEmail.ReadOnly;
                txtAddress.ReadOnly = !txtAddress.ReadOnly;

                txtId.Clear();
                txtName.Clear();
                txtCom.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                txtAddress.Clear();
            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnNew.Enabled = !btnNew.Enabled;
            btnUpdate.Enabled = !btnUpdate.Enabled;
            btnEdit.Enabled = !btnEdit.Enabled;
            btnDelete.Enabled = !btnDelete.Enabled;
            btnCancel.Enabled = !btnCancel.Enabled;
            txtName.ReadOnly = !txtName.ReadOnly;
            txtCom.ReadOnly = !txtCom.ReadOnly;
            txtPhone.ReadOnly = !txtPhone.ReadOnly;
            txtEmail.ReadOnly = !txtEmail.ReadOnly;
            txtAddress.ReadOnly = !txtAddress.ReadOnly;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Dovalidation_()) return;
                string sql = "update Customer_TB set cus_name = @name, cus_company = @com, cus_phone = @phone, cus_email = @email, cus_address = @add where cus_id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@com", txtCom.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@add", txtAddress.Text);
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Updated.", "Note...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData_();
                //conn.Close();

                btnNew.Enabled = !btnNew.Enabled;
                btnEdit.Enabled = !btnEdit.Enabled;
                btnUpdate.Enabled = !btnUpdate.Enabled;
                btnDelete.Enabled = !btnDelete.Enabled;
                btnCancel.Enabled = !btnCancel.Enabled;
                txtName.ReadOnly = !txtName.ReadOnly;
                txtCom.ReadOnly = !txtCom.ReadOnly;
                txtPhone.ReadOnly = !txtPhone.ReadOnly;
                txtEmail.ReadOnly = !txtEmail.ReadOnly;
                txtAddress.ReadOnly = !txtAddress.ReadOnly;
            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete this record?", "Confirmation...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string sql = "delete from Customer_TB where cus_id = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtId.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted.", "Note...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData_();
                }
                else return;
            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bsList.CancelEdit();
            btnNew.Enabled = !btnNew.Enabled;
            btnSave.Enabled = false;
            btnEdit.Enabled = !btnEdit.Enabled;
            btnDelete.Enabled = !btnDelete.Enabled;
            btnCancel.Enabled = !btnCancel.Enabled;
            btnUpdate.Enabled = false;
            txtName.ReadOnly = !txtName.ReadOnly;
            txtCom.ReadOnly = !txtCom.ReadOnly;
            txtPhone.ReadOnly = !txtPhone.ReadOnly;
            txtEmail.ReadOnly = !txtEmail.ReadOnly;
            txtAddress.ReadOnly = !txtAddress.ReadOnly;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            epName.Clear();
        }

        private void txtCom_TextChanged(object sender, EventArgs e)
        {
            epCom.Clear();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                txtId.Text = dgvData.SelectedRows[0].Cells[0].Value.ToString();
                txtName.Text = dgvData.SelectedRows[0].Cells[1].Value.ToString();
                txtCom.Text = dgvData.SelectedRows[0].Cells[2].Value.ToString();
                txtPhone.Text = dgvData.SelectedRows[0].Cells[3].Value.ToString();
                txtEmail.Text = dgvData.SelectedRows[0].Cells[4].Value.ToString();
                txtAddress.Text = dgvData.SelectedRows[0].Cells[5].Value.ToString();
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else return;
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }
    }
}
