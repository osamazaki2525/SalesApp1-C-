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
    public partial class frmOrder_Purchase : Form
    {
        DB db = new DB();
        string order_kind_id = "";
        public frmOrder_Purchase(string order_kind)
        {
            InitializeComponent();
            order_kind_id = order_kind;
        }

        private void frmOrder_Purchase_Load(object sender, EventArgs e)
        {
            loadstores();
            loadvendors();
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblEmpId.Text = "4";
        }
        void loadstores()
        {
            string sql = @"SELECT [store_id]
      ,[store_name]
      ,[branch_id]
  FROM [dbo].[branche_stores] ";
            DataTable dt = db.excuteDt(string.Format(sql));

            comboStores.DisplayMember = "store_name";
            comboStores.ValueMember = "store_id";
            comboStores.DataSource = dt;
        }
        void loadvendors()
        {
            string sql = @"SELECT [user_id]
      ,[user_code]
      ,[user_fullname]
      ,[user_kind_id]
  FROM [dbo].[Users] where user_kind_id='2' ";
            DataTable dt = db.excuteDt(string.Format(sql));

            comboVendors.DisplayMember = "user_fullname";
            comboVendors.ValueMember = "user_id";
            comboVendors.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var comboboxproducts = new DataGridViewComboBoxColumn();
            var comboboxunits = new DataGridViewComboBoxColumn();
            var txtprice = new DataGridViewTextBoxColumn();
            var txtquantity = new DataGridViewTextBoxColumn();
            //
            string sqlproducts = @"SELECT [product_id]
      ,[product_name]
  FROM [dbo].[products] where store_id='{0}'";
            DataTable dtproducts = db.excuteDt(string.Format(sqlproducts,comboStores.SelectedValue));
            comboboxproducts.HeaderText = "المنتجات";
            comboboxproducts.Name = "products";
            comboboxproducts.DisplayMember = "product_name";
            comboboxproducts.ValueMember = "product_id";
            comboboxproducts.DataSource = dtproducts;
            //
            string sqlunits = @"SELECT [unit_id]
      ,[unit_name]
  FROM [dbo].[units]";
            DataTable dtunits = db.excuteDt(string.Format(sqlunits));
            comboboxunits.HeaderText = "الوحده";
            comboboxunits.Name = "units";
            comboboxunits.DisplayMember = "unit_name";
            comboboxunits.ValueMember = "unit_id";
            comboboxunits.DataSource = dtunits;
            //
            txtprice.HeaderText = "السعر";
            txtprice.Name = "price";
            //
            txtquantity.HeaderText="الكميه";
            txtquantity.Name = "quantity";
            //
            dataGridView1.Columns.AddRange(comboboxproducts, comboboxunits, txtquantity, txtprice);


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //insert master details 
            string sql = @"INSERT INTO [dbo].[order_master]
           ([store_id]
           ,[user_id]
           ,[emp_id]
           ,[order_code]
           ,[order_date]
           ,[order_tax]
           ,[order_discount]
           ,[order_kind_id])
     VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}')";
            var order_date = DateTime.Now;
            string order_code = "";
            string order_id = db.excuteSql_ReturnId(string.Format(sql,comboStores.SelectedValue,comboVendors.SelectedValue,lblEmpId.Text,order_code,order_date,txtTax.Text,txtDiscount.Text,order_kind_id));
            Random rand = new Random();
            int x = rand.Next(1000, 5000);
            order_code = x.ToString() + order_id;
            //update code
            string updateSql = @"update order_master set order_code=N'{0}' where order_id=N'{1}'";
            db.excuteSql(string.Format(updateSql, order_code, order_id));
            //insert details
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                // it's nessecry to do this if statement or program crashes
                if (item.Cells["products"].Value!=null)
                {


                    string product_id = item.Cells["products"].Value.ToString();   
                    string unit_id = item.Cells["units"].Value.ToString();
                    string price = item.Cells["price"].Value.ToString();

                    string quantity = item.Cells["quantity"].Value.ToString();
                    string sqldetails = @"INSERT INTO [dbo].[details]
           ([order_id]
           ,[unit_id]
           ,[product_id]
           ,[price]
           ,[quantity])
     VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}')";
                    db.excuteSql(string.Format(sqldetails, order_id, unit_id, product_id, price, quantity));
                    MessageBox.Show("تم تنفيذ العمليه بنجاح");
                    this.Hide();

                }
                else
                {
                    return;
                }
            }

        }
    }
}
