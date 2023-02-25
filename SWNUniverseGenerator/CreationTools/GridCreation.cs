﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public Universe CreateGrid(Universe universe)
        {
            int width =
                (int) ((universe.Grid.X * ((Hypotenuse * Sine) + Hypotenuse)) + (Hypotenuse * Sine) + Hypotenuse);
            int height = (int) ((universe.Grid.Y * (2 * Hypotenuse * Cosign)) + (Hypotenuse * Cosign) + Hypotenuse);
            float penThickness = Hypotenuse * 0.05F;
            float starSize = Hypotenuse * 0.175F;
            float planetSize = Hypotenuse * 0.075F;
            float orbitSize = starSize;
            float orbitThickness = Hypotenuse * 0.001F;

            while (width * height > 500000000)
            {
                Hypotenuse -= 10;

                width =
                    (int) ((universe.Grid.X * ((Hypotenuse * Sine) + Hypotenuse)) + (Hypotenuse * Sine) + Hypotenuse);
                height = (int) ((universe.Grid.Y * (2 * Hypotenuse * Cosign)) + (Hypotenuse * Cosign) + Hypotenuse);

                penThickness = Hypotenuse * 0.05F;
                starSize = Hypotenuse * 0.25F;
                planetSize = Hypotenuse * 0.1F;
                orbitSize = starSize;
                orbitThickness = Hypotenuse * 0.001F;
            }


            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(19, 21, 64));
            Pen pen = new Pen(Color.White, penThickness);
            Pen orbitPen = new Pen(Color.White, orbitThickness);
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

                if (zone != null && zone.Planets.Count > 0)
                {
                    for (int i = 1; i <= zone.Planets.Count; i++)
                    {
                        float modifer = i == 1 ? 2F : i * 1.5F;
                        float currentOrbit = orbitSize * modifer;

                        RectangleF orbitRect = CircleToRectangle(hex.MidPoint, currentOrbit);

                        g.DrawEllipse(orbitPen, orbitRect);


                        int angle = new Random().Next(0, 360);
                        double radian = (angle * (Math.PI / 180));

                        PointF planetPoint = new PointF(hex.MidPoint.X + (currentOrbit * (float) Math.Cos(radian)),
                            hex.MidPoint.Y + (currentOrbit * (float) Math.Sin(radian)));

                        RectangleF planetRect = CircleToRectangle(planetPoint, planetSize);

                        g.FillEllipse(planetBrush, planetRect);
                    }
                }

                if (zone is {StarId: { }})
                {
                    RectangleF starRect = CircleToRectangle(hex.MidPoint, starSize);

                    g.FillEllipse(starBrush, starRect);
                    g.DrawString(zone.StarId,
                        new Font("Cascadia Mono", Hypotenuse * 0.13F, FontStyle.Bold),
                        textBrush,
                        hex.TextLocation);
                }
            }

            universe.StarMap = ImageToBase64String(bmp);
            return universe;
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

        public static RectangleF CircleToRectangle(PointF midpoint, float radius)
        {
            return new RectangleF(midpoint.X - radius,
                midpoint.Y - radius,
                radius * 2,
                radius * 2);
        }

        public string ImageToBase64String(Image image)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, ImageFormat.Png);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();
            return base64;
        }

        public Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();
            return result;
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