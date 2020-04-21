using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator
{
    /// <summary>
    /// This class and method handles the creation of randomly generated ID for each IEntity in the Generator
    /// </summary>
    internal class IDGen
    {
        /// <summary>
        /// This method receives an generic IEntity item and then creates an ID based on its type and assigns it to
        /// the Object.ID
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public static void GenerateID<T>(T item) where T : IEntity
        {
            // Create a Guid and chop everything from the first hyphen and on
            var id = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            // Set the ID based on the type of Object
            id = item switch
            {
                Planet _ => ("P-" + id),
                Star _ => ("S-" + id),
                Character _ => ("U-" + id),
                City _ => ("C-" + id),
                Problem _ => ("Q-" + id),
                PointOfInterest _ => ("I-"+id),
                Ship _ => ("CS-"+id),
                _ => ("Z-" + id)
            };

            // Set the ID
            item.ID = id;
        }
    }
}