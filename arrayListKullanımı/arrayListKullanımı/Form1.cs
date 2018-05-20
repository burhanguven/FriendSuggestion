using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using System.Data.OleDb;

namespace arrayListKullanımı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=CAN;Initial Catalog=arkadasOneri;Integrated Security=True");

        public void ogrenciNetworkDB()
        {
            conn.Open();

            String[] oku = File.ReadAllLines(@"C:\Users\Burhan\Desktop\arkadasoneri\arrayListKullanımı\ogrenciNetwork.csv");
            for (int j = 0; j < oku.Length; j++)
            {
                String[] parcala = oku[j].Split(',');

                //boşluksuz olarak bir list e attım.
                //List<string> numaralar = new List<string>();
                ArrayList numaralar = new ArrayList();
                for (int i = 0; i < parcala.Length; i++)
                {
                    //list deki boş elemanları göz ardı ederek sadece doluları numaralar list ine atıyor.
                    if (!String.IsNullOrEmpty(parcala[i]))
                    {
                        numaralar.Add(parcala[i].ToString());
                    }
                }
                SqlCommand command = new SqlCommand("insert into arkadasNetwork(id,arkadas1,arkadas2,arkadas3,arkadas4,arkadas5,arkadas6,arkadas7,arkadas8,arkadas9,arkadas10) values(@id,@arkadas1,@arkadas2,@arkadas3,@arkadas4,@arkadas5,@arkadas6,@arkadas7,@arkadas8,@arkadas9,@arkadas10) ", conn);

                //@id @arkadas1 .... tipindeki değerleri ne kadar arkadaş varsa o kadar tutacak.
                //(örneğin; @id,@arkadas1,@arkadas2,@arakdas3,...,@arkadas6 ya kadar list oluşturacak.).
                //List<string> arkadas = new List<string>();
                ArrayList arkadas = new ArrayList();
                String[] atama = new String[] { "@id", "@arkadas1", "@arkadas2", "@arkadas3", "@arkadas4", "@arkadas5", "@arkadas6", "@arkadas7", "@arkadas8", "@arkadas9", "@arkadas10" };

                for (int i = 0; i < atama.Length; i++)
                {
                    arkadas.Add(atama[i].ToString());
                }

                for (int i = 0; i < arkadas.Count; i++)
                {
                    //burada amaç elimizde kaç adet numara varsa onları kolonlara yerleştirme.
                    if (i < numaralar.Count)
                    {
                        command.Parameters.AddWithValue(arkadas[i].ToString(), numaralar[i].ToString());
                    }
                    //boş kalan kısımlarada " " boş bir değer eklemek
                    else
                    {
                        command.Parameters.AddWithValue(arkadas[i].ToString(), " ");
                    }

                }
                command.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void ogrenciProfilDB()
        {
            conn.Open();

            String[] oku = File.ReadAllLines(@"C:\Users\Burhan\Desktop\arkadasoneri\arrayListKullanımı\ogrenciProfil.csv");
            for (int j = 0; j < oku.Length; j++)
            {
                String[] parcala = oku[j].Split(',');

                //boşluksuz olarak bir list e attım.
                //List<string> numaralar = new List<string>();
                ArrayList numaralar = new ArrayList();
                for (int i = 0; i < parcala.Length; i++)
                {
                    //list deki boş elemanları göz ardı ederek sadece doluları numaralar list ine atıyor.
                    if (!String.IsNullOrEmpty(parcala[i]))
                    {
                        numaralar.Add(parcala[i].ToString());
                    }
                }
                SqlCommand command = new SqlCommand("insert into ogrenciProfil(id,soru1,soru2,soru3,soru4,soru5,soru6,soru7,soru8,soru9,soru10,soru11,soru12,soru13,soru14,soru15) values('" + numaralar[0].ToString() + "','" + numaralar[1].ToString() + "','" + numaralar[2].ToString() + "','" + numaralar[3].ToString() + "','" + numaralar[4].ToString() + "','" + numaralar[5].ToString() + "','" + numaralar[6].ToString() + "','" + numaralar[7].ToString() + "','" + numaralar[8].ToString() + "','" + numaralar[9].ToString() + "','" + numaralar[10].ToString() + "','" + numaralar[11].ToString() + "','" + numaralar[12].ToString() + "','" + numaralar[13].ToString() + "','" + numaralar[14].ToString() + "','" + numaralar[15].ToString() + "') ", conn);
                command.ExecuteNonQuery();
            }

            conn.Close();
        }
        //bu fonksiyon program her çalıştığında veritabanına veriyi üst üste eklememek için
        //program çalıştığında ogrenciNetwork butonuna basuldığında vt den bütün verileri siler.
        public void ogrenciNetworkDelete()
        {
            conn.Open();
            SqlCommand delete = new SqlCommand("delete from arkadasNetwork", conn);
            delete.ExecuteNonQuery();
            conn.Close();
        }
        //bu fonksiyon program her çalıştığında veritabanına veriyi üst üste eklememek için
        //program çalıştığında ogrenciProfil butonuna basuldığında vt den bütün verileri siler.
        public void ogrenciProfilDelete()
        {
            conn.Open();
            SqlCommand deleteProfil = new SqlCommand("delete from ogrenciProfil", conn);
            deleteProfil.ExecuteNonQuery();
            conn.Close();
        }

        public void ogrenciListeleDelete()
        {
            conn.Open();
            SqlCommand deleteListele = new SqlCommand("delete from ogrenciListesi", conn);
            deleteListele.ExecuteNonQuery();
            conn.Close();
        }

        private void ogrenciNetworkToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ogrenciNetworkDelete();
            //excel dosyamızdan verileri okuyup veritabanına atan fonk.
            ogrenciNetworkDB();
            ogrenciNetwork oNetwork = new ogrenciNetwork();
            oNetwork.ShowDialog();

        }

        private void ogrenciProfilToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ogrenciProfilDelete();
            //excel dosyamızdan verileri okuyup veritabanına atan fonk.
            ogrenciProfilDB();
            ogrenciProfil oProfil = new ogrenciProfil();
            oProfil.ShowDialog();
        }
        private void ogrenciListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ilk başta vt de elaman varsa siliyoruz ki üstüne veri eklemeyelimdiye.
            ogrenciListeleDelete();
            //ogrenciListele panelini açıp form aktif olduğunda vt ye atma işlemi tamamlanacak.
            ogrenciListele oListele = new ogrenciListele();
            oListele.ShowDialog();
        }

        public int[] tempFunc(String[,] op, int a)
        {
            //Arakadaşları ve arkadaşların olmayanlarının yarısıyla doldurduğum Array listdeki her bir kişinin anket değerlerini diziye attım.

            int[] tempPoint = new int[16];
            tempPoint[0] = 0;//Bu değer önemsiz.15 tane anket değeri olduğu için bu değerleri dizinin diğer elamnlarında tuttum.

            for (int i = 1; i < tempPoint.Length; i++)
            {
                tempPoint[i] = Convert.ToInt32(op[a, i]);
            }
            return tempPoint;//Anket değerlerini döndürdüm.
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //listView itemizleme
            listView1.Items.Clear();
            //bu butonda text e girdiğimiz okul numarasının arkadaşlarını verir.
            conn.Open();
            String girilenNumara = textBox1.Text;
            SqlCommand komut = new SqlCommand("select *from arkadasNetwork where id='" + girilenNumara.ToString() + "'", conn);

            SqlDataReader dr = komut.ExecuteReader();

            //List<string> arananNumaralar = new List<string>();
            ArrayList arananNumaralar = new ArrayList();
            while (dr.Read())
            {
                for (int i = 0; i < 11; i++)
                {
                    if (dr[i].ToString() != " ")
                    {
                        arananNumaralar.Add(dr[i].ToString());
                    }
                }
            }
            //dr yikapatmazsak aşağıda kullandığımı dataReader da hata alırız.
            dr.Close();

            int satir = ((90 - arananNumaralar.Count + 1)) / 2;
            int sutun = 16;
            //ogrenciProfiMatris aratılan numaraların arkadaşlarını ve onların arkadaşProfil den puanlarını atmak için kullanılacak.
            //satir + arananNumaralar.Count-1 kendimi eklemeyeceğim için -1 diyorum 
            //örneğin 9 arkadaşım varsa 90-9=81 81-1=80 (-1 girilen numara) 80/2=40;....
            String[,] ogrenciProfilMatris = new String[satir + arananNumaralar.Count - 1, sutun];
            int sayac = arananNumaralar.Count;
            //bu forda sadece numaralar 1. sutuna atılır.
            for (int i = 0; i < sayac; i++)
            {
                if (i + 1 < arananNumaralar.Count)
                {
                    ogrenciProfilMatris[i, 0] = arananNumaralar[i + 1].ToString();
                }
            }
            //arkasProfil vt sinden puanları alıp matrise atılcak.(arkdas sayısı kadar dönecek.) 
            for (int i = 1; i < arananNumaralar.Count; i++)
            {
                SqlCommand arkadasProfilCekme = new SqlCommand("select *from ogrenciProfil where id='" + arananNumaralar[i] + "'", conn);
                SqlDataReader arkadasProfilDR = arkadasProfilCekme.ExecuteReader();

                ArrayList profilPuan = new ArrayList();

                while (arkadasProfilDR.Read())
                {
                    for (int q = 0; q < sutun; q++)
                    {
                        //puanları gecici bir arraylist e atıyorum.
                        profilPuan.Add(arkadasProfilDR[q].ToString());
                    }
                }
                //puanları ogrenciProfilMaris ine ekler.
                for (int b = 0; b < sutun; b++)
                {
                    ogrenciProfilMatris[i - 1, b] = profilPuan[b].ToString();
                }
                arkadasProfilDR.Close();
            }

            String[,] ogrenciNetworkMatris = new String[90, 16];

            SqlCommand ilkYarisiniAlma = new SqlCommand("select * from ogrenciProfil ", conn);
            SqlDataReader ilkYarisiniDR = ilkYarisiniAlma.ExecuteReader();

            int xx = 0;
            while (ilkYarisiniDR.Read())
            {
                //ogrenciProfil in hepsini matrise atadık.             
                for (int j = 0; j < 16; j++)
                {
                    ogrenciNetworkMatris[xx, j] = ilkYarisiniDR[j].ToString();
                }
                xx++;
            }
            //-1 in amacı arananNumaralar.count diyince 1 fazla alıyor.
            int x = arananNumaralar.Count - 1;
            for (int i = 0; i < 90; i++)
            {
                int sayac1 = 0;
                for (int j = 0; j < arananNumaralar.Count; j++)
                {
                    if (ogrenciNetworkMatris[i, 0].ToString() == arananNumaralar[j].ToString())
                    {
                        sayac1++;
                        break;
                    }
                }
                if (sayac1 == 0)
                {
                    for (int y = 0; y < sutun; y++)
                    {
                        ogrenciProfilMatris[x, y] = ogrenciNetworkMatris[i, y].ToString();
                    }
                    x++;
                }
                if (x == satir + arananNumaralar.Count - 1)
                {
                    break;
                }
            }
            //bu kısımda arkadaşları ve arkadaş olmayan ilk yarıyı aldıkdan sonra arta kalan kısmı matrise atacağız.
            int yeniSatir = 90 - satir - arananNumaralar.Count;
            int yeniSutun = 16;
            //+1 in amacı etiket kullanmak için
            String[,] artanOgrenci = new String[yeniSatir, yeniSutun + 1];
            int indis = 0;                                                    //matrsin satırı için kullanıldı
            for (int i = 0; i < 90; i++)
            {
                int sayac2 = 0;
                for (int j = 0; j < satir + arananNumaralar.Count - 1; j++)
                {
                    if (ogrenciNetworkMatris[i, 0].ToString() == ogrenciProfilMatris[j, 0].ToString())
                    {
                        sayac2++;
                        break;
                    }
                }
                if (sayac2 == 0)
                {
                    if (arananNumaralar[0].ToString() != ogrenciNetworkMatris[i, 0].ToString())
                    {
                        for (int y = 0; y < yeniSutun; y++)
                        {
                            artanOgrenci[indis, y] = ogrenciNetworkMatris[i, y].ToString();
                        }
                        artanOgrenci[indis, 16] = "0";
                        indis++;
                    }
                }
                if (indis == yeniSatir)
                {
                    break;
                }
            }
            //ogrenciProfilMatris de arkadaş olanlarda denk gelecek şekilde etiket matrisi oluşturulacak.
            //aranan kişinin arkadaşı ise "1" değil ise "0" etiketini atacağız.
            double[] etiket = new double[satir + arananNumaralar.Count - 1];

            for (int i = 0; i < satir; i++)
            {
                //arkadaş sayısı kadar 1 atacağız
                if (i < arananNumaralar.Count - 1)
                {
                    etiket[i] = 1;
                }
                //arkadaş olmayan kısım.
                else
                {
                    etiket[i] = 0;
                }
            }
            //bu  kısımdan sonra regresyon ile ilgilidir.
            int n = satir + arananNumaralar.Count - 1;
            int nArkadaşOlmayan = 90 - satir - arananNumaralar.Count;
            int maxIterSayisi = 100;
            double stepSize = 0.001;
            double[] beta = new double[16];

            //betayı biz verilen formülde olduğu gibi 1 ile dolduruyorum
            for (int i = 0; i < beta.Length; i++)
            {
                beta[i] = 1;
            }
            double[] newBeta = new double[16];
            double toplam1 = 0.0;
            double toplam2 = 0.0;

            for (int i = 1; i < maxIterSayisi; i++)
            {
                toplam1 = 0.0;
                for (int j = 0; j < n; j++)
                {
                    int[] x1 = tempFunc(ogrenciProfilMatris, j);
                    double hx = Math.Exp(-(beta[0] + beta[1] * x1[1] + beta[2] * x1[2] + beta[3] * x1[3] + beta[4] * x1[4] + beta[5] * x1[5] +
                        beta[6] * x1[6] + beta[7] * x1[7] + beta[8] * x1[8] + beta[9] * x1[9] + beta[10] * x1[10] + beta[11] * x1[11] + beta[12] * x1[12] +
                        beta[13] * x1[13] + beta[14] * x1[14] + beta[15] * x1[15]));
                    double hb = 1.0 / (1.0 + hx);
                    double y = etiket[j];//Arakadaşımı ,değilmiyi etikette tutuyoruz.

                    toplam1 += hb - y;
                }
                //betanın yeni değerini tuttuk.
                newBeta[0] = beta[0] - (stepSize * toplam1 / n);
                for (int q = 1; q <= 15; q++)
                {
                    toplam2 = 0.0;
                    for (int a = 0; a < n; a++)
                    {
                        int[] x1 = tempFunc(ogrenciProfilMatris, a);
                        double hb = 1.0 / (1.0 + Math.Exp(-(beta[0] + beta[1] * x1[1] + beta[2] * x1[2] + beta[3] * x1[3] + beta[4] * x1[4] + beta[5] * x1[5] +
                           beta[6] * x1[6] + beta[7] * x1[7] + beta[8] * x1[8] + beta[9] * x1[9] + beta[10] * x1[10] + beta[11] * x1[11] + beta[12] * x1[12] +
                           beta[13] * x1[13] + beta[14] * x1[14] + beta[15] * x1[15])));
                        double y = etiket[a];
                        toplam2 += (hb - y) * x1[q];
                    }
                    //beta 0 hariç diğer betaların değerini tutuk.
                    newBeta[q] = beta[q] - (stepSize * toplam2 / n);
                }
                for (int s = 0; s < beta.Length; s++)
                {
                    beta[s] = newBeta[s];
                }
            }
            //son beta değerlerini bulduk.
            for (int i = 0; i < nArkadaşOlmayan; i++)
            {
                int[] x1 = tempFunc(artanOgrenci, i);
                double hb = 1.0 / (1.0 + Math.Exp(-(beta[0] + beta[1] * x1[1] + beta[2] * x1[2] + beta[3] * x1[3] + beta[4] * x1[4] + beta[5] * x1[5] +
                   beta[6] * x1[6] + beta[7] * x1[7] + beta[8] * x1[8] + beta[9] * x1[9] + beta[10] * x1[10] + beta[11] * x1[11] + beta[12] * x1[12] +
                   beta[13] * x1[13] + beta[14] * x1[14] + beta[15] * x1[15])));
                if (artanOgrenci[i, 16] == "0")
                {
                    artanOgrenci[i, 16] = hb.ToString();
                }
            }

            //regresyon puanı yüksek olan ilk 10 kişiyi matrise atadım.
            double[,] ilkOnKisi = new double[10, 2];
            double[,] siralanmisMatris = new double[10, 2];
            int arttir = 0;
            int sayacc = 0;

            for (int i = 0; i < nArkadaşOlmayan; i++)
            {
                arttir = arttir + 16;
                if (Convert.ToDouble(artanOgrenci[i, arttir]) == 1.0)
                {
                    arttir -= 16;
                    for (int j = 0; j < 2; j++)
                    {

                        siralanmisMatris[sayacc, j] = Convert.ToDouble(artanOgrenci[i, arttir]);
                        arttir += 16;
                    }
                    sayacc++;
                }
                arttir = 0;
                if (sayacc == 10)
                    break;
            }
            ilkYarisiniDR.Close();
            conn.Close();

            //bulunan arkadaşları vt ye bağlanıp ad-soyad adını ve tekrardan numaralarını ekrana basaağım.
            for (int i = 0; i < 10; i++)
            {
                conn.Open();
                SqlCommand onerilinArkadasAl = new SqlCommand("select * from ogrenciListesi where id='" + siralanmisMatris[i, 0] + "'", conn);
                SqlDataReader dtr = onerilinArkadasAl.ExecuteReader();

                while (dtr.Read())
                {
                    String listNo = dtr[0].ToString();
                    String listAdSoyad = dtr[1].ToString();
                    listView1.Items.Add(dtr[0].ToString() + "       " + dtr[1].ToString());
                }
                dtr.Close();
                conn.Close();
            }
            //textbox ı temizleme
            textBox1.Clear();

        }

        //from kapatma
        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            this.Close();
        }
        //form u büyültme
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
        }
        //formu alta atma
        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //bu kısımda panel hareketi kontrolü yapılmaktadır.
        bool mouseDown = false;
        Point lastLocation;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseDown == false && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouseDown = true;
                lastLocation = e.Location;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //contextMenuStrip1.Show(this.DesktopLocation.X + e.X, this.DesktopLocation.Y + e.Y);	
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(15, 15, 15);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.SetDesktopLocation(this.DesktopLocation.X - lastLocation.X + e.X, this.DesktopLocation.Y - lastLocation.Y + e.Y);
                this.Update();
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }     
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {    
            //textBoxa metin girişini engelledik.
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            //textde 10 dan fazla karakter girmemesi için 
            if (textBox1.TextLength == 10)
            {
                e.Handled = true;
            }     
        }
    }
}
