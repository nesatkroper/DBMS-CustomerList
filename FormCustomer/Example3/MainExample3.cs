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

namespace FormCustomer.Example3
{
    public partial class MainExample3 : Form
    {
        public MainExample3()
        {
            InitializeComponent();
        }

        Consts consts = new Consts();
        NewForm newForm = new NewForm();
        EditForm editForm = new EditForm();
        DataTable dt = new DataTable();
        SqlConnection conn;

        string id;

        void LoadData_()
        {
            conn.Open();
            string sql = "select cus_id, cus_name from Customer_TB";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            conn.Close();
            dgvData.DataSource = dt;
            dgvData.Columns[0].Visible = false;
            dgvData.Columns[1].HeaderText = "Customer Name";
            dgvData.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void MainExample3_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(consts.connString);
            LoadData_();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (newForm.ShowDialog() == DialogResult.OK) LoadData_();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            editForm.id = this.id;
            if (editForm.ShowDialog() == DialogResult.OK) LoadData_();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (MessageBox.Show("Are you sure to delete this record?", "Confirmation...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "delete from Customer_TB where cus_id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer deleted.", "Note...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                LoadData_();
            }
            else return;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            conn.Open();
            if (dgvData.SelectedRows.Count > 0)
            {
                id = dgvData.SelectedRows[0].Cells[0].Value.ToString();
                string sql = "select * from Customer_TB where cus_id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtName.Text = dr[1].ToString();
                    txtCom.Text = dr[2].ToString();
                    txtPhone.Text = dr[3].ToString();
                    txtEmail.Text = dr[4].ToString();
                    txtAddress.Text = dr[5].ToString();
                }
            }
            conn.Close();
        }
    }
}
