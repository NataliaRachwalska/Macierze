using Macierze;
using System.Windows.Forms;
namespace Obrazy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Bitmap? img;
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            if (file != null)
            {
                img = new Bitmap(file);
                pictureBox1.Image = img;
            }
        }
        public void Odcienie_szarosci(Bitmap? img)
        {
                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color piksel_kolor = img.GetPixel(i, j);
                        int wartosc = (piksel_kolor.R + piksel_kolor.G + piksel_kolor.B) / 3;
                        img.SetPixel(i, j, Color.FromArgb(piksel_kolor.A, wartosc, wartosc, wartosc));
                    }
                }
                pictureBox2.Image = img;
            
        }
        public void Wykrywanie_krawedzi(Bitmap? img)
        {
              for (int i = 0; i < img.Width - 1; i++)
              {
                for (int j = 0; j < img.Height - 1; j++)
                {
                    Color piksel_kolor_1 = img.GetPixel(i, j);
                    Color piksel_kolor_2 = img.GetPixel(i + 1, j + 1);
                    Color piksel_kolor_3 = img.GetPixel(i + 1, j);
                    Color piksel_kolor_4 = img.GetPixel(i, j + 1);

                    int wartosc = Math.Abs(piksel_kolor_1.R - piksel_kolor_2.R) +
                                Math.Abs(piksel_kolor_1.G - piksel_kolor_2.G) +
                                Math.Abs(piksel_kolor_1.B - piksel_kolor_2.B) +
                                Math.Abs(piksel_kolor_3.R - piksel_kolor_4.R) +
                                Math.Abs(piksel_kolor_3.G - piksel_kolor_4.G) +
                                Math.Abs(piksel_kolor_3.B - piksel_kolor_4.B);
                    wartosc = Math.Min(255, Math.Max(0, wartosc));
                    img.SetPixel(i, j, Color.FromArgb(piksel_kolor_1.A, wartosc, wartosc, wartosc));
                }
              }
              pictureBox3.Image = img;
       
        }
        public void Negatyw (Bitmap? img)
        {
                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color piksel_kolor = img.GetPixel(i, j);
                        int wartosc_R = 255 - piksel_kolor.R;
                        int wartosc_G = 255 - piksel_kolor.G;
                        int wartosc_B = 255 - piksel_kolor.B;
                        img.SetPixel(i, j, Color.FromArgb(piksel_kolor.A, wartosc_R, wartosc_G, wartosc_B));
                    }
                }
                pictureBox4.Image = img;
    
        }
        public void Progowanie(Bitmap? img)
        {
                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color piksel_kolor = img.GetPixel(i, j);
                        int jasnosc = (piksel_kolor.R + piksel_kolor.G + piksel_kolor.B) / 3;
                        if (jasnosc >= 120)
                        {
                            img.SetPixel(i, j, Color.White);
                        }
                        else
                        {
                            img.SetPixel(i, j, Color.Black);
                        }
                    }
                }
                pictureBox5.Image = img;
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var file = openFileDialog1.FileName;
            if (file != null)
            {
                img = new Bitmap(file);
                Thread[] threads = new Thread[4];
                threads[0] = new Thread(() => Odcienie_szarosci((Bitmap)img.Clone()));
                threads[1] = new Thread(() => Wykrywanie_krawedzi((Bitmap)img.Clone()));
                threads[2] = new Thread(() => Negatyw((Bitmap)img.Clone()));
                threads[3] = new Thread(() => Progowanie((Bitmap)img.Clone()));
                foreach (Thread thread in threads) thread.Start();              
                foreach (Thread x in threads) x.Join();
            }
        }
    }
}
