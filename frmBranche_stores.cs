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
    public partial class frmBranche_stores : Form
    {
        DB db = new DB();
        public frmBranche_stores()
        {
            InitializeComponent();
        }

        private void frmBranche_stores_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT [branch_id]
      ,[branch_name]
  FROM [dbo].[branches]";
            DataTable dt = db.excuteDt(string.Format(sql));
            comboBoxBranches.DataSource = dt;
            comboBoxBranches.DisplayMember = "branch_name";
            comboBoxBranches.ValueMember = "branch_id";
            show();
        }
        void show()
        {
            string sql = @"SELECT        dbo.branches.branch_name, dbo.branche_stores.store_name, dbo.branche_stores.store_id
FROM            dbo.branches INNER JOIN
                         dbo.branche_stores ON dbo.branches.branch_id = dbo.branche_stores.branch_id";
            DataTable dt = db.excuteDt(string.Format(sql));
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            dataGridView1.Columns[0].HeaderText = "اسم الفرع";
            dataGridView1.Columns[1].HeaderText = "اسم المخزن";
            dataGridView1.Columns[2].HeaderText = "رقم المخزن";


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO [dbo].[branche_stores]
           ([store_name]
           ,[branch_id])
     VALUES (N'{0}',N'{1}')";
            db.excuteSql(string.Format(sql, txtName.Text, comboBoxBranches.SelectedValue));
            MessageBox.Show("تم الحفظ");
            show();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow row = this.dataGridView1.Rows[rowindex];
                string store_id = row.Cells["store_id"].Value.ToString();
                lblid.Text = store_id;
                string sql = "select store_name from branche_stores where store_id='{0}'";
                string store_name = db.excuteSql(string.Format(sql, store_id));
                txtName.Text = store_name;
                string sql_branche = "select branch_id from branche_stores where store_id='{0}'";
                string branche_id = db.excuteSql(string.Format(sql_branche, store_id));
                comboBoxBranches.SelectedValue = branche_id;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                btnSave.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
            }
      
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string sql = "update branche_stores set store_name =N'{0}',branch_id=N'{1}' where store_id =N'{2}'";
            db.excuteSql(string.Format(sql, txtName.Text,comboBoxBranches.SelectedValue, lblid.Text));
            MessageBox.Show("تم التعديل");
            show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = "delete from branche_stores where store_id='{0}'";
            db.excuteSql(string.Format(sql, lblid.Text));
            MessageBox.Show("تم الحذف");
            show();
        }

        private void frmBranche_stores_Activated(object sender, EventArgs e)
        {

        }

        private void frmBranche_stores_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
