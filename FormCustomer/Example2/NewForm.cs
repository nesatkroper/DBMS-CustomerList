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
    public partial class NewForm : Form
    {
        public NewForm()
        {
            InitializeComponent();
        }

        Consts consts = new Consts();
        ErrorProvider epName = new ErrorProvider();
        SqlConnection conn;

        bool Dovalidation_()
        {
            bool result = true;
            if (txtName.Text.Trim() == "")
            {
                epName.SetError(txtName, "Required");
                result = false;
            }
            return result;
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(consts.connString);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (!Dovalidation_()) return;
            string sql = "insert into Customer_TB (cus_name, cus_company, cus_phone, cus_email, cus_address) values (@name, @com, @phone, @email, @add);";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@com", txtCom.Text);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@add", txtAddress.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
