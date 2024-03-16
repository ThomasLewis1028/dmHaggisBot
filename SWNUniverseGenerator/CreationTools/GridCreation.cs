using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SWNUniverseGenerator.Database;
using SWNUniverseGenerator.Models;
using VectSharp;
using VectSharp.Filters;
using VectSharp.Raster.ImageSharp;
using VectSharp.SVG;

namespace SWNUniverseGenerator.CreationTools
{
    public class GridCreation
    {
        private static readonly Random Rand = new();
        private const float Hypotenuse = 150; // hypotenuse for each side of the hex
        private const int IntAngle = 30; // angle in degrees because I can actually write that down
        private const double IntRadians = IntAngle * (Math.PI / 180); // degrees to radians
        private static readonly float Cosign = (float)Math.Cos(IntRadians); // cos(30)
        private static readonly float Sine = (float)Math.Sin(IntRadians); // sin(30)

        public String CreateGrid(string universeId, UniverseContext universeContext)
        {
            int gridX;
            int gridY;
            using (var univRepo = new Repository<Universe>(universeContext))
            {
                gridX = ((Universe)univRepo.Search(u => u.Id == universeId).First()).GridX;
                gridY = ((Universe)univRepo.Search(u => u.Id == universeId).First()).GridY;
            }

            int width =
                (int)((gridX * ((Hypotenuse * Sine) + Hypotenuse)) + (Hypotenuse * Sine) + Hypotenuse);
            int height = (int)((gridY * (2 * Hypotenuse * Cosign)) + (Hypotenuse * Cosign) + Hypotenuse);
            float gridPenThickness = Hypotenuse * 0.01F;
            float starRadius = Hypotenuse * 0.15F;
            float planetRadius = starRadius * 0.25F;
            float orbitRadius = starRadius + planetRadius * 3F;

            // Colour wheel
            Colour bgStarColor;
            Colour blueStarColor = Colour.FromRgb(21, 167, 255);
            Colour whiteStarColor = Colours.WhiteSmoke;
            Colour yellowStarColor = Colour.FromRgb(255, 241, 104);
            Colour lightOrangeStarColor = Colour.FromRgb(255, 185, 50);
            Colour orangeRedStarColor = Colour.FromRgb(255, 134, 47);
            Colour yellowWhiteStarColor = Colour.FromRgb(255, 251, 174);
            Colour blueWhiteStarColor = Colour.FromRgb(150, 219, 255);
            Colour planetColor = Colour.FromRgb(0, 66, 7);
            Colour textColor = Colours.WhiteSmoke;
            Colour gridPen = Colours.White;
            Colour orbitPen = Colours.White.WithAlpha(0.25);
            Colour background = Colour.FromRgb(8, 8, 17);

            Page grid = new Page(width, height);
            Graphics g = grid.Graphics;
            Graphics starGraphics = new Graphics();
            Graphics bgGraphics = new Graphics();
            grid.Background = background;

            List<Hex> hexList = new();
            Point startingPoint = new Point(Hypotenuse / 2, Hypotenuse / 2);

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
            double bgStarCount = (width * height) * .0075;

            for (int i = 0; i < bgStarCount; i++)
            {
                // Set the Class of the Star
                int starRand = Rand.Next(0, 100);
                double opacityRand = Rand.NextDouble();
                bgStarColor = starRand switch
                {
                    >= 0 and < 1 => blueStarColor.WithAlpha(opacityRand),
                    >= 1 and < 2 => whiteStarColor.WithAlpha(opacityRand),
                    >= 2 and < 3 => yellowStarColor.WithAlpha(opacityRand),
                    >= 3 and < 6 => lightOrangeStarColor.WithAlpha(opacityRand),
                    >= 6 and < 13 => blueWhiteStarColor.WithAlpha(opacityRand),
                    >= 13 and < 25 => orangeRedStarColor.WithAlpha(opacityRand),
                    _ => yellowWhiteStarColor.WithAlpha(opacityRand)
                };


                bgGraphics.FillRectangle(new Point()
                    {
                        X = new Random().Next(0, width),
                        Y = new Random().Next(0, height)
                    }, new Size(1, 1), bgStarColor
                );
            }

            IFilter bgFilter = new GaussianBlurFilter(0.5);
            g.DrawGraphics(0, 0, bgGraphics, bgFilter);

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
                        float currentOrbit = orbitRadius;
                        for (int i = 1; i <= planets.Count; i++)
                        {
                            GraphicsPath orbitPath = new GraphicsPath();

                            Graphics orbitGraphics = new Graphics();
                            orbitPath.Arc(hex.MidPoint, currentOrbit, 0, 360);
                            orbitGraphics.StrokePath(orbitPath, orbitPen);

                            IFilter orbitFilter = new GaussianBlurFilter(.1);
                            g.DrawGraphics(0, 0, orbitGraphics, orbitFilter);

                            int angle = new Random().Next(0, 360);
                            double radian = angle * (Math.PI / 180);

                            Point planetPoint = new Point(
                                hex.MidPoint.X + (currentOrbit * (float)Math.Cos(radian)),
                                hex.MidPoint.Y + (currentOrbit * (float)Math.Sin(radian)));

                            GraphicsPath planetPath = new GraphicsPath();

                            planetPath.Arc(planetPoint, planetRadius, 0, 360);
                            g.FillPath(planetPath, planetColor);

                            currentOrbit += planetRadius * 2;
                        }
                    }

                    // Stars
                    if (star != null)
                    {
                        Colour starColor = orangeRedStarColor;

                        starColor = star.StarColor switch
                        {
                            Star.StarColorEnum.Blue => blueStarColor,
                            Star.StarColorEnum.BlueWhite => blueWhiteStarColor,
                            Star.StarColorEnum.White => whiteStarColor,
                            Star.StarColorEnum.YellowWhite => yellowWhiteStarColor,
                            Star.StarColorEnum.Yellow => yellowStarColor,
                            Star.StarColorEnum.LightOrange => lightOrangeStarColor,
                            Star.StarColorEnum.OrangeRed => orangeRedStarColor,
                            _ => starColor
                        };

                        RadialGradientBrush radialStarBrush = new RadialGradientBrush(
                            hex.MidPoint,
                            hex.MidPoint,
                            starRadius,
                            new GradientStop(Colours.White, 0),
                            new GradientStop(starColor, 0.75),
                            new GradientStop(starColor, 1));

                        GraphicsPath starPath = new GraphicsPath();
                        starPath.Arc(hex.MidPoint, starRadius, 0, 360);
                        starGraphics.FillPath(starPath, radialStarBrush);

                        // Create blur the stars to give a less "solid" look
                        IFilter filter = new GaussianBlurFilter(1.25);
                        g.DrawGraphics(0, 0, starGraphics, filter);


                        Font font = new Font(FontFamily.ResolveFontFamily(FontFamily.StandardFontFamilies.TimesBold),
                            Hypotenuse * 0.13F);
                        Size textSize = font.MeasureText(star.Name);

                        g.FillText(
                            // Center the text at the midpoint
                            hex.TextLocation.X - textSize.Width * 0.5,
                            hex.TextLocation.Y,
                            star.Name,
                            font,
                            textColor
                        );
                    }
                }
            }

            var svgPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                          + "/"
                          + universeId
                          + ".svg";

            grid.SaveAsSVG(svgPath);

            return svgPath;
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
                new(startX + (Hypotenuse * Sine + Hypotenuse), startY),
                new(startX + (Hypotenuse * Sine), startY),
                new(startX, startY + (Hypotenuse * Cosign)),
                new(startX + (Hypotenuse * Sine), startY + (2 * Hypotenuse * Cosign)),
                new(startX + (Hypotenuse * Sine + Hypotenuse), startY + (2 * Hypotenuse * Cosign)),
                new(startX + (2 * Hypotenuse * Sine + Hypotenuse), startY + (Hypotenuse * Cosign))
            };

            hex.MidRight = new Point(startX + ((Hypotenuse * Sine) + Hypotenuse), startY + (Hypotenuse * Cosign));
            hex.BottomLeft = new Point(startX, startY + (2 * Cosign * Hypotenuse));
            hex.MidPoint = new Point(startX + (Hypotenuse * Sine) + (Hypotenuse / 2),
                startY + (Hypotenuse * Cosign));
            hex.TopRight = new Point(hex.MidRight.X, startY - (Hypotenuse * Cosign));
            hex.TextLocation =
                new Point(hex.MidPoint.X, hex.MidPoint.Y + (Hypotenuse * 0.5F));
            hex.X = xAndY.x;
            hex.Y = xAndY.y;

            hexes.Add(hex);

            return hex;
        }

        public string GetPng(string universeId)
        {
            var svgPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                          + "/"
                          + universeId
                          + ".svg";

            Page page = Parser.FromFile(svgPath);

            var pngPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                          + "/"
                          + universeId
                          + ".png";
            page.SaveAsImage(pngPath);
            return pngPath;
        }

        /// <summary>
        /// Hex class. This is so that the points for an individual hex can be stored and retrieved automatically.
        /// </summary>
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