using System.Text;

namespace Stenografi_Final_CebirselSifreleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        String str = "";
        string crp = "";
        private void button1_Click(object sender, EventArgs e)
        {
            //iþle
            Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();
            crp = "";    
            str = richTextBox1.Text;
            //str = "In order to embed more encrypted data and obtain lesser degeneration in the image, embedding was performed by changing 2 least significant bits (4 intotal) of the Green and Blue (G and B) channels ofthe cover-image RGB channels through a K-bit LSBalgorithm. Sensing of the colours is related to theamount of light in a given wave length. The humaneye detects light whose wave length is between 370-770 nm. The linear order of the wave lengths formsthe colour spectrum. The colour sense which thesewave lengths form in the visual system are red - 620nm, green - 530 nm and blue - 470 nm of the spectralcolours. The detection of the spectrum is calledspectral sense (Griffin, 2009). In addition, the numberof color sensors (cones) is different from the numberof luminance sensors (rods) in the human eye (rods >cones). Thus, the light sensitivity and color sensitivityof the human vision system (HVS) are different fromeach other (Yalman and Erturk., 2013; Koçak, 2015).In consideration of the spectral sensitivity of the eye,the Green (G) and the Blue (B) bits of the image,which are low, are used and by reasons of the sensitivity in the red colour, the R channel was not used.";
            
            
            
            richTextBox1.Text = str;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(str);


            int sayi = 0, yuzler = 0, onlar = 0, birler = 0;
            int[] xy = new int[str.Length + 100];
            int[] yz = new int[str.Length + 100];
            int[] xyz = new int[str.Length + 100];
            for (int i = 0; i < str.Length; i++)
            {
                sayi = Convert.ToInt32(asciiBytes[i]);
                yuzler = sayi / 100;
                sayi = sayi - (yuzler * 100);
                onlar = sayi / 10;
                sayi = sayi - (onlar * 10);
                birler = sayi;
                xy[i] = yuzler + onlar;
                yz[i] = onlar + birler;
                xyz[i] = yuzler + onlar + birler;

            }
            string[] c = new string[str.Length * 6];
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (xy[i] < 10)
                {
                    c[j] += "0";
                    c[j] += xy[i];
                }
                else
                    c[j] += xy[i];

                j++;
                if (yz[i] < 10)
                {
                    c[j] += "0";
                    c[j] += yz[i];
                }
                else
                    c[j] += yz[i];
                j++;
                if (xyz[i] < 10)
                {
                    c[j] += "0";
                    c[j] += xyz[i];
                }
                else
                    c[j] += xyz[i];
                j++;

            }

            for (int i = 0; i < str.Length * 3; i++)
            {
                crp += c[i];
            }

            int say = 0;
            for (int y = 0; y < 256; y++)
            {
                for (int x = 0; x < 256; x++)
                {

                    Color pixel = bmp.GetPixel(x, y);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;

                    r = r - (r % 10);
                    g = g - (g % 10);
                    b = b - (b % 10);


                    r = r + Convert.ToInt32(crp.Substring(say, 1));
                    say++;
                    g = g + Convert.ToInt32(crp.Substring(say, 1));
                    say++;
                    b = b + Convert.ToInt32(crp.Substring(say, 1));
                    say++;
                    if (r > 255)
                    {
                        r = r - 10;
                    }
                    else if (g > 255)
                    {
                        g = g - 10;
                    }
                    else if (b > 255)
                    {
                        b = b - 10;
                    }

                    pixel = Color.FromArgb(r, g, b);
                    bmp.SetPixel(x, y, pixel);

                    if (say == c.Length) break;
                }
                if (say == c.Length) break;
            }

            pictureBox2.Image = bmp;
            pictureBox2.Size = bmp.Size;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //çöz                   
            richTextBox2.Text = "";
            string yenitext = "";
            Bitmap bmp = (Bitmap)pictureBox2.Image.Clone();


            int say = 0;
            for (int y = 0; y < 256; y++)
            {
                for (int x = 0; x < 256; x++)
                {

                    Color pixel = bmp.GetPixel(x, y);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;

                    int ronlar = 0;
                    int gonlar = 0;
                    int bonlar = 0;

                    ronlar = r % 10;
                    gonlar = g % 10;
                    bonlar = b % 10;

                    yenitext += ronlar.ToString() + gonlar.ToString() + bonlar.ToString();

                    say++;
                    if (say == richTextBox1.Text.Length * 2) break;
                }
                if (say == richTextBox1.Text.Length * 2) break;
            }

            int yuzler, onlar, birler, xy, yz, xyz;
            int ss = 0;
            for (int i = 0; i < richTextBox1.Text.Length; i++)
            {
                xy = (Convert.ToInt32(yenitext.Substring(ss, 1)) * 10) + Convert.ToInt32(yenitext.Substring(ss + 1, 1));
                ss += 2;
                yz = (Convert.ToInt32(yenitext.Substring(ss, 1)) * 10) + Convert.ToInt32(yenitext.Substring(ss + 1, 1));
                ss += 2;
                xyz = (Convert.ToInt32(yenitext.Substring(ss, 1)) * 10) + Convert.ToInt32(yenitext.Substring(ss + 1, 1));
                ss += 2;

                yuzler = xyz - yz;
                onlar = xy - yuzler;
                birler = yz - onlar;

                int toplam = yuzler * 100 + onlar * 10 + birler;


                char character = (char)toplam;
                string text = character.ToString();

                richTextBox2.Text += text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"BMP|*.bmp" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image.Save(saveFileDialog.FileName);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = @"BMP|*.bmp" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.Size = Image.FromFile(openFileDialog.FileName).Size;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //fark hesapla
            double[] MSE = new double[3];
            double[] PSNR = new double[3];

            for (int i = 0; i < 3; i++)
            {
                MSE[i] = 0;
                PSNR[i] = 0;
            }

            int x, y; Color pixel1, pixel2;
            int dif1 = 0, dif2 = 0, dif3 = 0;

            Bitmap bmp1 = (Bitmap)pictureBox1.Image.Clone();
            Bitmap bmp2 = (Bitmap)pictureBox2.Image.Clone();

            for (y = 0; y < 256; y++)
            {
                for (x = 0; x < 256; x++)
                {
                    pixel1 = bmp1.GetPixel(x, y);
                    pixel2 = bmp2.GetPixel(x, y);
                    dif1 = dif1 + (pixel1.R - pixel2.R) * (pixel1.R - pixel2.R);
                    dif2 = dif2 + (pixel1.G - pixel2.G) * (pixel1.G - pixel2.G);
                    dif3 = dif3 + (pixel1.B - pixel2.B) * (pixel1.B - pixel2.B);
                }
            }

            MSE[0] = (double)dif1 / (bmp1.Height * bmp1.Width);
            MSE[1] = (double)dif2 / (bmp1.Height * bmp1.Width);
            MSE[2] = (double)dif3 / (bmp1.Height * bmp1.Width);
            PSNR[0] = 20 * Math.Log10(255 / Math.Sqrt(MSE[0]));
            PSNR[1] = 20 * Math.Log10(255 / Math.Sqrt(MSE[1]));
            PSNR[2] = 20 * Math.Log10(255 / Math.Sqrt(MSE[2]));

            textBox1.Text = "R: " + MSE[0];
            textBox2.Text = "G: " + MSE[1];
            textBox3.Text = "B: " + MSE[2];

            textBox4.Text = "R: " + PSNR[0];
            textBox5.Text = "G: " + PSNR[1];
            textBox6.Text = "B: " + PSNR[2];
        }
    }
}