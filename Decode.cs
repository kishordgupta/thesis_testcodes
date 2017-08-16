using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Decode : Form
    {
        int size = 0;
        int bitsize = 0;
        Bitmap b = null;
        string horijontal = "";
        string vertical = "";
        public Decode()
        {
            InitializeComponent();
            button1.Enabled = false;
        }
        public static Bitmap resizeImage(Bitmap imgToResize, Size size)
        {
            return (new Bitmap(imgToResize, size));
        }
        private void bpload_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
             b = null;
             horijontal = "";
             vertical = "";
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    b = (Bitmap)Image.FromFile(file);// resizeImage(new Bitmap(file),new Size(1024, 1024));
                    pictureBox2.Image = b;// new Bitmap(file);
                    button1.Enabled = true;
                }
                catch (System.IO.IOException)
                {
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            var watch = System.Diagnostics.Stopwatch.StartNew();
            System.Drawing.Imaging.BitmapData objectsData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
             System.Drawing.Imaging.ImageLockMode.ReadOnly, b.PixelFormat);
           
            AForge.Imaging.UnmanagedImage grayImage = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(new AForge.Imaging.UnmanagedImage(objectsData));
            AForge.Imaging.Filters.Threshold th = new AForge.Imaging.Filters.Threshold(128);
            th.ApplyInPlace(grayImage);
          
            // unlock image
            b.UnlockBits(objectsData);
            pictureBox2.Image = grayImage.ToManagedImage(); ;
            Pen pen = new Pen(Color.Red, 2);
            Bitmap img = grayImage.ToManagedImage();
            Graphics g = Graphics.FromImage(b);
            string text = "0";
            int z = 0;
           
        
            {
                for (int j = 1; j < img.Height; j++)
                {
                    int pixel = img.GetPixel(1, j).R;
                    if (pixel == 255)
                    {
                        z = j;
                        break;
                    }
                  //  g.DrawString(j.ToString(), new System.Drawing.Font("Tahoma", 12, FontStyle.Bold), Brushes.Red, i, j);

                 //  g.DrawLine(pen, i, j, i+1, j+1 );
                }
             
              //  if (z!=0)
             //   break;
            //    text = text + "\n";
            }
            g.DrawRectangle(pen, 0, 0, z, z);
            bitsize = z;
            int l = bitsize % 3;
            
            int lastblack = 0;
            int lastwhite = 0;
            for (int i = bitsize +bitsize/3; i < img.Width; )
            {
                int ptb = 0;
                int ptw = 0;
                int bitsizecount = 0;
                for (int j = bitsize ; j < img.Height; j++)
                {
                    bitsizecount++;
                //   g.DrawLine(pen, i, i, i+1, j );
                    int pixel = img.GetPixel(i, j).R;
                    if (pixel == 255)
                    {
                        ptb++;
                        ptw = 0;
                        
                    }
                    if (pixel == 0)
                    {
                        ptb=0;
                        ptw++;

                    }
                    if (bitsize == bitsizecount)
                    {
                        if (ptb >= ptw)
                            horijontal = horijontal + 0;
                        else
                            horijontal = horijontal + 1;
                        bitsizecount = 0;
                        ptb = 0;
                         ptw = 0;
                    }

                }
                i = i + bitsize;
                //if (i % 15 == 0)
                //    i = i + bitsize - 2;
                //else if (i % 3 == 0)
                //    i = i + bitsize + 1;
                //else if (i % 5 == 0)
                //    i = i + bitsize + 1;
                //else
                //    i = i + bitsize +bitsize%3 ;
            }

            for (int j = bitsize   + bitsize / 3; j < img.Height;)
            {
                int ptb = 0;
                int ptw = 0;
                int bitsizecount = 0;
                for (int i = bitsize ; i < img.Height; i++)
                {
                    bitsizecount++;
              //  g.DrawLine(pen, i, j, i, j + 1);
                    int pixel = img.GetPixel(i, j).R;
                    if (pixel == 255)
                    {
                        ptb++;
                        ptw = 0;

                    }
                    if (pixel == 0)
                    {
                        ptb = 0;
                        ptw++;

                    }
                    if (bitsize == bitsizecount)
                    {
                        if (ptb > ptw)
                            vertical = vertical + 0;
                        else
                            vertical = vertical + 1;
                        bitsizecount = 0;
                        ptb = 0;
                        ptw = 0;
                    }

                }
                 j = j + bitsize ;
               // if (j % 15 == 0)
               //     j = j + bitsize -2;
               //else if (j % 3 == 0)
               //     j = j + bitsize + 1;
               // else if (j % 5 == 0)
               //     j = j + bitsize + 1;
               // else
               //     j = j + bitsize + bitsize % 3;
            }


            pictureBox2.Image = b;


           int la = horijontal.LastIndexOf('1');
           horijontal = horijontal.Substring(0, la);
            int lb = vertical.LastIndexOf('1');
           vertical = vertical.Substring(0, lb);
            String s1 = horijontal + "" + vertical;
            String s2 = vertical + "" + horijontal;
            int f = 8- s2.Length % 8;
            for (int k = 0; k < f; k++)
            {
                s2 = s2+ "0";
                s1 = s1 + "0";
            }
            watch.Stop();
            richTextBox2.Text = "Result Binarycode: " + vertical + horijontal;
            richTextBox1.Text = "Result TEXT: " + BinaryToString(vertical + horijontal);// + BinaryToString(s2);// +" - " + BinaryToString(horijontal);
            label2.Text = "Bitsize: " + bitsize + "  Datalength " + s2.Length + "  Decoding time: " + watch.ElapsedMilliseconds.ToString() + "Milisecond";
        }
        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                try
                {
                    byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
                }
                catch (Exception e)
                { }
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
       
    }
}
