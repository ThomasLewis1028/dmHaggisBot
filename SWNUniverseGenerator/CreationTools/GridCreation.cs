using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    public class GridCreation
    {
        private static float Hypotenuse = 1000; // hypotenuse for each side of the hex
        private const int IntAngle = 30; // angle in degrees because I can actually write that down
        private const double IntRadians = IntAngle * (Math.PI / 180); // degrees to radians
        private static readonly float Cosign = (float) Math.Cos(IntRadians); // cos(30)
        private static readonly float Sine = (float) Math.Sin(IntRadians); // sin(30)
        // private static float PenThickness = Hypotenuse * 0.05F;
        // private static float StarSize = Hypotenuse * 0.25F;
        // private static float PlanetSize = Hypotenuse * 0.1F;
        // private static float OrbitSize = StarSize;
        // private static float DistFromStar = Hypotenuse * 0.1F;
        // private static float OrbitThickness = Hypotenuse * 0.001F;

        public static void CreateGrid(Universe universe)
        {
            int width = (int) ((universe.Grid.X * ((Hypotenuse * Sine) + Hypotenuse)) + (Hypotenuse * Sine) + Hypotenuse);
            int height = (int) ((universe.Grid.Y * (2 * Hypotenuse * Cosign)) + (Hypotenuse * Cosign) + Hypotenuse);
            float PenThickness = Hypotenuse * 0.05F;
            float StarSize = Hypotenuse * 0.25F;
            float PlanetSize = Hypotenuse * 0.1F;
            float OrbitSize = StarSize;
            float DistFromStar = Hypotenuse * 0.1F;
            float OrbitThickness = Hypotenuse * 0.001F;

            while (width * height > 500000000) 
            {
                Hypotenuse -= 10;
                
                width = (int) ((universe.Grid.X * ((Hypotenuse * Sine) + Hypotenuse)) + (Hypotenuse * Sine) + Hypotenuse);
                height = (int) ((universe.Grid.Y * (2 * Hypotenuse * Cosign)) + (Hypotenuse * Cosign) + Hypotenuse);
                
                PenThickness = Hypotenuse * 0.05F;
                StarSize = Hypotenuse * 0.25F;
                PlanetSize = Hypotenuse * 0.1F;
                OrbitSize = StarSize;
                DistFromStar = Hypotenuse * 0.1F;
                OrbitThickness = Hypotenuse * 0.001F;
            }


            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(19, 21, 64));
            Pen pen = new Pen(Color.White, PenThickness);
            Pen orbitPen = new Pen(Color.White, OrbitThickness);
            Brush starBrush = new SolidBrush(Color.FromArgb(255, 241, 104));
            Brush textBrush = new SolidBrush(Color.White);
            Brush planetBrush = new SolidBrush(Color.DarkGreen);

            List<Hex> hexList = new();
            PointF startingPoint = new PointF(Hypotenuse / 2, Hypotenuse / 2);

            for (int x = 0; x < universe.Grid.X; x++)
            {
                for (int y = 0; y < universe.Grid.Y; y++)
                {
                    var hex = CreateHex(startingPoint, hexList, (x, y));
                    startingPoint = hex.BottomLeft;
                }

                startingPoint = x % 2 != 0 ? hexList[^universe.Grid.Y].TopRight : hexList[^universe.Grid.Y].MidRight;
            }

            foreach (var hex in hexList)
            {
                g.DrawPolygon(pen, hex.Points.ToArray());

                Zone zone = universe.Zones.Find(z => z.X == hex.X && z.Y == hex.Y);

                if (zone is {StarId: { }})
                {
                    RectangleF starRect = new RectangleF(
                        hex.MidPoint.X - StarSize / 2,
                        hex.MidPoint.Y - StarSize / 2,
                        StarSize,
                        StarSize);

                    g.FillEllipse(starBrush, starRect);
                    g.DrawString(zone.StarId,
                        new Font("Cascadia Mono", Hypotenuse * 0.13F, FontStyle.Bold),
                        textBrush,
                        hex.TextLocation);
                }

                if (zone != null && zone.Planets.Count > 0)
                {
                    for (int i = 1; i <= zone.Planets.Count; i++)
                    {
                        float currentOrbit = i == 1 ? OrbitSize * 2F : OrbitSize * (i * 1.5F);
                        
                        RectangleF orbitRect = new RectangleF(
                            hex.MidPoint.X - (currentOrbit / 2),
                            hex.MidPoint.Y - (currentOrbit / 2),
                            currentOrbit,
                            currentOrbit);

                        g.DrawEllipse(orbitPen, orbitRect);

                        // center + (radius * cosine(angle))
                        // center + (radius * sine(angle))
                        RectangleF rectangleF = new RectangleF(
                            hex.MidPoint.X + (currentOrbit / 2) * (float) Math.Cos(new Random().Next()),
                            hex.MidPoint.Y + (currentOrbit / 2) * (float) Math.Sin(new Random().Next()),
                            PlanetSize,
                            PlanetSize);


                        g.FillEllipse(planetBrush, rectangleF);
                    }
                }
            }

            bmp.Save(@"C:\Users\thoma\Desktop\grid.png", ImageFormat.Png);
        }

        /// <summary>
        /// CreateHex takes a point and creates a hex shape based on that starting point
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="hexes"></param>
        /// <param name="xAndY"></param>
        /// <returns>
        /// An array of points needed to create a hex shape
        /// </returns>
        private static Hex CreateHex(PointF startingPoint, List<Hex> hexes, (int x, int y) xAndY)
        {
            Hex hex = new Hex();
            float startX = startingPoint.X;
            float startY = startingPoint.Y;

            // Points starting from top right going counter-clockwise
            hex.Points = new List<PointF>()
            {
                new(startX + (Hypotenuse * Sine + Hypotenuse), startY),
                new(startX + (Hypotenuse * Sine), startY),
                new(startX, startY + (Hypotenuse * Cosign)),
                new(startX + (Hypotenuse * Sine), startY + (2 * Hypotenuse * Cosign)),
                new(startX + (Hypotenuse * Sine + Hypotenuse), startY + (2 * Hypotenuse * Cosign)),
                new(startX + (2 * Hypotenuse * Sine + Hypotenuse), startY + (Hypotenuse * Cosign))
            };

            hex.MidRight = new PointF(startX + ((Hypotenuse * Sine) + Hypotenuse), startY + (Hypotenuse * Cosign));
            hex.BottomLeft = new PointF(startX, startY + (2 * Cosign * Hypotenuse));
            hex.MidPoint = new PointF(startX + (Hypotenuse * Sine) + (Hypotenuse / 2), startY + (Hypotenuse * Cosign));
            hex.TopRight = new PointF(hex.MidRight.X, startY - (Hypotenuse * Cosign));
            hex.MidBottom = new PointF(hex.MidPoint.X, hex.MidPoint.Y + (Hypotenuse * 0.5F));
            hex.TextLocation = new PointF(hex.MidPoint.X - (Hypotenuse * 0.55F), hex.MidPoint.Y + (Hypotenuse * 0.5F));
            hex.X = xAndY.x;
            hex.Y = xAndY.y;

            hexes.Add(hex);

            return hex;
        }

        private class Hex
        {
            public List<PointF> Points { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public PointF MidRight { get; set; }

            public PointF BottomLeft { get; set; }

            public PointF MidPoint { get; set; }

            public PointF TopRight { get; set; }

            public PointF MidBottom { get; set; }

            public PointF TextLocation { get; set; }
        }
    }
}