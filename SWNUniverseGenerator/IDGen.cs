using System;
using System.Linq;

namespace SWNUniverseGenerator
{
    public class IDGen
    {
        public static void GenerateID<T>(T item) where T : IEntity
        {
            var id = Guid.NewGuid().ToString().Substring(0, 8);
            
            if (item is Planet)
                id = "P-" + id;
            else if (item is Star)
                id = "S-" + id;
            else if (item is Character)
                id = "U-" + id;
            else if (item is City)
                id = "C-" + id;
            else if (item is Problem)
                id = "Q-" + id;
            else
                id = "Z-" + id;

            item.ID = id.ToUpper();
        }
    }
}