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
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
        }

        public string id;


        SqlConnection conn;
        Consts consts = new Consts();
        ErrorProvider epName = new ErrorProvider();

        bool Dovalidation_()
        {
            bool result = true;
            if (txtName.Text == "")
            {
                epName.SetError(txtName, "Required");
                result = false;
            }
            return result;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(consts.connString);
            conn.Open();
            string sql = "select * from Customer_TB";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtName.Text = dr[1].ToString();
                txtCom.Text = dr[2].ToString();
                txtPhone.Text = dr[3].ToString();
                txtEmail.Text = dr[4].ToString();
                txtAddress.Text = dr[5].ToString();
            }
            conn.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (!Dovalidation_()) return;
            string sql = "update Customer_TB set cus_name = @name, cus_company = @com, cus_phone = @phone, cus_email = @email, cus_address = @add where cus_id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@com", txtCom.Text);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@add", txtAddress.Text);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Customer Updated.", "Note...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
