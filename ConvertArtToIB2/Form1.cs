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
        public string combatTokenFruaPath = "";
        public string combatTokenIB2Path = "";
        public string tilesFruaPath = "";
        public string tilesIB2Path = "";
        public string mainDirectory;

        public Form1()
        {
            InitializeComponent();
            mainDirectory = Directory.GetCurrentDirectory();
            combatTokenFruaPath = mainDirectory + "\\CombatTokensFRUA";
            combatTokenIB2Path = mainDirectory + "\\CombatTokensIB2";
            tilesFruaPath = mainDirectory + "\\TilesFRUA";
            tilesIB2Path = mainDirectory + "\\TilesIB2";
        }

        private void btnCreateTokens_Click(object sender, EventArgs e)
        {
            btnCreateTokens.Enabled = false;
            if (Directory.Exists(combatTokenFruaPath))
            {
                string[] files = Directory.GetFiles(combatTokenFruaPath, "*.pcx");
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
                            toSave.Save(combatTokenIB2Path + "\\tkn_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
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
                MessageBox.Show("Tokens Source directory does not exist");
            }
            btnCreateTokens.Enabled = true;
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

        private void btnCreateTiles_Click(object sender, EventArgs e)
        {
            btnCreateTiles.Enabled = false;
            if (Directory.Exists(tilesFruaPath))
            {
                string[] files = Directory.GetFiles(tilesFruaPath, "*.pcx");
                foreach (string file in files)
                {
                    try
                    {
                        string filename = Path.GetFileName(file);
                        string filenameNoExt = Path.GetFileNameWithoutExtension(file);
                        if ((filename.EndsWith(".pcx")) || (filename.EndsWith(".PCX")))
                        {
                            imagepcx = new ImagePcx(file);
                            Bitmap toSave = createTile(imagepcx.PcxImage);
                            toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                            toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                            toSave.MakeTransparent(Color.FromArgb(128, 255, 128));
                            toSave.MakeTransparent(Color.FromArgb(252, 100, 252));
                            toSave.Save(tilesIB2Path + "\\t_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error While Creating Tiles: " + ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Tile Source directory does not exist");
            }
            btnCreateTiles.Enabled = true;
        }
        public Bitmap createTile(Bitmap b)
        {
            Bitmap returnBitmap = new Bitmap(100, 100);
            int top = 0;
            int left = 0;
            int bottom = 0;
            int right = 0;

            if (WallIsAt90(b)) //wall is at 65,60
            {
                top = 60;
                left = 65;
                bottom = traverseDown(left, top, b);
                right = traverseRight(left, bottom, b);
            }
            else //wall is at 130,60 -> 187,117 (56x56)
            {
                top = 60;
                left = 130;
                bottom = traverseDown(left, top, b);
                right = traverseRight(left, bottom, b);
            }
            
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                //Draw tile centered at bottom
                int width = right - left;
                int height = bottom - top;
                int shiftX = (int)(((56f - (float)width) / 2f) * (100.0f / 56.0f));
                int shiftY = (int)((56f - (float)height) * (100.0f / 56.0f));
                Rectangle source = new Rectangle(left + 1, top + 1, width - 1, height - 1);
                Rectangle target = new Rectangle(shiftX, shiftY, 100 - shiftX, 100 - shiftY);
                g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
            }
            return returnBitmap;
        }
        public bool WallIsAt90(Bitmap b)
        {
            //try traversing vertically along x = 100 to find a non-black pixel, if you do then you found the wall tile.
            for (int y = 0; y < b.Height; y++)
            {
                var pixel = b.GetPixel(90, y);
                if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                {
                    //still black pixel
                }
                else
                {
                    return true;
                }
            }            
            return false;
        }
        public int traverseDown(int x, int startY, Bitmap b)
        {
            for (int y = startY; y < b.Height; y++)
            {
                var pixel = b.GetPixel(x, y);
                if ((pixel.A == 255) && (pixel.R == 252) && (pixel.G == 100) && (pixel.B == 252))
                {
                    //still magenta R=252, G=100, B=252
                }
                else
                {
                    return y - 1;
                }
            }
            return -1;
        }
        public int traverseRight(int startX, int y, Bitmap b)
        {
            for (int x = startX; x < b.Height; x++)
            {
                var pixel = b.GetPixel(x, y);
                if ((pixel.A == 255) && (pixel.R == 252) && (pixel.G == 100) && (pixel.B == 252))
                {
                    //still magenta R=252, G=100, B=252
                }
                else
                {
                    return x - 1;
                }
            }
            return -1;
        }

        private void btnCreatePortraits_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateItemIcons_Click(object sender, EventArgs e)
        {

        }
        private void btnHelp_Click(object sender, EventArgs e)
        {

        }
    }
}
