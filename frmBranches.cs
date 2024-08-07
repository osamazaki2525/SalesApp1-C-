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
    public partial class frmBranches : Form
    {
        DB db = new DB();
       
        public frmBranches()
        {
            InitializeComponent();
           
            this.AcceptButton = btnSave;
            dataGridView1.PerformLayout();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtName.Text == "")
            {
                MessageBox.Show("عفوا يجب ادخال البيانات");
                return;
            }
            string sql1 = "select branch_name from branches";
            DataTable dt =db.excuteDt(string.Format(sql1));
            foreach (DataRow row in dt.Rows) 
            {
                if (txtName.Text==row["branch_name"].ToString())
                {
                    MessageBox.Show("الاسم موجود مسبقا يرجي ادخال اسم اخر");
                    return;
                }
            }
            string sql = @"INSERT INTO branches (branch_name) VALUES (N'{0}')";
            db.excuteSql(string.Format(sql, txtName.Text));

            MessageBox.Show("تم الحفظ بنجاح");
            show();
            dataGridView1.ClearSelection();
        }

        private void frmBranchAdd_Load(object sender, EventArgs e)
        {
            show();
            dataGridView1.MultiSelect = false;
           
           
        }
        void show()
        {
           
            string sql = @"SELECT [branch_id]
      ,[branch_name]
  FROM [dbo].[branches]";
            DataTable dt = db.excuteDt(string.Format(sql));
            dataGridView1.DataSource = dt;

            dataGridView1.Refresh();
            dataGridView1.PerformLayout();

            dataGridView1.Columns[0].HeaderText = "رقم الفرع";
            dataGridView1.Columns[1].HeaderText = "اسم الفرع";
            dataGridView1.Columns[0].Visible = false;

           

        }

      

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string sql = "update branches set branch_name =N'{0}' where branch_id =N'{1}'";
            db.excuteSql(string.Format(sql,txtName.Text,lblid.Text));
           show();
            dataGridView1.ClearSelection();
            frmPOS frm = new frmPOS("s");
           

            MessageBox.Show("تم التعديل");
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = "delete from branches where branch_id='{0}'";
            db.excuteSql(string.Format(sql, lblid.Text));
            show();
            dataGridView1.ClearSelection();

            MessageBox.Show("تم الحذف");
           
        }

        private void frmBranches_Activated(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            txtName.Text = "";
        }

        private void frmBranches_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow row = this.dataGridView1.Rows[rowindex];
                string branch_id = row.Cells["branch_id"].Value.ToString();
                string sql = "select branch_name from branches where branch_id='{0}'";
                string branch_name = db.excuteSql(string.Format(sql, branch_id));
                txtName.Text = branch_name;
                lblid.Text = branch_id;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtName.Text = "";
            }
        }
    }
}
