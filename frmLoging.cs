using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice
{
    public partial class frmLoging : Form
    {
        DB db = new DB();
        public frmLoging()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT [user_id]
 
  FROM [dbo].[Users] where [user_name]=N'{0}' and [user_password]=N'{1}'";
            string user_id =db.excuteSql(string.Format(sql,txtUsername.Text,txtPassword.Text));
            if (user_id=="")
            {
                MessageBox.Show("عفوا,البيانات غير صحيحه");
            }
            else
            {
                   frmMain frm = new frmMain(user_id);
                frm.Show();
                this.Hide();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
