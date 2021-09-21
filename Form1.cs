using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace image_processing
{
    public partial class Form1 : Form
    {
        Bitmap loadImage, resultImage;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Open file with dialog
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        // Open file and load into memory and picturebox
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loadImage = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loadImage;
        }

        // Save file with dialog
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                resultImage.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception) { return; }
        }

        // Save Image
        private void saveStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        // Copy image to 2nd box
        private void copyImageButton(object sender, EventArgs e)
        {
            try
            {
                resultImage = new Bitmap(loadImage);
                pictureBox2.Image = resultImage;
            }
            catch (Exception) { return; }
        }

        // Greyscale
        private void greyScaleImage(object sender, EventArgs e)
        {
            try
            {
                resultImage = new Bitmap(loadImage);
            }
            catch (Exception) { return; }
            int w, h;
            w = loadImage.Width;
            h = loadImage.Height;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Color color = loadImage.GetPixel(i, j);
                    int grey = (color.R + color.G + color.B) / 3;

                    resultImage.SetPixel(i, j, Color.FromArgb(grey, grey, grey));
                }
            pictureBox2.Image = resultImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Invert image colors
        private void invertImageColors(object sender, EventArgs e)
        {
            try
            {
                resultImage = new Bitmap(loadImage);
            }
            catch (Exception) { return; }
            int w, h;
            w = loadImage.Width;
            h = loadImage.Height;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Color color = loadImage.GetPixel(i, j);
                    resultImage.SetPixel(i, j, Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B));
                }
            pictureBox2.Image = resultImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Sepia image
        private void sepiaImage(object sender, EventArgs e)
        {
            try
            {
                resultImage = new Bitmap(loadImage);
            }
            catch (Exception) { return; }
            int w, h;
            w = loadImage.Width;
            h = loadImage.Height;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Color color = loadImage.GetPixel(i, j);
                    int r, g, b;
                    r = (int)Math.Min((0.393 * color.R + 0.769 * color.G + 0.189 * color.B), 255);
                    g = (int)Math.Min((0.349 * color.R + 0.686 * color.G + 0.168 * color.B), 255);
                    b = (int)Math.Min((0.272 * color.R + 0.534 * color.G + 0.131 * color.B), 255);
                    resultImage.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            pictureBox2.Image = resultImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Generate histogram
        private void generateHistogram(object sender, EventArgs e)
        {
            try
            {
                resultImage = new Bitmap(loadImage);
            }
            catch (Exception) { return; }
            int w, h;
            w = loadImage.Width;
            h = loadImage.Height;
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Color color = loadImage.GetPixel(i, j);
                    int grey = (color.R + color.G + color.B) / 3;

                    resultImage.SetPixel(i, j, Color.FromArgb(grey, grey, grey));
                }
            
            Color ctemp;
            int[] histogram = new int[256];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    ctemp = resultImage.GetPixel(i, j);
                    histogram[ctemp.R]++;
                }

            // Draw histogram graph
            int x = 256, y = 512;
            Bitmap data = new Bitmap(x, y);
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    data.SetPixel(i, j, Color.White);
                }
            
            for (int i = 0; i < x; i++)
                for (int j = 0; j < Math.Min(histogram[i]/5, y); j++)
                {
                    data.SetPixel(i, 511 - j, Color.Black);
                }

            pictureBox2.Image = data;
            resultImage = data;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        // Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
