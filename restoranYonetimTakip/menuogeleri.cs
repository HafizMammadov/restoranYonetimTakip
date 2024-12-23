using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace restoranYonetimTakip
{
    public partial class menuogeleri : Form
    {
        public menuogeleri()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432;" +
           "database=restoranYonetimTakip; user ID=postgres; password=9446398");
        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "select*from \"menuOgeleri\"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand(
                    "INSERT INTO public.\"menuOgeleri\" (\"yemekAd\", fiyat, stok, \"kategori\", \"kategoriID\") " +
                    "VALUES (@p1, @p2, @p3, @p4, (SELECT \"kategoriID\" FROM \"yemekKategorileri\" WHERE \"kategoriAdi\" = @p4))",
                    baglanti);

                komut1.Parameters.AddWithValue("@p1", comboBox1.Text);
                komut1.Parameters.AddWithValue("@p2", Convert.ToDecimal(textBox2.Text));
                komut1.Parameters.AddWithValue("@p3", Convert.ToInt32(numericUpDown2.Text));
                komut1.Parameters.AddWithValue("@p4", comboBox2.Text);

                komut1.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Ekleme işlemi başarılı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
                baglanti.Close();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuogeleri_Load(object sender, EventArgs e)
        {
            try
            {
               
                {
                    baglanti.Open();

                    // Yemek kategorilerini comboBox2'ye ekliyoruz
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT * FROM \"yemekKategorileri\"", baglanti);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox2.DisplayMember = "kategoriAdi";
                    comboBox2.ValueMember = "kategoriID";
                    comboBox2.DataSource = dt;

                    // Yemek adlarını comboBox1'e ekliyoruz
                    NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("SELECT * FROM \"menuOgeleri\"", baglanti);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    // Yemek adlarını comboBox1'de gösteriyoruz
                    comboBox1.DisplayMember = "yemekAd";
                    comboBox1.ValueMember = "ogeID";
                    comboBox1.DataSource = dt1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message + "\nStack Trace: " + ex.StackTrace);
            }

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
       
            try
            {
                // Bağlantıyı açıyoruz
                baglanti.Open();

                // Silme komutunu oluşturuyoruz
                NpgsqlCommand komut2 = new NpgsqlCommand(
                    "DELETE FROM \"menuOgeleri\" WHERE \"yemekAd\" = @p1",
                    baglanti);

                // Parametreyi ekliyoruz
                komut2.Parameters.AddWithValue("@p1", comboBox1.Text);  // yemek adı TextBox'tan alınan değer

                // Komutu çalıştırıyoruz
                komut2.ExecuteNonQuery();

                // Kullanıcıya başarı mesajı gösteriyoruz
                MessageBox.Show("Ürün silme işlemi başarılı bir şekilde gerçekleşti.");
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj gösteriyoruz
                MessageBox.Show($"Hata: {ex.Message}");
            }
            finally
            {
                // Bağlantıyı her durumda kapatıyoruz
                baglanti.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı açmadan önce kontrol ediyoruz
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand komut3 = new NpgsqlCommand(
                    "UPDATE \"menuOgeleri\" SET stok = @p2, kategori = @p3, fiyat = @p4 WHERE \"yemekAd\" = @p1",
                    baglanti);

                // Parametreleri ekliyoruz
                komut3.Parameters.AddWithValue("@p1", comboBox1.Text); // Yemek adı
                komut3.Parameters.AddWithValue("@p2", numericUpDown2.Value); // Stok miktarı
                komut3.Parameters.AddWithValue("@p3", comboBox2.SelectedValue); // Kategori ID'si
                komut3.Parameters.AddWithValue("@p4", Convert.ToDecimal(textBox2.Text)); // Fiyat

                // Sorguyu çalıştırıyoruz
                komut3.ExecuteNonQuery();
                MessageBox.Show("Güncelleme başarılı.");
            }
            catch (Exception ex)
            {
                // Hata mesajı
                MessageBox.Show($"Hata: {ex.Message}");
            }
            finally
            {
                // Bağlantıyı her durumda kapatıyoruz
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

