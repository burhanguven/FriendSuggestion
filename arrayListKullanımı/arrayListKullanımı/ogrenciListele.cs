using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace arrayListKullanımı
{
    public partial class ogrenciListele : Form
    {
        public ogrenciListele()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=CAN;Initial Catalog=arkadasOneri;Integrated Security=True");

        //öğrenci numarası ve adının soyadının tutuludu excelden okuyum vt ye atma
        public void ogrenciListesiDB()
        {
            DataSet dataSet;
            OleDbConnection xlsxbaglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Burhan\\Desktop\\arkadasoneri\\arrayListKullanımı\\ogrencilistesi.xlsx; Extended Properties='Excel 12.0 Xml;HDR=YES'"); //excel_dosya.xlsx kısmını kendi excel dosyanızın adıyla değiştirin.

            DataTable tablo = new DataTable();//Verileri direkt datagrid'e çekmek için DataTable kodunu tanımlıyoruz.
            xlsxbaglanti.Open(); //Excel dosyamızığın bağlantısını açıyoruz.
            tablo.Clear(); //En üstte tanımladığımız Datatable değişkenini temizliyoruz.
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", xlsxbaglanti);
            //OleDbDataAdapter ile excel dosyamızdaki verileri listeliyoruz. 
            //Burada önemli olan kısım sorgu cümleciğinde ki YeniSayfa$ kısmı yerine excel dosyasındaki ismi yazmanız gerek. Bu isim excel dosyanızı açtığınızda en altta yazan isimdir. 
            //Eğer değiştirmediyseniz zaten Sayfa1 olarak yazar. Ayrıca " $ " simgesi ve köşeli parentezleri ellememeniz gerek.
            da.Fill(tablo); //Gelen sonuçları datatable'a gönderiyoruz.

            dataGridView1.DataSource = tablo; //datatable'da ki verileri datagrid'de listeliyoruz.

            //burada for dongusu ile kisileri veritabanina ekliyoruz.
            conn.Open();

            for (int i = 0; i < 90; i++)
            {
                SqlCommand sqlCommand = new SqlCommand("insert into ogrenciListesi(id,adSoyad) values('" + dataGridView1.Rows[i].Cells["Öğrenci Numaranızı giriniz:"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Adınızı ve soyadınızı giriniz:"].Value.ToString() + "')", conn);
                sqlCommand.ExecuteNonQuery();
            }
            conn.Close();

            xlsxbaglanti.Close();
        }
        
        private void ogrenciListele_Load(object sender, EventArgs e)
        {   
            ogrenciListesiDB();
        }

        //from kapatma
        private void button1_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
        //from büyültme 
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
        }
        //Form küçültme
        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //panle hareketi
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

    }
}
