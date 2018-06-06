using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace QLCuaHang
{
    public partial class Form1 : Form
    {
        SqlConnection con;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string conString = ConfigurationManager.ConnectionStrings["SanPham"].ConnectionString.ToString();
            con = new SqlConnection(conString);
            con.Open();
            HienThi();
        }
        private void Form1_FormClosing(object sender, EventArgs e)
        {
            con.Close();
        }
        
        public void HienThi()
        {
            string SqlSELECT = "SELECT * FROM SanPham";
            SqlCommand cmd = new SqlCommand(SqlSELECT, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
           
        }
        //chuc nang them san pham
        private void btnThem_Click(object sender, EventArgs e)
        {
            string SqlINSERT = "INSERT INTO SanPham VALUES (@maSP , @tenSP , @giaSP)";
            SqlCommand cmd = new SqlCommand(SqlINSERT, con);
            cmd.Parameters.AddWithValue("maSP" , txtMa.Text);
            cmd.Parameters.AddWithValue("tenSP", txtTen.Text);
            cmd.Parameters.AddWithValue("giaSP" , txtGia.Text);
            cmd.ExecuteNonQuery();
            HienThi();
        }
        //chuc nang xoa san pham
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string SqlDELETE = "DELETE FROM SanPHam WHERE maSP = @maSP";
            SqlCommand cmd = new SqlCommand(SqlDELETE, con);
            cmd.Parameters.AddWithValue("maSP", txtMa.Text);
            cmd.Parameters.AddWithValue("tenSP", txtTen.Text);
            cmd.Parameters.AddWithValue("giaSP", txtGia.Text);
            cmd.ExecuteNonQuery();
            HienThi();
        }
        //chuc nang sua san pham
        private void btnSua_Click(object sender, EventArgs e)
        {
            string SqlEDIT = "UPDATE SanPham SET tenSP = @tenSP , giaSP = @giaSP WHERE maSP=@maSP";
            SqlCommand cmd = new SqlCommand(SqlEDIT, con);
            cmd.Parameters.AddWithValue("maSP", txtMa.Text);
            cmd.Parameters.AddWithValue("tenSP", txtTen.Text);
            cmd.Parameters.AddWithValue("giaSP", txtGia.Text);
            cmd.ExecuteNonQuery();
            HienThi();
        }
        // tim kiem san pham
        private void btnTimMa_Click(object sender, EventArgs e)
        {
            string SqlSEARCH = "SELECT * FROM SanPham WHERE maSP = @maSP";
            SqlCommand cmd = new SqlCommand(SqlSEARCH, con);
            cmd.Parameters.AddWithValue("maSP", txtTimMa.Text);
            cmd.Parameters.AddWithValue("tenSP", txtTen.Text);
            cmd.Parameters.AddWithValue("giaSP", txtGia.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }
     
        // tinh tien
        private void btnTong_Click(object sender, EventArgs e)
        {
            int sum = 0;
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Selected == true)
                {
                    sum+= Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                }
                lblTong.Text = "Tổng cộng là : " + sum.ToString() + " đồng";  
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtGia.Clear();
            txtMa.Clear();
            txtTen.Clear();
            txtTimMa.Clear();
            
        }

    }
}
