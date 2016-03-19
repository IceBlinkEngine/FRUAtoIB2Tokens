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
        public string propTokenIB2Path = "";
        public string tilesFruaPath = "";
        public string tilesIB2Path = "";
        public string itemIconsNWN2Path = "";
        public string itemIconsIB2Path = ""; 
        public string mainDirectory;
        public int tranR = 255;
        public int tranG = 0;
        public int tranB = 227;

        public Form1()
        {
            InitializeComponent();
            mainDirectory = Directory.GetCurrentDirectory();
            combatTokenFruaPath = mainDirectory + "\\CombatTokensFRUA";
            combatTokenIB2Path = mainDirectory + "\\CombatTokensIB2";
            propTokenIB2Path = mainDirectory + "\\PropsIB2";
            tilesFruaPath = mainDirectory + "\\TilesFRUA";
            tilesIB2Path = mainDirectory + "\\TilesIB2";
            itemIconsNWN2Path = mainDirectory + "\\ItemIconsNWN2";
            itemIconsIB2Path = mainDirectory + "\\ItemIconsIB2";
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
                            toSave.MakeTransparent(Color.FromArgb(255, 0, 255));
                            toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                            toSave.MakeTransparent(Color.FromArgb(100, 244, 156));
                            toSave.MakeTransparent(Color.FromArgb(tranR, tranG, tranB));
                            toSave.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            toSave.Save(combatTokenIB2Path + "\\tkn_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            if (chkIncludePropsFromTokens.Checked)
                            {
                                toSave = createPropFromCombatToken(imagepcx.PcxImage);
                                toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                                toSave.MakeTransparent(Color.FromArgb(255, 0, 255));
                                toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                                toSave.MakeTransparent(Color.FromArgb(100, 244, 156));
                                toSave.MakeTransparent(Color.FromArgb(tranR, tranG, tranB));
                                toSave.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                toSave.Save(propTokenIB2Path + "\\prp_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
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
                    Rectangle source = new Rectangle(8, 84, 48, 24);
                    Rectangle target = new Rectangle(4, 52, 192, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(64, 84, 48, 24);
                    target = new Rectangle(4, 252, 192, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else if ((tlp.X == 160) && (tlp.Y == 22)) //tall
                {
                    //Draw top token
                    Rectangle source = new Rectangle(160, 22, 24, 48);
                    Rectangle target = new Rectangle(52, 4, 96, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(192, 22, 24, 48);
                    target = new Rectangle(52, 204, 96, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else if ((tlp.X == 8) && (tlp.Y == 22)) //large
                {
                    //Draw top token
                    Rectangle source = new Rectangle(8, 22, 48, 48);
                    Rectangle target = new Rectangle(4, 4, 192, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(64, 22, 48, 48);
                    target = new Rectangle(4, 204, 192, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else //normal
                {
                    //Draw top token
                    Rectangle source = new Rectangle(160, 84, 24, 24);
                    Rectangle target = new Rectangle(2, 2, 96, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);

                    //Draw bottom token
                    source = new Rectangle(192, 84, 24, 24);
                    target = new Rectangle(2, 102, 96, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
            }
            return returnBitmap;
        }
        public Bitmap createPropFromCombatToken(Bitmap b)
        {
            Bitmap returnBitmap = new Bitmap(200, 200);
            Point tlp = findTopLeft(b);
            if ((tlp.X == 160) && (tlp.Y == 84)) //normal
            {
                returnBitmap = new Bitmap(100, 100);                
            }
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                if ((tlp.X == 8) && (tlp.Y == 84)) //wide
                {
                    //Draw top token
                    Rectangle source = new Rectangle(8, 84, 48, 24);
                    Rectangle target = new Rectangle(4, 52, 192, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else if ((tlp.X == 160) && (tlp.Y == 22)) //tall
                {
                    //Draw top token
                    Rectangle source = new Rectangle(160, 22, 24, 48);
                    Rectangle target = new Rectangle(52, 4, 96, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else if ((tlp.X == 8) && (tlp.Y == 22)) //large
                {
                    //Draw top token
                    Rectangle source = new Rectangle(8, 22, 48, 48);
                    Rectangle target = new Rectangle(4, 4, 192, 192);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
                else //normal
                {
                    //Draw top token
                    Rectangle source = new Rectangle(160, 84, 24, 24);
                    Rectangle target = new Rectangle(2, 2, 96, 96);
                    g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
                }
            }
            return returnBitmap;
        }
        public Point findTopLeft(Bitmap b)
        {
            var pixel = b.GetPixel(1, 1);
            if (b.Width > 215)
            {
                //normal
                pixel = b.GetPixel(160, 84);
                if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                {
                    //still black pixel
                }
                else
                {
                    tranR = pixel.R;
                    tranG = pixel.G;
                    tranB = pixel.B;
                    return new Point(160, 84);
                }
            }

            if (b.Width > 215)
            {
                //tall
                pixel = b.GetPixel(160, 22);
                if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                {
                    //still black pixel
                }
                else
                {
                    tranR = pixel.R;
                    tranG = pixel.G;
                    tranB = pixel.B;
                    return new Point(160, 22);
                }
            }

            //wide
            pixel = b.GetPixel(8, 84);
            if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
            {
                //still black pixel
            }
            else
            {
                tranR = pixel.R;
                tranG = pixel.G;
                tranB = pixel.B;
                return new Point(8, 84);
            }

            //large
            pixel = b.GetPixel(8, 22);
            if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
            {
                //still black pixel
            }
            else
            {
                tranR = pixel.R;
                tranG = pixel.G;
                tranB = pixel.B;
                return new Point(8, 22);
            }

            //still didn't find it so try this
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    pixel = b.GetPixel(x, y);
                    if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
                    {
                        //still black pixel
                    }
                    else
                    {
                        tranR = pixel.R;
                        tranG = pixel.G;
                        tranB = pixel.B;
                        return new Point(x, y);
                    }
                }
            }
            tranR = 255;
            tranG = 0;
            tranB = 227;
            return new Point(-1, -1);
        }
        public Point findBottomRight(Bitmap b)
        {
            //normal
            var pixel = b.GetPixel(216, 108);
            if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
            {
                //still black pixel
            }
            else
            {
                return new Point(216, 108);
            }

            //tall
            pixel = b.GetPixel(216, 69);
            if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
            {
                //still black pixel
            }
            else
            {
                return new Point(216, 69);
            }

            //wide
            pixel = b.GetPixel(111, 108);
            if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
            {
                //still black pixel
            }
            else
            {
                return new Point(111, 108);
            }

            //large
            pixel = b.GetPixel(111, 69);
            if ((pixel.A == 255) && (pixel.R == 0) && (pixel.G == 0) && (pixel.B == 0))
            {
                //still black pixel
            }
            else
            {
                return new Point(111, 69);
            }

            for (int x = b.Width - 1; x >= 0; x--)
            {
                for (int y = b.Height - 1; y >= 0; y--)
                {
                    pixel = b.GetPixel(x, y);
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
                            if (makeTileDown(imagepcx.PcxImage)) //make two versions, up and down
                            {
                                //at top
                                Bitmap toSave = createTile(imagepcx.PcxImage, true);
                                toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                                toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                                toSave.MakeTransparent(Color.FromArgb(128, 255, 128));
                                toSave.MakeTransparent(Color.FromArgb(252, 100, 252));
                                toSave.Save(tilesIB2Path + "\\t_" + filenameNoExt.ToLower() + "_U.png", System.Drawing.Imaging.ImageFormat.Png);
                                //at bottom
                                toSave = createTile(imagepcx.PcxImage, false);
                                toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                                toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                                toSave.MakeTransparent(Color.FromArgb(128, 255, 128));
                                toSave.MakeTransparent(Color.FromArgb(252, 100, 252));
                                toSave.Save(tilesIB2Path + "\\t_" + filenameNoExt.ToLower() + "_D.png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                            else
                            {
                                Bitmap toSave = createTile(imagepcx.PcxImage, true);
                                toSave.MakeTransparent(Color.FromArgb(255, 0, 227));
                                toSave.MakeTransparent(Color.FromArgb(103, 247, 159));
                                toSave.MakeTransparent(Color.FromArgb(128, 255, 128));
                                toSave.MakeTransparent(Color.FromArgb(252, 100, 252));
                                toSave.Save(tilesIB2Path + "\\t_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
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
        public bool makeTileDown(Bitmap b)
        {
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
            int width = right - left - 1;
            int height = bottom - top - 1;
            //if tile is 56 wide but not 56 tall then this is a tile that could be shifted down, return true
            if ((width == 56) && (height < 56))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Bitmap createTile(Bitmap b, bool makeTileUp)
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
                g.PixelOffsetMode = PixelOffsetMode.Default;

                float scale = 100.0f / 56.0f;

                int width = right - left - 1;
                int height = bottom - top - 1;
                float widthScaled = width * scale;
                float heightScaled = height * scale;
                float shiftXscaled = 0f;
                float shiftYscaled = 0f;
                //if tile is 56 wide but not 56 tall then this is a tile that should be at the top of the tile, shift up only
                if ((width == 56) && (height < 56))
                {
                    shiftXscaled = 0f;
                    if (makeTileUp)
                    {
                        shiftYscaled = 0f;
                    }
                    else
                    {
                        shiftYscaled = (float)(56 - height) * scale;
                    }
                }
                //if tile is under 56x56 and not a top tile, shift to bottom and right 8 pixels
                else if ((width < 56) && (height < 56))
                {
                    shiftXscaled = 8f;
                    shiftYscaled = (float)(56 - height) * scale;
                }
                else //if tile is 56x56, do not shift
                {
                    shiftXscaled = 0f;
                    shiftYscaled = 0f;
                }
                
                //Draw tile centered at bottom
                //int shiftX = (int)(((56f - (float)width) / 2f) * (100.0f / 56.0f));
                //int shiftY = (int)((56f - (float)height) * (100.0f / 56.0f));
                RectangleF source = new RectangleF(left + 1, top + 1, width - 1, height - 1);
                RectangleF target = new RectangleF(shiftXscaled, shiftYscaled, widthScaled, heightScaled);
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
            btnCreateItemIcons.Enabled = false;
            if (Directory.Exists(itemIconsNWN2Path))
            {
                string[] files = Directory.GetFiles(itemIconsNWN2Path, "*.tga");
                foreach (string file in files)
                {
                    try
                    {
                        string filename = Path.GetFileName(file);
                        string filenameNoExt = Path.GetFileNameWithoutExtension(file);
                        if ((filename.EndsWith(".tga")) || (filename.EndsWith(".TGA")))
                        {
                            Bitmap toSave = resizeItemIconToIB2(TargaImage.LoadTargaImage(file));
                            
                            if (filenameNoExt.StartsWith("it_"))
                            {
                                toSave.Save(itemIconsIB2Path + "\\" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                            else
                            {
                                toSave.Save(itemIconsIB2Path + "\\it_" + filenameNoExt.ToLower() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error While Creating Item Icons: " + ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("NWN2 Icon Source directory does not exist");
            }
            btnCreateItemIcons.Enabled = true;
        }
        public Bitmap resizeItemIconToIB2(Bitmap b)
        {
            Bitmap returnBitmap = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //g.PixelOffsetMode = PixelOffsetMode.Half;

                Rectangle source = new Rectangle(0, 0, b.Width, b.Height);
                Rectangle target = new Rectangle(0, 0, 100, 100);
                g.DrawImage((Image)b, target, source, GraphicsUnit.Pixel);
            }
            return returnBitmap;
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            string help = "To create combat tokens, place any FRUA combat token PCX files in the CombatTokensFRUA"
            + " folder and then press the 'Create Combat Tokens' button. All the converted tokens will"
            + " be placed in the CombatTokensIB2 folder." + Environment.NewLine
            + " To create Item icons from any TGA file (like NWN or NWN2), place any TGA files in the ItemIconsNWN2"
            + " folder and then press the 'Create Item Icons' button. All the converted items will"
            + " be placed in the ItemIconsIB2 folder." + Environment.NewLine
            + " To create tiles, place any FRUA wall tile PCX files in the TilesFRUA folder and then press the"
            + " 'Create Tiles' button. All the converted tiles will be placed in the TilesIB2 folder.";

            MessageBox.Show(help);
        }
    }
}
