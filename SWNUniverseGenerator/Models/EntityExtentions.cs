using System;
using System.Runtime.CompilerServices;

namespace SWNUniverseGenerator.Models
{
    public static class EntityExtension
        {
            public static string GenerateId(this IEntity myInterface)
            {
                // Create a Guid and chop everything from the first hyphen and on
                var id = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                
                // Set the ID based on the type of Object
                id = myInterface switch
                {
                    Universe _ => ("UN-" + id),
                    Planet _ => ("P-" + id),
                    Star _ => ("S-" + id),
                    Character _ => ("U-" + id),
                    City _ => ("C-" + id),
                    Problem _ => ("Q-" + id),
                    PointOfInterest _ => ("I-" + id),
                    Ship _ => ("SH-" + id),
                    Job _ => ("J-" + id),
                    Alien _ => ("X-" + id),
                    // Location _ => ("L-" + id),
                    _ => ("Z-" + id)
                };

                // Set the ID
                return id;
            }
        }
}