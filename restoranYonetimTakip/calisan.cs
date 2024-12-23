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
    public partial class calisan : Form
    {
        public calisan()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
           
                try
                {
                    // Giriş ve çıkış saatlerini al
                    TimeSpan girisSaat = dateTimePicker1.Value.TimeOfDay;
                    TimeSpan cikisSaat = dateTimePicker2.Value.TimeOfDay;

                    // Saatler arasındaki farkı hesapla
                    TimeSpan fark = cikisSaat - girisSaat;

                    if (fark.TotalMinutes < 0)
                    {
                        MessageBox.Show("Giriş saati çıkış saatinden sonra olamaz!");
                    }
                    else
                    {
                        MessageBox.Show($"Çalışma süresi: {fark.Hours} saat, {fark.Minutes} dakika.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            



        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
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
                    "DELETE FROM \"calisanlar\" WHERE \"telefon\" = @p1",
                    baglanti);

                // Parametreyi ekliyoruz
                komut2.Parameters.AddWithValue("@p1", maskedTextBox1.Text);  // yemek adı TextBox'tan alınan değer

                // Komutu çalıştırıyoruz
                komut2.ExecuteNonQuery();

                // Kullanıcıya başarı mesajı gösteriyoruz
                MessageBox.Show("calisan silme işlemi başarılı bir şekilde gerçekleşti.");
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
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432;" +
          "database=restoranYonetimTakip; user ID=postgres; password=9446398");
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut1 = new NpgsqlCommand(
                    "INSERT INTO calisanlar (\"ad\", \"soyad\", \"telefon\", \"e_posta\", \"maas\") " +
                    "VALUES (@p1, @p2, @p3, @p4, @p5)", baglanti);

                komut1.Parameters.AddWithValue("@p1", textBox1.Text);  // Ad
                komut1.Parameters.AddWithValue("@p2", textBox3.Text);  // Soyad
                komut1.Parameters.AddWithValue("@p3", maskedTextBox1.Text);  // Telefon
                komut1.Parameters.AddWithValue("@p4", textBox2.Text);  // E-posta
                komut1.Parameters.AddWithValue("@p5", Convert.ToInt32(textBox4.Text));  // Maaş
                komut1.ExecuteNonQuery();

                MessageBox.Show("Çalışan ekleme işlemi başarılı.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
            }
            finally
            {
                baglanti.Close();  // Bağlantıyı her durumda kapatıyoruz
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "select*from \"calisanlar\"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
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
                    "UPDATE \"calisanlar\" SET \"ad\"= @p1, \"soyad\"= @p2, \"e_posta\"= @p4,\"maas\"=@p5 WHERE \"telefon\" = @p3", baglanti);

                // Parametreleri ekliyoruz
                komut3.Parameters.AddWithValue("@p1", textBox1.Text); // musteri adı
                komut3.Parameters.AddWithValue("@p2", textBox3.Text); // musteri soyad
                komut3.Parameters.AddWithValue("@p3", maskedTextBox1.Text); // 
                komut3.Parameters.AddWithValue("@p4", textBox2.Text); //
                komut3.Parameters.AddWithValue("@p5", Convert.ToInt32(textBox4.Text));  // Maaş
                                                                                        // 
                komut3.Parameters.AddWithValue("@p6", textBox4.Text); // 

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

        private void calisan_Load(object sender, EventArgs e)
        {
           

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
        

