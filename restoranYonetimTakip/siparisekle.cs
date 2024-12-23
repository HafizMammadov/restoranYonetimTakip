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
    public partial class siparisekle : Form
    {
        public siparisekle()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432;" +
            "database=restoranYonetimTakip; user ID=postgres; password=9446398");

        private void button2_Click(object sender, EventArgs e)
        {
               try
                {
                    baglanti.Open();
                    NpgsqlCommand komut1 = new NpgsqlCommand(
                        "INSERT INTO public.\"siparisDetaylari\" (\"telefon\", \"ogead\", \"miktar\", \"birimFiyati\") " +
                        "VALUES (@p1, @p2, @p3, @p4)",
                        baglanti);

                    komut1.Parameters.AddWithValue("@p1", maskedTextBox1.Text);  // telefon
                    komut1.Parameters.AddWithValue("@p2", comboBox1.SelectedValue);  // ogeID
                    komut1.Parameters.AddWithValue("@p3", Convert.ToDecimal(numericUpDown1.Value));  // miktar
                    komut1.Parameters.AddWithValue("@p4", Convert.ToDecimal(textBox1.Text));  // birimFiyat

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


            private void siparisekle_Load(object sender, EventArgs e)
        {
          
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(" select* from \"menuOgeleri\" ", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "yemekAd";
            comboBox1.ValueMember = "ogeID";

            comboBox1.DataSource = dt;

            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı açıyoruz
                baglanti.Open();

                // Silme komutunu oluşturuyoruz
                NpgsqlCommand komut2 = new NpgsqlCommand(
                    "DELETE FROM \"siparisDetaylari\" WHERE \"telefon\" = @p1",
                    baglanti);

                // Parametreyi ekliyoruz
                komut2.Parameters.AddWithValue("@p1", maskedTextBox1.Text);  // telefon

                // Komutu çalıştırıyoruz
                komut2.ExecuteNonQuery();

                // Kullanıcıya başarı mesajı gösteriyoruz
                MessageBox.Show("siparis silme işlemi başarılı bir şekilde gerçekleşti.");
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

        private void button3_Click(object sender, EventArgs e)
        {

            string sorgu = "SELECT * FROM public.\"siparisDetaylari\";";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
