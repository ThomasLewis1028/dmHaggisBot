using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator.CreationTools
{
    public class GridCreation
    {
        private static float _hypotenuse = 1000; // hypotenuse for each side of the hex
        private const int IntAngle = 30; // angle in degrees because I can actually write that down
        private const double IntRadians = IntAngle * (Math.PI / 180); // degrees to radians
        private static readonly float Cosign = (float) Math.Cos(IntRadians); // cos(30)
        private static readonly float Sine = (float) Math.Sin(IntRadians); // sin(30)

        public String CreateGrid(string universeId, UniverseContext universeContext)
        {
            var gridX = 0;
            var gridY = 0;
            using (var univRepo = new Repository<Universe>(universeContext))
            {
                gridX = ((Universe) univRepo.Search(u => u.Id == universeId).First()).GridX;
                gridY = ((Universe) univRepo.Search(u => u.Id == universeId).First()).GridY;
            }

            int width =
                (int) ((gridX * ((_hypotenuse * Sine) + _hypotenuse)) + (_hypotenuse * Sine) + _hypotenuse);
            int height = (int) ((gridY * (2 * _hypotenuse * Cosign)) + (_hypotenuse * Cosign) + _hypotenuse);
            float gridPenThickness = _hypotenuse * 0.05F;
            float starSize = _hypotenuse * 0.175F;
            float planetSize = _hypotenuse * 0.075F;
            float orbitSize = starSize;
            float orbitThickness = _hypotenuse * 0.001F;
            float bgStarThickness = _hypotenuse * 0.001F;

            while (width * height > 500000000)
            {
                _hypotenuse -= 10;

                width =
                    (int) ((gridX * ((_hypotenuse * Sine) + _hypotenuse)) + (_hypotenuse * Sine) + _hypotenuse);
                height = (int) ((gridY * (2 * _hypotenuse * Cosign)) + (_hypotenuse * Cosign) + _hypotenuse);

                gridPenThickness = _hypotenuse * 0.05F;
                starSize = _hypotenuse * 0.25F;
                planetSize = _hypotenuse * 0.1F;
                orbitSize = starSize;
                orbitThickness = _hypotenuse * 0.001F;
                bgStarThickness = _hypotenuse * 0.001F;
            }

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(8, 8, 17));
            Pen gridPen = new Pen(Color.White, gridPenThickness);
            Pen orbitPen = new Pen(Color.White, orbitThickness);

            Brush bgStarBrush = new SolidBrush(Color.White);
            Brush blueStarBrush = new SolidBrush(Color.FromArgb(21, 167, 255));
            Brush whiteStarBrush = new SolidBrush(Color.WhiteSmoke);
            Brush yellowStarBrush = new SolidBrush(Color.FromArgb(255, 241, 104));
            Brush lightOrangeStarBrush = new SolidBrush(Color.FromArgb(255, 185, 50));
            Brush orangeRedStarBrush = new SolidBrush(Color.FromArgb(255, 134, 47));
            Brush yellowWhiteStarBrush = new SolidBrush(Color.FromArgb(255, 251, 174));
            Brush blueWhiteStarBrush = new SolidBrush(Color.FromArgb(150, 219, 255));

            Brush textBrush = new SolidBrush(Color.White);
            Brush planetBrush = new SolidBrush(Color.FromArgb(0, 66, 7));

            List<Hex> hexList = new();
            PointF startingPoint = new PointF(_hypotenuse / 2, _hypotenuse / 2);

            // Create the hex grid
            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    var hex = CreateHex(startingPoint, hexList, (x, y));
                    startingPoint = hex.BottomLeft;
                }

                startingPoint = x % 2 != 0
                    ? hexList[^gridY].TopRight
                    : hexList[^gridY].MidRight;
            }


            // Generate background stars
            var bgStarCount = (width * height) * .01;

            for (int i = 0; i < bgStarCount; i++)
            {
                var variation = new Random().Next(0, 100) * .001;

                RectangleF starRect = CircleToRectangle(new PointF()
                {
                    X = new Random().Next(0, width),
                    Y = new Random().Next(0, height)
                }, (float) (bgStarThickness * variation));

                g.FillEllipse(bgStarBrush, starRect);
            }

            // Generate stars and planets
            foreach (var hex in hexList)
            {
                g.DrawPolygon(gridPen, hex.Points.ToArray());

                using (var zoneRepo = new Repository<Zone>(universeContext))
                {
                    String zoneId = zoneRepo.Search(z => z.UniverseId == universeId
                                                         && z.X == hex.X
                                                         && z.Y == hex.Y)
                        .First().Id;

                    var planets = new Repository<Planet>(universeContext)
                        .Search(p => p.ZoneId == zoneId).ToList();

                    Star star = null;
                    if (new Repository<Star>(universeContext).Count(s => s.ZoneId == zoneId) > 0)
                        star = (Star) new Repository<Star>(universeContext)
                            .Search(s => s.ZoneId == zoneId).ToList().First();

                    // Planets
                    if (planets.Count > 0)
                    {
                        for (int i = 1; i <= planets.Count; i++)
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

                    // Stars
                    if (star != null)
                    {
                        Brush starBrush = orangeRedStarBrush;

                        RectangleF starRect = CircleToRectangle(hex.MidPoint, starSize);

                        starBrush = star.StarColor switch
                        {
                            Star.StarColorEnum.Blue => blueStarBrush,
                            Star.StarColorEnum.BlueWhite => blueWhiteStarBrush,
                            Star.StarColorEnum.White => whiteStarBrush,
                            Star.StarColorEnum.YellowWhite => yellowWhiteStarBrush,
                            Star.StarColorEnum.Yellow => yellowStarBrush,
                            Star.StarColorEnum.LightOrange => lightOrangeStarBrush,
                            Star.StarColorEnum.OrangeRed => orangeRedStarBrush,
                            _ => starBrush
                        };

                        g.FillEllipse(starBrush, starRect);
                        g.DrawString(star.Name,
                            new Font("Cascadia Mono", _hypotenuse * 0.13F, FontStyle.Bold),
                            textBrush,
                            hex.TextLocation);
                    }
                }
            }

            var imagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                            + "/"
                            + universeId
                            + ".png";

            bmp.Save(imagePath,
                ImageFormat.Png);

            return imagePath;
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
                new(startX + (_hypotenuse * Sine + _hypotenuse), startY),
                new(startX + (_hypotenuse * Sine), startY),
                new(startX, startY + (_hypotenuse * Cosign)),
                new(startX + (_hypotenuse * Sine), startY + (2 * _hypotenuse * Cosign)),
                new(startX + (_hypotenuse * Sine + _hypotenuse), startY + (2 * _hypotenuse * Cosign)),
                new(startX + (2 * _hypotenuse * Sine + _hypotenuse), startY + (_hypotenuse * Cosign))
            };

            hex.MidRight = new PointF(startX + ((_hypotenuse * Sine) + _hypotenuse), startY + (_hypotenuse * Cosign));
            hex.BottomLeft = new PointF(startX, startY + (2 * Cosign * _hypotenuse));
            hex.MidPoint = new PointF(startX + (_hypotenuse * Sine) + (_hypotenuse / 2), startY + (_hypotenuse * Cosign));
            hex.TopRight = new PointF(hex.MidRight.X, startY - (_hypotenuse * Cosign));
            hex.TextLocation = new PointF(hex.MidPoint.X - (_hypotenuse * 0.55F), hex.MidPoint.Y + (_hypotenuse * 0.5F));
            hex.X = xAndY.x;
            hex.Y = xAndY.y;

            hexes.Add(hex);

            return hex;
        }

        private static RectangleF CircleToRectangle(PointF midpoint, float radius)
        {
            return new RectangleF(midpoint.X - radius,
                midpoint.Y - radius,
                radius * 2,
                radius * 2);
        }

        // public string ImageToBase64String(Image image)
        // {
        //     MemoryStream memory = new MemoryStream();
        //     image.Save(memory, ImageFormat.Png);
        //     string base64 = Convert.ToBase64String(memory.ToArray());
        //     memory.Close();
        //     return base64;
        // }
        //
        // public Image ImageFromBase64String(string base64)
        // {
        //     MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
        //     Image result = Image.FromStream(memory);
        //     memory.Close();
        //     return result;
        // }

        private class Hex
        {
            public List<PointF> Points { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public PointF MidRight { get; set; }

            public PointF BottomLeft { get; set; }

            public PointF MidPoint { get; set; }

            public PointF TopRight { get; set; }

            public PointF TextLocation { get; set; }
        }
    }
}