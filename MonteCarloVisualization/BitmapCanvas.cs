using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MonteCarloVisualization
{
    public class BitmapCanvas
    {
        public WriteableBitmap Bitmap { get; }

        public BitmapCanvas(int width, int height)
        {
            Bitmap = new WriteableBitmap(width, height, 96d, 96d, PixelFormats.Bgr24, null);
        }

        public void Clear()
        {
            Bitmap.Lock();
            DrawRectangle(0, 0, (int)Bitmap.Width, (int)Bitmap.Height, Brushes.White.Color);
            Bitmap.Unlock();
        }

        public void DrawPoints(IEnumerable<Point> points)
        {
            Bitmap.Lock();
            foreach (var point in points)
            {
                DrawPoint( (int)point.X, (int)point.Y);
            }
            Bitmap.Unlock();
        }

        private void DrawPoint(int left, int top)
        {
            int width = 3;
            int height = 3;
            var color = Brushes.DarkOrange.Color;
            DrawRectangle(left, top, width, height, color);
        }

        private void DrawRectangle(int left, int top, int width, int height, Color color)
        {
            if (left + width >= Bitmap.Width)
                left = (int) Bitmap.Width - width;

            if (top + height >= Bitmap.Height)
                top = (int)Bitmap.Height - height;

            int colorData = color.R << 16; // R
            colorData |= color.G << 8; // G
            colorData |= color.B << 0; // B
            int bpp = Bitmap.Format.BitsPerPixel / 8;

            unsafe
            {
                for (int y = 0; y < height; y++)
                {
                    int pBackBuffer = (int)Bitmap.BackBuffer;

                    pBackBuffer += (top + y) * Bitmap.BackBufferStride;
                    pBackBuffer += left * bpp;

                    for (int x = 0; x < width; x++)
                    {
                        *((int*)pBackBuffer) = colorData;

                        pBackBuffer += bpp;
                    }
                }
            }

            Bitmap.AddDirtyRect(new Int32Rect(left, top, width, height));
        }
    }
}