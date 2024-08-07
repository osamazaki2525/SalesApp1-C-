using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


public partial class MyImagePanel_category : Panel
    {
    DB db = new DB();
    
    private string imgURL = "";
    private string title = "";
    private string id_label = "";
    private string _id = string.Empty;
    private PictureBox pict = null;
    private LinkLabel label1 = null;
   
   
    
    public MyImagePanel_category()
            : base()
        {
            InitializeComponent();
       
        }
        public MyImagePanel_category(string img, string title, string id)
            : base()
        {
        imgURL = img;
        id_label = id;
        this.title = title;
        this._id = id;
        this.BorderStyle = BorderStyle.Fixed3D;
        this.Name = "panel1";
        this.Size = new System.Drawing.Size(250, 130);
        this.pict = new PictureBox();
        this.pict.Location = new System.Drawing.Point(3, 3);
        this.pict.Image = new Bitmap(img);
        this.pict.Size = new System.Drawing.Size(250, 130);
        this.pict.SizeMode = PictureBoxSizeMode.StretchImage;
        this.label1 = new LinkLabel();
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Arial", 12);
        this.label1.Location = new System.Drawing.Point(120, 100);
        this.label1.Size = new System.Drawing.Size(131, 13);
        this.label1.Text = title;
        this.label1.Tag = id;
        label1.Click += label1_Click;
        this.Controls.Add(this.label1);
        this.Controls.Add(this.pict);

        InitializeComponent();
        }
  
    void label1_Click(object sender, EventArgs e)
    {






        if (!File.Exists("D://practice/test.txt"))
        {
            File.WriteAllText("D://practice/test.txt", _id + Environment.NewLine);
        }
        else
        {
            File.AppendAllText("D://practice/test.txt", _id + Environment.NewLine);
        }
        MessageBox.Show("تم الاضافه");
        //this.label2 = new Label();
        //  this.label2.Visible = false;
        //  this.label2.Text = "";
        //  if (label2.Text=="")
        //  {

        //      this.DataGridView1 = new DataGridView();
        //      this.DataGridView1.Location = new System.Drawing.Point(20, 300);
        //      this.DataGridView1.Size = new System.Drawing.Size(300, 300);
        //      this.DataGridView1.Columns.Add("product_title", "Name");
        //      this.DataGridView1.Columns.Add("product_price", "Price");
        //      this.DataGridView1.Columns.Add("product_quantity", "Quantity");
        //      this.Controls.Add(DataGridView1);
        //      InitializeComponent();
        //      label2.Text = "1";
        //  }
        //  DB db = new DB();
        //  string price=db.excuteSql("select product_price from products where product_id='" + _id+ "' ");
        //  string quantity = "1";
        //  this.DataGridView1.Rows.Add(title, price, quantity);
        //  DataGridView1.Refresh();





    }


    public void setImageURL(String url)
        {
            this.imgURL = url;
        }
        public void setTitle(String title)
        {
            this.title = title;
        }
    }
 