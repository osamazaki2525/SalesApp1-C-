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
    public partial class frmUsersShow : Form
    {
        DB db = new DB();
        string user_kind_id = "";
        
        public frmUsersShow(string user_kind)
        {
            InitializeComponent();
            user_kind_id = user_kind;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void frmUsersShow_Load(object sender, EventArgs e)
        {
            
            linkLabel1.Visible = false;
            btnDelete.Visible = false;

            string sql = @"SELECT [branch_id]
      ,[branch_name]
  FROM [dbo].[branches]";
            DataTable dt = db.excuteDt(string.Format(sql));
            comboBoxBranches.DataSource = dt;
            comboBoxBranches.DisplayMember = "branch_name";
            comboBoxBranches.ValueMember = "branch_id";
            // 
            comboBoxBranches.SelectedIndex = -1;
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //بحث وفلتره المستخدمين
            string sql = @"SELECT dbo.Users.user_id, dbo.Users.user_code, dbo.Users.user_fullname, dbo.Users.user_address, dbo.Users.user_phone, dbo.Users.user_date, dbo.Users.user_name, dbo.Users.user_password, dbo.Users.user_kind_id, 
                         dbo.Users.branch_id, dbo.branches.branch_name, dbo.Users_kind.user_kind
FROM  dbo.Users INNER JOIN
                         dbo.Users_kind ON dbo.Users.user_kind_id = dbo.Users_kind.user_kind_id LEFT OUTER JOIN
                         dbo.branches ON dbo.Users.branch_id = dbo.branches.branch_id where 1=1";
            string condition = "";
            if (txtName.Text !="")
            {
                condition += " and Users.user_fullname ='" + txtName.Text + "'";
            }
            if (txtCode.Text != "")
            {
                condition += " and Users.user_code ='" + txtCode.Text + "'";
            }
            if (txtPhone.Text != "")
            {
                condition += " and Users.user_phone ='" + txtPhone.Text + "'";
            }
            //مهم
            if (comboBoxBranches.SelectedIndex >=0)
            {
                condition += " and Users.branch_id ='" + comboBoxBranches.SelectedValue + "'";
            }
            if (user_kind_id!="")
            {
                condition += " and Users.user_kind_id ='" +user_kind_id +"'";
            }
            DataTable dt = db.excuteDt(string.Format(sql+condition));
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns["branch_name"].HeaderText = "اسم الفرع";
            dataGridView1.Columns["user_id"].HeaderText = "رقم المستخدم";
            dataGridView1.Columns["user_code"].HeaderText = "كود المستخدم";
            dataGridView1.Columns["user_fullname"].HeaderText = "الاسم";
            dataGridView1.Columns["user_address"].HeaderText = "العنوان";
            dataGridView1.Columns["user_phone"].HeaderText = "التليفون";
            dataGridView1.Columns["user_date"].HeaderText = "التاريخ";
            dataGridView1.Columns["user_name"].HeaderText = "اسم الدخول";
            dataGridView1.Columns["user_password"].HeaderText = "كلمه الموور";
            dataGridView1.Columns["user_kind"].HeaderText = "نوع المستخدم";
            if (user_kind_id=="1"||user_kind_id=="2")
            {
                dataGridView1.Columns["user_name"].Visible = false;
                dataGridView1.Columns["user_password"].Visible = false;
                dataGridView1.ClearSelection();
                btnDelete.Visible = false;
                linkLabel1.Visible = false;
            }
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow row = this.dataGridView1.Rows[rowindex];
                string user_id = row.Cells["user_id"].Value.ToString();
                linkLabel1.Visible = true;
                btnDelete.Visible = true;
                lblUserId.Text = user_id;

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmUsersEdit frm = new frmUsersEdit(lblUserId.Text,user_kind_id);
            frm.MdiParent = frmMain.ActiveForm;
            frm.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql1 = "delete from Users where user_id='{0}'";
            db.excuteSql(string.Format(sql1,lblUserId.Text));
            MessageBox.Show("تم الحذف");
            dataGridView1.ClearSelection();
            dataGridView1.Refresh();
            comboBoxBranches.SelectedIndex = -1;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            
            string sql = @"SELECT dbo.Users.user_id, dbo.Users.user_code, dbo.Users.user_fullname, dbo.Users.user_address, dbo.Users.user_phone, dbo.Users.user_date, dbo.Users.user_name, dbo.Users.user_password, dbo.Users.user_kind_id, 
                         dbo.Users.branch_id, dbo.branches.branch_name, dbo.Users_kind.user_kind
FROM  dbo.Users INNER JOIN
                         dbo.Users_kind ON dbo.Users.user_kind_id = dbo.Users_kind.user_kind_id LEFT OUTER JOIN
                         dbo.branches ON dbo.Users.branch_id = dbo.branches.branch_id where";
            string condition = "";
            if (user_kind_id != "")
            {
                condition += " Users.user_kind_id ='" + user_kind_id + "'";
            }
            if (txtName.Text != "")
            {
                condition += " and Users.user_fullname like '%" + txtName.Text + "%'";
            }
           
           
            DataTable dt = db.excuteDt(string.Format(sql + condition));
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns["branch_name"].HeaderText = "اسم الفرع";
            dataGridView1.Columns["user_id"].HeaderText = "رقم المستخدم";
            dataGridView1.Columns["user_code"].HeaderText = "كود المستخدم";
            dataGridView1.Columns["user_fullname"].HeaderText = "الاسم";
            dataGridView1.Columns["user_address"].HeaderText = "العنوان";
            dataGridView1.Columns["user_phone"].HeaderText = "التليفون";
            dataGridView1.Columns["user_date"].HeaderText = "التاريخ";
            dataGridView1.Columns["user_name"].HeaderText = "اسم الدخول";
            dataGridView1.Columns["user_password"].HeaderText = "كلمه الموور";
            dataGridView1.Columns["user_kind"].HeaderText = "نوع المستخدم";
            if (user_kind_id == "1" || user_kind_id == "2")
            {
                dataGridView1.Columns["user_name"].Visible = false;
                dataGridView1.Columns["user_password"].Visible = false;
                dataGridView1.ClearSelection();
                btnDelete.Visible = false;
                linkLabel1.Visible = false;
            }
        }
    }
}
