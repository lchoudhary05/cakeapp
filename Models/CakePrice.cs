using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace CakeApp.Models
{
    class CakePrice
    {
        [BsonId]
        public int Id { get; set; }
        [Required]
        public string? Flavour { get; set; }
        [Required]
        public string? Size { get; set; }
    }
}