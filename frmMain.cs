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
    public partial class frmMain : Form
    {
        DB db = new DB();   
        string user_id = "";
        public frmMain(string userid)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            user_id = userid;
        }
        public bool permissions(string user_id,string screen_name)
        {
            bool per = false;
            string sql = @"select group_id from Users where user_id=N'{0}'";
            string group_id = db.excuteSql(string.Format(sql, user_id));
            string sql2 = @"select screen_name from permissions_screens where group_id=N'{0}' and screen_name=N'{1}'";
            string ispermission = db.excuteSql(string.Format(sql2, group_id, screen_name));
            if (ispermission!="")
            {
                per = true;
            }

            return per; 
        }

        private void اضافهفروعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmBranches");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {


                frmBranches frm = new frmBranches();
                frm.MdiParent = this;
                frm.Show();
                frm.StartPosition = FormStartPosition.CenterScreen;
            }
            
        }
        

        private void المحازنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmBranche_stores");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmBranche_stores frm = new frmBranche_stores();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void اعداداتالبرنامجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            bool access = permissions(user_id, "frmSettings");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmSettings frm = new frmSettings();
                frm.MdiParent = this;
                frm.Show();

            }
        }

        private void اضافهموردToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmUserAdd");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmUserAdd frm = new frmUserAdd("2");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void اضافهعميلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmUserAdd");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmUserAdd frm = new frmUserAdd("1");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void اضافهموظفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmUserAdd");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmUserAdd frm = new frmUserAdd("3");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void بحثوفلترهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmUsersShow");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmUsersShow frm = new frmUsersShow("2");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void بحثوفلترهToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmUsersShow");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmUsersShow frm = new frmUsersShow("1");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void بحثوفلترهToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmUsersShow");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmUsersShow frm = new frmUsersShow("3");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void فئاتالمنتجاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmProductsCategory");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmProductsCategory frm = new frmProductsCategory();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void المنتجاتToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmProducts");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmProducts frm = new frmProducts();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            if (MessageBox.Show("هل تريد الخروج", "تنبيه", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {

                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void اوامرالشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmOrder_Purchase");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmOrder_Purchase frm = new frmOrder_Purchase("1");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void اوامرالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmOrderSales");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmOrderSales frm = new frmOrderSales("2");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void مرتجعبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmOrder_Purchase");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmOrder_Purchase frm = new frmOrder_Purchase("3");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void مرتجعشراءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmOrderSales");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmOrderSales frm = new frmOrderSales("4");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void تقاريرالعملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmUsersReports");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmUsersReports frm = new frmUsersReports("1");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void تقاريرالموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmUsersReports");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmUsersReports frm = new frmUsersReports("2");
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void صلاحياتالمستخدمينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool access = permissions(user_id, "frmPermissions_screens");
            if (access == false)
            {
                MessageBox.Show("عذرا, ليس لديك صلاحيه الدخول");
            }
            else
            {
                frmPermissions_screens frm = new frmPermissions_screens();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void pOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmPOS frm = new frmPOS(user_id);
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
