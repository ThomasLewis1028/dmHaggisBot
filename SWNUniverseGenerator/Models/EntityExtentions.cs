using System;

namespace SWNUniverseGenerator.Models
{
    public static class EntityExtension
        {
            public static string GenerateId(this IEntity myInterface)
            {
                // Create a Guid
                var id = Guid.NewGuid().ToString().ToUpper();
                
                // Set the ID based on the type of Object
                id = myInterface switch
                {
                    Universe _ => ("UN-" + id),
                    Planet _ => ("P-" + id),
                    Star _ => ("S-" + id),
                    Character _ => ("U-" + id),
                    City _ => ("C-" + id),
                    LocationProblem _ => ("Q-" + id),
                    PointOfInterest _ => ("I-" + id),
                    Ship _ => ("SH-" + id),
                    Job _ => ("J-" + id),
                    Alien _ => ("X-" + id),
                    Zone _ => ("SC-" + id),
                    // Location _ => ("L-" + id),
                    _ => ("Z-" + id)
                };

                // Set the ID
                return id;
            }
        }
}