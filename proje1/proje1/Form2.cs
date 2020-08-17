using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace proje1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        DbSınavOgrenciEntities db = new DbSınavOgrenciEntities();
        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var degerler = db.TBLNOTLAR.Where(p => p.SINAV1 < 50);
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton2.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Where(p => p.AD == "Ali");
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton3.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Select(x => new { soyadı = x.SOYAD });
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton4.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Select(x =>
                new
                {
                    ad = x.AD.ToUpper(),
                    soyadı = x.SOYAD.ToLower()
                });
                dataGridView1.DataSource = degerler.ToList();
            }

            if (radioButton5.Checked == true)
            {
                var degerler = db.TBLOGRENCI.Select(x =>
                new
                {
                    ad = x.AD.ToUpper(),
                    soyadı = x.SOYAD.ToLower()
                }).Where(x => x.ad != "Ali");
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton6.Checked == true)
            {
                var degerler = db.TBLNOTLAR.Select(x => new
                {
                    ÖgrenciAd = x.OGR,
                    Ortalama = x.ORTALAM,
                    Durum = x.DURUM == true ? "Gecti" : "Kaldı"


                });
                dataGridView1.DataSource = degerler.ToList();

            }

            if (radioButton7.Checked == true)
            {
                var degerler = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(y => y.ID == x.OGR), (x, y) => new
                {
                    Ad = y.AD,
                    Soyad = y.SOYAD,
                    Ortalam = x.ORTALAM,
                    Durum = x.DURUM == true ? "Gecti" : "Kaldı"
                });

                dataGridView1.DataSource = degerler.ToList();

            }



        }
    }
}
