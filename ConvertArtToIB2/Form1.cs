using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConvertArtToIB2
{
    public partial class Form1 : Form
    {
        public ImagePcx imagepcx;
        public string sourceFolderPath = "";
        public string targetFolderPath = "";
        public string mainDirectory;

        public Form1()
        {
            InitializeComponent();
            mainDirectory = Directory.GetCurrentDirectory();
        }
                
        public Bitmap createCombatToken(Bitmap b)
        {
            Bitmap returnBitmap = new Bitmap(200, 400);
            Point tlp = findTopLeft(b);
            Point brp = findBottomRight(b);
            if ((tlp.X == 160) && (tlp.Y == 84)) //normal
            {
                returnBitmap = new Bitmap(100, 200);                
            }
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                if ((tlp.X == 8) && (tlp.Y == 84)) //wide
                {
                    //Draw top token
                    Rectangle source = new Rectangle(tlp.X, tlp.Y, 48, 24);
                    Rectangle target = new Rectangle(4, 52, 192, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(brp.X - 47, tlp.Y, 48, 24);
                    target = new Rectangle(4, 252, 192, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else if ((tlp.X == 160) && (tlp.Y == 22)) //tall
                {
                    //Draw top token
                    Rectangle source = new Rectangle(tlp.X, tlp.Y, 24, 48);
                    Rectangle target = new Rectangle(52, 4, 96, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(brp.X - 23, tlp.Y, 24, 48);
                    target = new Rectangle(52, 204, 96, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else if ((tlp.X == 8) && (tlp.Y == 22)) //large
                {
                    //Draw top token
                    Rectangle source = new Rectangle(tlp.X, tlp.Y, 48, 48);
                    Rectangle target = new Rectangle(4, 4, 192, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(brp.X - 47, tlp.Y, 48, 48);
                    target = new Rectangle(4, 204, 192, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else //normal
                {
                    //Draw top token
                    Rectangle source = new Rectangle(tlp.X, tlp.Y, 24, 24);
                    Rectangle target = new Rectangle(2, 2, 96, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(brp.X - 23, tlp.Y, 24, 24);
                    target = new Rectangle(2, 102, 96, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
            }
            return returnBitmap;
        }
        public Point findTopLeft(Bitmap b)
        {
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    var pixel = b.GetPixel(x, y);
                    if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                    {
                        //still black pixel
                    }
                    else
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(-1, -1);
        }
        public Point findBottomRight(Bitmap b)
        {
            for (int x = b.Width - 1; x >= 0; x--)
            {
                for (int y = b.Height - 1; y >= 0; y--)
                {
                    var pixel = b.GetPixel(x, y);
                    if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                    {
                        //still black pixel
                    }
                    else
                    {
                        return new Point(x, y);
                    }
                }
            }
            return new Point(-1, -1);
        }

        private void btnCreateAll_Click(object sender, EventArgs e)
        {
            btnCreateAll.Enabled = false;
            if (Directory.Exists(sourceFolderPath))
            {
                string[] files = Directory.GetFiles(sourceFolderPath, "*.pcx");
                foreach (string file in files)
                {
                    try
                    {
                        string filename = Path.GetFileName(file);
                        string filenameNoExt = Path.GetFileNameWithoutExtension(file);
                        if ((filename.EndsWith(".pcx")) || (filename.EndsWith(".PCX")))
                        {
                            imagepcx = new ImagePcx(file);
                            Bitmap toSave = createCombatToken(imagepcx.PcxImage);
                            toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                            toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                            toSave.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            Invalidate();
                            toSave.Save(targetFolderPath + "\\tkn_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error While Creating Tokens: " + ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Source directory does not exist");
            }
            btnCreateAll.Enabled = true;
        }

        private void btnSourceFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = mainDirectory;
            DialogResult result = folderBrowserDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                sourceFolderPath = folderBrowserDialog1.SelectedPath;
                txtSourceFolder.Text = sourceFolderPath;
            }
        }

        private void btnTargetFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.SelectedPath = mainDirectory;
            DialogResult result = folderBrowserDialog2.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                targetFolderPath = folderBrowserDialog2.SelectedPath;
                txtTargetFolder.Text = targetFolderPath;
            }
        }
    }
}
