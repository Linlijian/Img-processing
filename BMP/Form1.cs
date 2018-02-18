using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap f_image = null;  //input image
        public Bitmap image;       //output image

        private void B_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileImage = new OpenFileDialog();
            OpenFileImage.Filter = "bitmap (*.bmp)|*.bmp";
            OpenFileImage.FilterIndex = 1;
            if (OpenFileImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (f_image != null)
                        f_image.Dispose();
                    f_image = (Bitmap)Bitmap.FromFile(OpenFileImage.FileName, false);

                }
                catch (Exception)
                {
                    MessageBox.Show("Can not open file”, “File Error");
                }
            }
            image = new Bitmap(f_image.Width, f_image.Height);

            //threshold
            int avg = 0;
            int sum = 0;
            int lastC = 0;

            // Loop through the images pixels
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {

                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    avg += C_gray;
                    sum += 1;

                    //check color
                    lastC = C_gray;

                }
            }
            //check color show
            label2.Text = lastC.ToString();

            int tmp = avg / sum;
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {

                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    if(C_gray >= tmp)
                    {
                        image.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        image.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                    
                }
            }
            textBox1.Text = tmp.ToString();
            pictureBox1.Image = f_image;
           // pictureBox2.Image = image;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            f_image.RotateFlip(RotateFlipType.Rotate90FlipXY);
            pictureBox1.Image = f_image;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            f_image.RotateFlip(RotateFlipType.Rotate270FlipXY);
            pictureBox1.Image = f_image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = image;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int y = 6;
            int s = 0;
            //for(int r = 1; r < y; r++)
            //{
                for (int i = 0; i < f_image.Width; i++)
                {
                    for (int j = 0; j < f_image.Height; j++)
                    {

                        Color PixelColor = f_image.GetPixel(i, j);
                        int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    // C_gray = 1 * C_gray ^ y * r;

                    C_gray = 1 * C_gray ^ y;
                    image.SetPixel(i, j, Color.FromArgb(C_gray, C_gray, C_gray));

                    }
                }
            //}

            label3.Text = s.ToString();
            pictureBox2.Image = image;
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            int y = 0;
            int b = 0;
            int d = 0;
            int max = 0;
            int min = 255;
            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {

                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    if (C_gray > max)
                    {
                        max = C_gray;
                    }
                    else if (C_gray < min)
                    {
                        min = C_gray;
                    }

                }
            }

            for (int i = 0; i < f_image.Width; i++)
            {
                for (int j = 0; j < f_image.Height; j++)
                {

                    Color PixelColor = f_image.GetPixel(i, j);
                    int C_gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                   
                    if (C_gray <= min)
                    {
                        image.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                    else if (C_gray >= max)
                    {
                        image.SetPixel(i, j, Color.FromArgb(max, max, max));
                    }
                    else
                    {
                        y = 255 * (C_gray - min) / (max - min);
                        //y = (C_gray - max) / (255 / min - max) + 0;
                        image.SetPixel(i, j, Color.FromArgb(y, y, y));
                    }

                    
                }
            }
            label1.Text = "MAX";
            label4.Text = "MIN";
            textBox1.Text = max.ToString();
            textBox2.Text = min.ToString();
            pictureBox2.Image = image;
        }
    }
}

