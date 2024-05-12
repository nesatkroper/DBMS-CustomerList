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

namespace FormCustomer.Example2
{
    public partial class MainExample2 : Form
    {
        public MainExample2()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        Consts consts = new Consts();
        EditForm editForm = new EditForm();
        NewForm newForm = new NewForm();

        void LoadData_()
        {
            conn.Open();
            string sql = "select * from Customer_TB;";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dgvData.Rows.Clear();
            while (dr.Read()) dgvData.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
            dr.Close();
            conn.Close();
        }

        private void MainExample2_Load(object sender, EventArgs e)
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
            if (dgvData.SelectedRows.Count > 0)
            {
                editForm.id = dgvData.SelectedRows[0].Cells[0].Value.ToString();
                editForm.name = dgvData.SelectedRows[0].Cells[1].Value.ToString();
                editForm.com = dgvData.SelectedRows[0].Cells[2].Value.ToString();
                editForm.phone = dgvData.SelectedRows[0].Cells[3].Value.ToString();
                editForm.email = dgvData.SelectedRows[0].Cells[4].Value.ToString();
                editForm.address = dgvData.SelectedRows[0].Cells[5].Value.ToString();
                if (editForm.ShowDialog() == DialogResult.OK) LoadData_();
            }
            else
            {
                MessageBox.Show("Please Select Row.", "Note...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                conn.Open();
                string id = dgvData.SelectedRows[0].Cells[0].Value.ToString();
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
            else
            {
                MessageBox.Show("Please Select Row.", "Note...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
    }
}
