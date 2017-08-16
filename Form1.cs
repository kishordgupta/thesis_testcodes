using AForge.Video.DirectShow;
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
    public partial class Form1 : Form
    {
        static int i = 0;
        string s = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {
            int i = 0;
            s = "";
            AForge.Video.DirectShow.FilterInfoCollection videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
                throw new ApplicationException();

            foreach (AForge.Video.DirectShow.FilterInfo device in videoDevices)
            {
                VideoCaptureDevice videoSource = new VideoCaptureDevice(device.MonikerString);
                videoSource.DesiredFrameSize = new Size(320, 240);
                videoSource.DesiredFrameRate = 15;
                videoSourcePlayer1.VideoSource = videoSource;
                videoSourcePlayer1.Start();
            }
        }

        
         private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            i++;
            Bitmap objectsImage = null;
            AForge.Imaging.Filters.EuclideanColorFiltering filter = new AForge.Imaging.Filters.EuclideanColorFiltering();
            // set center colol and radius
            AForge.Imaging.RGB f = new AForge.Imaging.RGB(Color.Red);
            filter.CenterColor = f;// AForge.Imaging.RGB..Blue;
            filter.Radius = (short)100;
            // apply the filter
            objectsImage = image;
            filter.ApplyInPlace(image);

            // lock image for further processing
            System.Drawing.Imaging.BitmapData objectsData = objectsImage.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, image.PixelFormat);
          //  AForge.Imaging.Filters.Grayscale.
            // grayscaling
            AForge.Imaging.UnmanagedImage grayImage = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(new AForge.Imaging.UnmanagedImage(objectsData));

            // unlock image
            objectsImage.UnlockBits(objectsData);
            AForge.Imaging.BlobCounter blobCounter = new AForge.Imaging.BlobCounter();
            // locate blobs
         //   blobCounter.MinHeight = 100;
           // blobCounter.MinWidth = 60;
            blobCounter.ProcessImage(grayImage);
            blobCounter.ObjectsOrder = AForge.Imaging.ObjectsOrder.Size;
           Rectangle[] rects = blobCounter.GetObjectsRectangles();
           
            if (rects.Length > 0)
            {
                Rectangle objectRect = rects[0];

                // draw rectangle around derected object
                Graphics g = Graphics.FromImage(image);
                s = s+"\n" + i + " - top =  " + objectRect.Top + " bottom  " + objectRect.Bottom +" left "+ objectRect.Left + "  right " +objectRect.Right +"  " + objectRect.X + "   " + objectRect.Location.X +"  " + objectRect.Location.Y;
                using (Pen pen = new Pen(Color.FromArgb(160, 255, 160), 5))
                {
                    g.DrawRectangle(pen, objectRect);
                }
                g.Dispose();
                int objectX = objectRect.X + objectRect.Width / 2 - image.Width / 2;
                int objectY = image.Height / 2 - (objectRect.Y + objectRect.Height / 2);
               // System.Console.Out.
                // label1.Text = label1.Text+objectRect.X;
            //    ParameterizedThreadStart t = new ParameterizedThreadStart(p);
            //    Thread aa = new Thread(t);
            //      aa.Start(rects[0]);
            }
            Graphics g1 = Graphics.FromImage(image);
            Pen pen1 = new Pen(Color.FromArgb(160, 255, 160), 3);
            g1.DrawLine(pen1, image.Width / 2, 0, image.Width / 2, image.Width);
            g1.DrawLine(pen1, image.Width, image.Height / 2, 0, image.Height / 2);
            g1.Dispose();
        }

        

        private void videoSourcePlayer1_PlayingFinished(object sender, AForge.Video.ReasonToFinishPlaying reason)
        {
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("write.txt"))
            {
                writetext.WriteLine(s);
            }
        }

        private void videoSourcePlayer2_Click(object sender, EventArgs e)
        {
            AForge.Video.DirectShow.FilterInfoCollection videoDevices = new AForge.Video.DirectShow.FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
                throw new ApplicationException();

           // foreach (AForge.Video.DirectShow.FilterInfo device in videoDevices)
          //  {
                VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
               // videoSource.DesiredFrameSize = new Size(320, 240);
               // videoSource.DesiredFrameRate = 15;
                videoSourcePlayer2.VideoSource = videoSource;
                videoSourcePlayer2.Start();
         //   }
        }
    }
}
