using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje2
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
            var degerler = db.TBLOGRENCI.OrderBy(x => x.SEHIR).GroupBy(y => y.SEHIR).Select(z => new
            {
                Şehir = z.Key,
                Toplam = z.Count()
            }).OrderByDescending(t=>t.Toplam);




            dataGridView1.DataSource = degerler.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var SONUC = db.TBLNOTLAR.Where(x => x.DURUM == false).OrderByDescending(y => y.ORTALAM).Take(1).Select(z => new
            {
                Ogrenci = z.OGR,
                Ortalama = z.ORTALAM,
                Durum = z.DURUM
            });
            dataGridView1.DataSource = SONUC.ToList();

    


        }
    }
}
