﻿using System;
using SWNUniverseGenerator.Models;

namespace SWNUniverseGenerator
{
    /// <summary>
    ///     This class and method handles the creation of randomly generated ID for each IEntity in the Generator
    /// </summary>
    [Obsolete]
    public class IdGen
    {
        /// <summary>
        ///     This method receives an generic IEntity item and then creates an ID based on its type and assigns it to
        ///     the Object.ID
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        [Obsolete]
        public static void DEADGenerateId<T>(T item) where T : IEntity
        {
            // Create a Guid and chop everything from the first hyphen and on
            var id = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            // Set the ID based on the type of Object
            id = item switch
            {
                Universe _ => "UN-" + id,
                Planet _ => "P-" + id,
                Star _ => "S-" + id,
                Character _ => "U-" + id,
                City _ => "C-" + id,
                LocationProblem _ => "Q-" + id,
                PointOfInterest _ => "I-" + id,
                Ship _ => "SH-" + id,
                Job _ => "J-" + id,
                Alien _ => "X-" + id,
                // Location _ => ("L-" + id),
                _ => "Z-" + id
            };

            // Set the ID
            item.Id = id;
        }
    }
}