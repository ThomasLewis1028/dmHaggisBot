using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using MongoDB.Bson;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    public class GridCreation
    {
        public static void CreateGrid(Grid grid)
        {
            Bitmap bmp = new Bitmap(500, 800, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.MidnightBlue);
            Pen pen = new Pen(Color.Black, 2);

            const float hyp = 50;
            const int intAngle = 30;
            const double intRadians = intAngle * (Math.PI / 180);
            var cos = (float) Math.Cos(intRadians);
            var sin = (float) Math.Sin(intRadians);

            var points = CreateHex(new PointF(0, 0));

            g.DrawPolygon(pen, points);
            
            bmp.Save(@"C:\Users\thoma\Desktop\grid.png", ImageFormat.Png);
        }

        /// <summary>
        /// CreateHex takes a point and creates a hex shape based on that starting point
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <returns>
        /// An array of points needed to create a hex shape
        /// </returns>
        private static PointF[] CreateHex(PointF startingPoint)
        {
            float startX = startingPoint.X;
            float startY = startingPoint.Y;
            const float hyp = 50; // hypotenuse for each side of the hex
            const int intAngle = 30; // angle in degrees because I can actually write that down
            const double intRadians = intAngle * (Math.PI / 180); // degrees to radians
            var cos = (float) Math.Cos(intRadians); // cos(30)
            var sin = (float) Math.Sin(intRadians); // sin(30)

            // Points starting from top left going clockwise
            PointF[] points =
            {
                new PointF(hyp * sin, startY), // h*sin(30), y
                new PointF(hyp * sin + hyp, startY), // h*sin(30)+h, y
                new PointF(startX, hyp * cos), // x, h*cos(30)
                new PointF(hyp * sin, 2 * hyp * cos), // h*sin(30), 2cos(30)  
                new PointF(hyp * sin + hyp, 2 * hyp * cos), // h*sin(30)+h, h*2cos(30)
                new PointF(2 * hyp * sin + hyp, hyp * cos) // h*2sin(30)+h, h*sin(30)  
            };

            return points;
        }
    }
}