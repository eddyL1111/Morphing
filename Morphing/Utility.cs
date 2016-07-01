using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Morphing
{
    class Utility
    {
        private static int IMG_WIDTH = 401;
        private static int IMG_HEIGHT = 371;


        // Opens dialog for laoding image
        public static Bitmap LoadImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" + "All files (*.*)|*.*";
            ofd.Title = "Select Image";

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Image loadedImg = Image.FromFile(ofd.FileName);
                return ResizeImg(new Bitmap(loadedImg));
            }
            return null;
        }

        // Resizes the image to fit the pictureboxes' size
        public static Bitmap ResizeImg(Bitmap bm)
        {
            Bitmap result = new Bitmap(IMG_WIDTH, IMG_HEIGHT);
            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(bm, 0, 0, IMG_WIDTH, IMG_HEIGHT);
            }

            return result;
        }

        public static Color[,] convertBitmapToData(Bitmap bm)
        {
            Color[,] result = new Color[bm.Width, bm.Height];

            for (int y = 0; y < bm.Height; y++)
            {
                for(int x =0; x < bm.Width; x++)
                {
                    Color px = bm.GetPixel(x, y);
                    result[x, y] = px;
                }
            }
            return result;
        }

        public static Bitmap convertDataToBitmap(Color[,] data)
        {
            Bitmap result = new Bitmap(data.GetLength(0), data.GetLength(1));

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    result.SetPixel(x, y, data[x,y]);
                }
            }
            return result;
        }
    }
}
