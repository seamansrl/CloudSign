using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace Horus
{
    class Graficas
    {
        public Image Sprite;

        public Int32 LastX = 0;
        public Int32 LastY = 0;
        public Int32 TotalFrames = 1;
        public Int32 ActualFrame = 1;

        public Boolean Visible = true;
        public Boolean Loop = true;
        public Boolean Smooth = true;
        
        public void Move(Graphics g, Int32 X, Int32 Y, Int32 width = 0, Int32 height = 0)
        {
            if (LastX == 0)
                LastX = X;

            if (LastY == 0)
                LastY = Y;

            if (Visible)
            {
                FrameDimension dimension = new FrameDimension(Sprite.FrameDimensionsList[0]);
                TotalFrames = Sprite.GetFrameCount(dimension);

                if (TotalFrames > 1)
                {
                    Sprite.SelectActiveFrame(dimension, ActualFrame - 1);

                    if (ActualFrame >= TotalFrames - 1 && Loop)
                        ActualFrame = 0;
                    else
                        if (ActualFrame < TotalFrames)
                        ActualFrame++;
                }

                if (height > 0)
                    Sprite = Resize(Sprite, Sprite.Width, height, false);

                if (width > 0)
                    Sprite = Resize(Sprite, width, Sprite.Height, false);

                if (Smooth)
                {
                    if (Distance(X, Y, LastX, LastY) > 2)
                    {
                        // SI LA DISTANCIA ESTA EN X
                        if (Math.Abs((LastX - X)) > Math.Abs((LastY - Y)))
                        {
                            float m = 0;

                            if ((LastX - X) != 0)
                            {
                                m = (float)(LastY - Y) / (float)(LastX - X);
                                float b = Y - m * X;

                                if (X > LastX)
                                    X = LastX + Convert.ToInt32(Math.Round(Distance(X, Y, LastX, LastY) / 1.2));
                                else
                                    X = LastX - Convert.ToInt32(Math.Round(Distance(X, Y, LastX, LastY) / 1.2));

                                Y = Convert.ToInt32((m * X) + b);

                            }

                            g.DrawImage(Sprite, new Point(X, Y));
                        }
                        else
                        {
                            float m = 0;

                            if ((LastY - Y) != 0)
                            {
                                m = (float)(LastX - X) / (float)(LastY - Y);
                                float b = X - m * Y;

                                if (Y > LastY)
                                    Y = LastY + Convert.ToInt32(Math.Round(Distance(X, Y, LastX, LastY) / 1.2));
                                else
                                    Y = LastY - Convert.ToInt32(Math.Round(Distance(X, Y, LastX, LastY) / 1.2));

                                X = Convert.ToInt32((m * Y) + b);
                            }

                            g.DrawImage(Sprite, new Point(X, Y));
                        }
                    }
                    else
                    {
                        g.DrawImage(Sprite, new Point(X, Y));
                    }
                }
                else
                {
                    g.DrawImage(Sprite, new Point(X, Y));
                }
            }

            LastX = X;
            LastY = Y;
        }

        private Double Distance(Int32 X1, Int32 Y1, Int32 X2, Int32 Y2)
        {
            Double Diff = (X2 - X1) + (Y2 - Y1);
            Diff = Math.Sqrt(Math.Pow(Diff, 2));

            return Diff;
        }

        public Image SpriteFromFile(String Archivo)
        {
            return (Bitmap)Image.FromFile(Archivo, true);
        }

        private Image Resize(Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height;
            newHeight = maxHeight;

            var res = new Bitmap(newWidth, newHeight);

            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return res;
        }
    }
}
