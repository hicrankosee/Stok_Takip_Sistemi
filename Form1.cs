using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Stok_Takip_Sistemi
{
    public partial class Form1 : Form
    {
        
        SqlConnection conn= new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=StokTakipDB;Integrated Security=True");
        public Form1()
        {
            
            InitializeComponent();
        }
        void listele()
        { 
            SqlDataAdapter da = new SqlDataAdapter("SELECT*FROM Urunler",conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource= dt;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Urunler(UrunAdi,BarkodNo,Kategori,Marka,Model,fiyat1,fiyat2,StokMiktari,KritikSeviye)values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", conn);
           
            cmd.Parameters.AddWithValue("@p1", txtUrunAdi.Text);
            cmd.Parameters.AddWithValue("@p2", txtBarkodNo.Text);
            cmd.Parameters.AddWithValue("@p3", txtKategori.Text);
            cmd.Parameters.AddWithValue("@p4", txtMarka.Text);
            cmd.Parameters.AddWithValue("@p5", txtModel.Text);
            cmd.Parameters.AddWithValue("@p6", txtfiyat1.Text);
            cmd.Parameters.AddWithValue("@p7", txtfiyat2.Text);
            cmd.Parameters.AddWithValue("@p8", txtStokMiktari.Text);
            cmd.Parameters.AddWithValue("@p9", txtKritikSeviye.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("ürün başarıyla eklendi.");
            listele();
         }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtUrunID.Text == "")
            {
                MessageBox.Show("lütfen silinecek ürünü tablodan seçin!");
                return;
            }
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Urunler WHERE UrunID=@p1", conn);
            cmd.Parameters.AddWithValue("@p1", txtUrunID.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Ürün başarıyla silindi.");
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUrunID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUrunID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtUrunAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtUrunAdi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtBarkodNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtMarka.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtfiyat1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtfiyat2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtStokMiktari.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            txtKritikSeviye.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Urunler SET UrunAdi=@p1,fiyat1=@p2,fiyat2=@p3 WHERE UrunID=@p4", conn);
            cmd.Parameters.AddWithValue("@p1", txtUrunAdi.Text);
            cmd.Parameters.AddWithValue("@p2", txtfiyat1.Text);
            cmd.Parameters.AddWithValue("@p3", txtfiyat2.Text);
            cmd.Parameters.AddWithValue("@p4", txtUrunID.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Ürün başarıyla güncellendi.");
            listele();
        }
    }
}
