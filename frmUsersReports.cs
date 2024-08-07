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
    public partial class frmUsersReports : Form
    {
        DB db = new DB();
        string user_kind_id = "";
        public frmUsersReports(string user_kind)
        {
            InitializeComponent();
            user_kind_id = user_kind;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT dbo.Users.user_id, dbo.Users.user_code, dbo.Users.user_fullname, dbo.Users.user_address, dbo.Users.user_phone, dbo.Users.user_date, dbo.Users.user_name, dbo.Users.user_password, dbo.Users.user_kind_id, 
                         dbo.Users.branch_id, dbo.branches.branch_name, dbo.Users_kind.user_kind
FROM  dbo.Users INNER JOIN
                         dbo.Users_kind ON dbo.Users.user_kind_id = dbo.Users_kind.user_kind_id LEFT OUTER JOIN
                         dbo.branches ON dbo.Users.branch_id = dbo.branches.branch_id where 1=1";
            string condition = "";
            if (txtName.Text != "")
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
            if (comboBoxBranches.SelectedIndex >= 0)
            {
                condition += " and Users.branch_id ='" + comboBoxBranches.SelectedValue + "'";
            }
            if (user_kind_id != "")
            {
                condition += " and Users.user_kind_id ='" + user_kind_id + "'";
            }
            DataTable dt = db.excuteDt(string.Format(sql + condition));
            dt.TableName = "customers";
            DataSet1 dataset = new DataSet1();
            dataset.Tables.Add(dt);
            crystalCustomers crystalCustomers = new crystalCustomers();
            crystalCustomers.SetDataSource(dt);
            crystalReportViewer1.ReportSource = crystalCustomers;
            crystalReportViewer1.Refresh();
            
        }

        private void frmUsersReports_Load(object sender, EventArgs e)
        {
            combobranch();
        }
        void combobranch()
        {
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
    }
}
