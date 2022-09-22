using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace int_to_bmp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Bytebeat by dharmatech
            // dharmatech/byte-beat.cs
            //
            // https://gist.github.com/dharmatech/6cb8fb83e46d56a1cc37f32e42559714

            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);

                writer.Write("RIFF".ToCharArray());    
                writer.Write((UInt32)0);               
                writer.Write("WAVE".ToCharArray());   

                writer.Write("fmt ".ToCharArray());    
                writer.Write((UInt32)16);              
                writer.Write((UInt16)1);               

                var channels = 1;
                var sample_rate = 11025;
                var bits_per_sample = 8;

                writer.Write((UInt16)channels);
                writer.Write((UInt32)sample_rate);
                writer.Write((UInt32)(sample_rate * channels * bits_per_sample / 8));   
                writer.Write((UInt16)(channels * bits_per_sample / 8));                 
                writer.Write((UInt16)bits_per_sample);

                writer.Write("data".ToCharArray());

                var seconds = 60;

                var data = new byte[sample_rate * seconds];

                for (var t = 0; t < data.Length; t++)
                    data[t] = (byte)(
                        Math.Sqrt(t * (t >> 9 | 9)) + Math.Tan(t ^ (int)Math.Sqrt(t * t * (t >> 10 & t >> 4)))
                        );

                writer.Write((UInt32)(data.Length * channels * bits_per_sample / 8));

                foreach (var elt in data) writer.Write(elt);

                writer.Seek(4, SeekOrigin.Begin);                           
                writer.Write((UInt32)(writer.BaseStream.Length - 8));   

                stream.Seek(0, SeekOrigin.Begin);

                new SoundPlayer(stream).PlayLooping();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private double ti;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Random random = new Random();
            int width = this.Width;
            int height = this.Height;
            int[] pixelArray = new int[width * height];  
            for (int tt = 0; tt < pixelArray.Length; tt++)
            {
                ti += 1;
                int t = (int)ti;
                pixelArray[tt] = (int)(
                    Math.Sqrt(t * (t >> 9 | 9)) + Math.Tan(t ^ (int)Math.Sqrt(t * t * (t >> 10 & t >> 4)))
                    );
            }

            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            BitmapData bmpData = bmp.LockBits(
                                new Rectangle(0, 0, bmp.Width, bmp.Height),
                                ImageLockMode.WriteOnly, bmp.PixelFormat);
            Marshal.Copy(pixelArray, 0, bmpData.Scan0, pixelArray.Length);

            bmp.UnlockBits(bmpData);
            pictureBox1.Image = bmp;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
