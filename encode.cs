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
    public partial class Form2 : Form
    {
        int lenght = 0;
        int size = 256;
        int totallenght = 0;
        int squarelength = 0;
        int sizeofbit = 0;
        string horijontal = "";
        string vertical = "";
        int line = 0;
        private static Bitmap bitmap = null;
        private static Bitmap bitmap1 = null;
        private static Bitmap bitmapf = null;
        void createimage()
        {


            var b = new Bitmap(1, 1);
           // b.SetPixel(0, 0, Color.White);
            
            bitmap = new Bitmap(b, size, size);
            var b1 = new Bitmap(1, 1);
            // b.SetPixel(0, 0, Color.White);

             bitmap1 = new Bitmap(b, size, size);
            Pen pen = new Pen(Color.Black, sizeofbit/3);
            Pen pen1 = new Pen(Color.Red, sizeofbit / 3);
           // Brush b = new Brush();
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Transparent);
            int x =  sizeofbit / 3;
            int y = 0;
            int i= 0;
            foreach (char c in vertical)
            {
               

                if (c == '1') 
                g.DrawLine(pen, x, y,x,y+sizeofbit);

             //   g.DrawString(c.ToString(), new System.Drawing.Font("Tahoma", 32, FontStyle.Bold), Brushes.Red, x, y);
                y = y + sizeofbit;


                if (y >= (size - sizeofbit))
                {
                    //g.DrawString(c.ToString(), new System.Drawing.Font("Tahoma", 32, FontStyle.Bold), Brushes.Red, x, y);

                    y = 0;
                    i = 0;
                    x = x + sizeofbit;
                }
                i++;
            }
            pictureBox1.Image = bitmap;

            ////////////////////////////

             g = Graphics.FromImage(bitmap1);
            g.Clear(Color.Transparent);
            y = sizeofbit / 3; ; ;
             x =0;
             i = 0;
            foreach (char c in horijontal)
            {
               

                if (c == '1')
                    g.DrawLine(pen, x, y, x+ sizeofbit, y );

             //   g.DrawString(c.ToString(), new System.Drawing.Font("Tahoma", 32, FontStyle.Bold), Brushes.Red, x, y);
               x = x + sizeofbit;

                if (x >= (size- sizeofbit))
                {
                 //   g.DrawString(c.ToString(), new System.Drawing.Font("Tahoma", 32, FontStyle.Bold), Brushes.Red, x, y);

                   x = 0;
                    i = 0;
                    y= y+ sizeofbit;
                }
                i++;
            }
            pictureBox2.Image = bitmap1;
            g.Dispose();
          
        }

        void createimage2()
        {


            var b = new Bitmap(1, 1);


            bitmapf = new Bitmap(b, size+ sizeofbit, size+ sizeofbit);
            var b1 = new Bitmap(1, 1);
          
            Pen pen = new Pen(Color.Black, sizeofbit / 3);
            Pen pen1 = new Pen(Color.Red, sizeofbit / 3);
            // Brush b = new Brush();
            Graphics g = Graphics.FromImage(bitmapf);
            g.Clear(Color.White);
            int x = sizeofbit+sizeofbit / 3;
            int y = sizeofbit;
            int i = 0;
            foreach (char c in vertical)
            {


               // if (c == '1')
               //     g.DrawLine(pen, x, y, x, y + sizeofbit);

               y = y + sizeofbit;


                if (y >= (size - sizeofbit))
                {
                    
                    y = 0;
                    i = 0;
                    x = x + sizeofbit;
                }
                i++;
            }
     
            y = sizeofbit+sizeofbit / 3; ; ;
            x = sizeofbit;
            i = 0;
            foreach (char c in horijontal)
            {


             //   if (c == '1')
                //    g.DrawLine(pen, x, y, x + sizeofbit, y);
                    x = x + sizeofbit;

                if (x >= (size - sizeofbit))
                {
                  
                    x = 0;
                    i = 0;
                    y = y + sizeofbit;
                }
                i++;
            }
            g.FillRectangle(Brushes.Black, 0, 0, sizeofbit, sizeofbit);
            g.DrawImage(bitmap, new Point(sizeofbit, sizeofbit));
            g.DrawImage(bitmap1, new Point(sizeofbit, sizeofbit));
            pictureBox3.Image = bitmapf;
            bitmapf.Save("test"+DateTime.Now.Second+DateTime.Now.Minute+".jpg");
            g.Dispose();
          
        }



        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = StringToBinary(textBox1.Text);
           // string result = Encoding.ASCII.GetString(data); ;
            
            textBox1.Text = result;

         
          
          lenght = result.Length;
            horijontal = result.Substring(0, lenght / 2) +"1";
            vertical = result.Substring(lenght / 2)+"1";//, textBox1.Text.Length-1);

           int horizontallegth = horijontal.Length;
           int  verticallength = vertical.Length;
            totallenght = (horizontallegth > verticallength) ? horizontallegth : verticallength;
            double valu = Math.Sqrt(totallenght);
            double sqrlength = Math.Ceiling(valu);
            squarelength = (int)sqrlength;
            sizeofbit = size / squarelength;
            line = (int)Math.Ceiling(32.0 / sizeofbit);
            createimage();
               createimage2();
            //MessageBox.Show(squarelength + " " + valu+ " " + lenght);
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
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

    }
}
