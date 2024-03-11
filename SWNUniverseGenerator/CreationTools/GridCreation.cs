using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;
using VectSharp;
using VectSharp.SVG;

namespace SWNUniverseGenerator.CreationTools
{
    public class GridCreation
    {
        private static float _hypotenuse = 100; // hypotenuse for each side of the hex
        private const int IntAngle = 30; // angle in degrees because I can actually write that down
        private const double IntRadians = IntAngle * (Math.PI / 180); // degrees to radians
        private static readonly float Cosign = (float)Math.Cos(IntRadians); // cos(30)
        private static readonly float Sine = (float)Math.Sin(IntRadians); // sin(30)

        public String CreateGrid(string universeId, UniverseContext universeContext)
        {
            var gridX = 0;
            var gridY = 0;
            using (var univRepo = new Repository<Universe>(universeContext))
            {
                gridX = ((Universe)univRepo.Search(u => u.Id == universeId).First()).GridX;
                gridY = ((Universe)univRepo.Search(u => u.Id == universeId).First()).GridY;
            }

            int width =
                (int)((gridX * ((_hypotenuse * Sine) + _hypotenuse)) + (_hypotenuse * Sine) + _hypotenuse);
            int height = (int)((gridY * (2 * _hypotenuse * Cosign)) + (_hypotenuse * Cosign) + _hypotenuse);
            float gridPenThickness = _hypotenuse * 0.05F;
            float starSize = _hypotenuse * 0.175F;
            float planetSize = _hypotenuse * 0.075F;
            float orbitSize = starSize;
            float orbitThickness = _hypotenuse * 0.001F;
            Size bgStarThickness = new Size(_hypotenuse * 0.001, _hypotenuse * 0.001);

            // while (width * height > 500000000)
            // {
            //     _hypotenuse -= 10;
            //
            //     width =
            //         (int)((gridX * ((_hypotenuse * Sine) + _hypotenuse)) + (_hypotenuse * Sine) + _hypotenuse);
            //     height = (int)((gridY * (2 * _hypotenuse * Cosign)) + (_hypotenuse * Cosign) + _hypotenuse);
            //
            //     gridPenThickness = _hypotenuse * 0.05F;
            //     starSize = _hypotenuse * 0.25F;
            //     planetSize = _hypotenuse * 0.1F;
            //     orbitSize = starSize;
            //     // orbitThickness = _hypotenuse * 0.001F;
            //     // bgStarThickness = _hypotenuse * 0.001F;
            // }
            
            // Colour wheel
            Colour bgStarBrush = Colours.White;
            Colour blueStarBrush = Colour.FromRgb(21, 167, 255);
            Colour whiteStarBrush = Colours.WhiteSmoke;
            Colour yellowStarBrush = Colour.FromRgb(255, 241, 104);
            Colour lightOrangeStarBrush = Colour.FromRgb(255, 185, 50);
            Colour orangeRedStarBrush = Colour.FromRgb(255, 134, 47);
            Colour yellowWhiteStarBrush = Colour.FromRgb(255, 251, 174);
            Colour blueWhiteStarBrush = Colour.FromRgb(150, 219, 255);
            Colour planetBrush = Colour.FromRgb(0, 66, 7);
            Colour textBrush = Colours.White;
            Colour gridPen = Colours.White;
            Colour orbitPen = Colours.White;
            Colour background = Colour.FromRgb(8, 8, 17);

            Page grid = new Page(width, height);
            Graphics g = grid.Graphics;
            grid.Background = background;


            List<Hex> hexList = new();
            Point startingPoint = new Point(_hypotenuse / 2, _hypotenuse / 2);

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

                g.FillRectangle(new Point()
                    {
                        X = new Random().Next(0, width),
                        Y = new Random().Next(0, height)
                    }, new Size(bgStarThickness.Width * variation, bgStarThickness.Height * variation), bgStarBrush
                );
            }

            // Generate stars and planets
            foreach (var hex in hexList)
            {
                GraphicsPath hexPath = new GraphicsPath();

                // Create the Hex Path
                hexPath.MoveTo(hex.Points.First());
                foreach (Point point in hex.Points.Skip(1))
                {
                    hexPath.LineTo(point.X, point.Y);
                }

                hexPath.LineTo(hex.Points.First());

                // Draw the Hex
                g.StrokePath(hexPath, gridPen, gridPenThickness);

                // Go through each zone
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
                        star = (Star)new Repository<Star>(universeContext)
                            .Search(s => s.ZoneId == zoneId).ToList().First();

                    // Planets
                    if (planets.Count > 0)
                    {
                        for (int i = 1; i <= planets.Count; i++)
                        {
                            float modifer = i == 1 ? 2F : i * 1.5F;
                            float currentOrbit = orbitSize * modifer;

                            GraphicsPath orbitPath = new GraphicsPath();

                            orbitPath.Arc(hex.MidPoint, currentOrbit, 0, 360);
                            g.StrokePath(orbitPath, orbitPen);

                            int angle = new Random().Next(0, 360);
                            double radian = (angle * (Math.PI / 180));

                            Point planetPoint = new Point(hex.MidPoint.X + (currentOrbit * (float)Math.Cos(radian)),
                                hex.MidPoint.Y + (currentOrbit * (float)Math.Sin(radian)));

                            GraphicsPath planetPath = new GraphicsPath();

                            planetPath.Arc(planetPoint, planetSize, 0, 360);
                            g.FillPath(planetPath, planetBrush);
                        }
                    }

                    // Stars
                    if (star != null)
                    {
                        Brush starBrush = orangeRedStarBrush;

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

                        GraphicsPath starPath = new GraphicsPath();
                        starPath.Arc(hex.MidPoint, starSize, 0, 360);
                        g.FillPath(starPath, starBrush);
                        
                        g.StrokeText(hex.TextLocation, star.Name,
                            new Font(FontFamily.ResolveFontFamily(FontFamily.StandardFontFamilies.TimesBold),
                                _hypotenuse * 0.13F),
                            textBrush
                        );
                    }
                }
            }

            var imagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                            + "/"
                            + universeId
                            + ".svg";

            grid.SaveAsSVG(imagePath);

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
        private static Hex CreateHex(Point startingPoint, List<Hex> hexes, (int x, int y) xAndY)
        {
            Hex hex = new Hex();
            double startX = startingPoint.X;
            double startY = startingPoint.Y;

            // Points starting from top right going counter-clockwise
            hex.Points = new List<Point>()
            {
                new(startX + (_hypotenuse * Sine + _hypotenuse), startY),
                new(startX + (_hypotenuse * Sine), startY),
                new(startX, startY + (_hypotenuse * Cosign)),
                new(startX + (_hypotenuse * Sine), startY + (2 * _hypotenuse * Cosign)),
                new(startX + (_hypotenuse * Sine + _hypotenuse), startY + (2 * _hypotenuse * Cosign)),
                new(startX + (2 * _hypotenuse * Sine + _hypotenuse), startY + (_hypotenuse * Cosign))
            };

            hex.MidRight = new Point(startX + ((_hypotenuse * Sine) + _hypotenuse), startY + (_hypotenuse * Cosign));
            hex.BottomLeft = new Point(startX, startY + (2 * Cosign * _hypotenuse));
            hex.MidPoint = new Point(startX + (_hypotenuse * Sine) + (_hypotenuse / 2),
                startY + (_hypotenuse * Cosign));
            hex.TopRight = new Point(hex.MidRight.X, startY - (_hypotenuse * Cosign));
            hex.TextLocation =
                new Point(hex.MidPoint.X - (_hypotenuse * 0.55F), hex.MidPoint.Y + (_hypotenuse * 0.5F));
            hex.X = xAndY.x;
            hex.Y = xAndY.y;

            hexes.Add(hex);

            return hex;
        }

        private static Rectangle CircleToRectangle(Point midpoint, float radius)
        {
            return new Rectangle(midpoint.X - radius,
                midpoint.Y - radius,
                radius * 2,
                radius * 2);
        }

        private class Hex
        {
            public List<Point> Points { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public Point MidRight { get; set; }

            public Point BottomLeft { get; set; }

            public Point MidPoint { get; set; }

            public Point TopRight { get; set; }

            public Point TextLocation { get; set; }
        }
    }
}