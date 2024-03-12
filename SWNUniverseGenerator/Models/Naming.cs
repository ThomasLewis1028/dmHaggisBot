using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SWNUniverseGenerator.Models
{
    public class Naming : BaseEntity
    {
        public string NameType { get; set; }
        public string Name { get; set; }
    }
}