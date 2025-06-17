using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CakeApp.Models
{
    public enum Sizes
    {
        FullSheet, HalfSheet, OneFourth, DoubleSheet

    }
    public enum Flavour
    {
        Choclate, Vanilla, RockyRoad, MintChip, CookieMonster,
    }
    public class Cakes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int? Userid { get; set; }
        public int Orderid { get; set; }
        [Required]
        [BsonRepresentation(BsonType.String)]
        public Flavour CakeFlavour { get; set; }
        [BsonRepresentation(BsonType.String)]
        [Required]
        public Sizes CakeSize { get; set; }
        [Required]
        public string? UserName { get; set; }
        public string? OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime OrderDue { get; set; }


        // private float Price;
        public float Price { get; set; }

        // public float Price
        // {
        //     get;
        //     set
        //     {
        //         price = priceList.cakeSize;
        //     }
        // }
        Dictionary<string, int> priceList = new Dictionary<string, int>
    {
        { "FullSheet", 40 },
        { "HalfSheet", 32 },
        { "OneFourth", 25 },
        { "DoubleSheet", 50 }
    };

    }
}