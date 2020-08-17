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

namespace proje1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        DbSınavOgrenciEntities db = new DbSınavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = db.TBLOGRENCI.ToList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnDersListesi_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-K1BPNQ3\SQLEXPRESS;Initial Catalog=DbSınavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("select * from TBLDERSLER",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            var query = from item in db.TBLNOTLAR
                        select new
                        {
                            item.NOTID,
                            item.OGR,
                            item.DERS,
                            item.SINAV1,
                            item.SINAV2,
                            item.SINAV3,
                            item.ORTALAM,
                            item.DURUM
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            t.AD = TextAd.Text;
            t.SOYAD = TextSoyad.Text;
            db.TBLOGRENCI.Add(t);
            db.SaveChanges();
            MessageBox.Show("Ögrenci Eklendi....");
            
        }

        private void TextSoyad_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = TextAd.Text;
            var degerler = from item in db.TBLOGRENCI where item.AD.Contains(aranan) select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciId.Text);

            var x = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Ögreci Sistemden Silindi....");
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciId.Text);

            var x = db.TBLOGRENCI.Find(id);
            x.AD = TextAd.Text;
            x.SOYAD = TextSoyad.Text;
            x.FOTOGRAF = TextFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Güncellendi....");
        }

        private void TextFoto_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnProsedür_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == TextAd.Text & x.SOYAD== TextSoyad.Text).ToList();
        }

        private void BtnButtonEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                {
                List<TBLOGRENCI> Liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = Liste1;
            }

            if (radioButton2.Checked == true)
            {
                List<TBLOGRENCI> Liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = Liste2;
            }

            if (radioButton3.Checked == true)
            {
                List<TBLOGRENCI> Liste3 = db.TBLOGRENCI.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = Liste3;
            }

            if (radioButton4.Checked == true)
            {
                List<TBLOGRENCI> Liste4 = db.TBLOGRENCI.Where(p=>p.ID==5).ToList();
                dataGridView1.DataSource = Liste4;
            }

            if (radioButton5.Checked == true)
            {
                var toplam = db.TBLNOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show("Toplam Ögrenci Sınav 1 Puanı:" + " " + toplam);
            }

            if (radioButton6.Checked == true)
            {
                var Ortalama = db.TBLNOTLAR.Average(p => p.SINAV1);
               
                MessageBox.Show("Toplam Ögrenci Sınav 1 Puanı:" + " " + Ortalama);
            }

            if (radioButton7.Checked == true)
            {
                var Ortalama2 = db.TBLNOTLAR.Where(p=>p.SINAV1>80).ToList();
                dataGridView1.DataSource = Ortalama2;

                //MessageBox.Show("Toplam Ögrenci Sınav 1 Puanı:" + " " + Ortalama2);
            }
            if (radioButton8.Checked == true)
            {
                var EnYuksek = db.TBLNOTLAR.Max(p => p.SINAV1);
                
               
                MessageBox.Show("En Yuksek Sınav1 Sonucu   " + " " + EnYuksek);
            }

            if (radioButton9.Checked == true)



    
            {   //***********************************************************
                var sorgu = db.TBLNOTLAR.Max(x => x.SINAV1);
             
                var enYuksekAdiGetir = from item in db.TBLNOTLAR
                                       where item.SINAV1 == sorgu
                                       select new { item.TBLOGRENCI.AD,item.TBLOGRENCI.SOYAD};

                dataGridView1.DataSource = enYuksekAdiGetir.ToList(); 





            }
        }

        private void BtnForm2Ac_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Visible = false;
            Form2 formac = new Form2();
            formac.Show();
        }
    }
}
