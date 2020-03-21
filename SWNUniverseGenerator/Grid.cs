using System;

namespace SWNUniverseGenerator
{
    /// <summary>
    /// Grid object that stores an X and Y length
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Constructor requires an X and Y value to create the grid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Grid(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The X length of a grid
        /// </summary>
        public Int32 X { get; set; }

        /// <summary>
        /// The Y length of a grid
        /// </summary>
        public Int32 Y { get; set; }
    }
}