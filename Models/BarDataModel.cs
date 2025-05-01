using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactApp1.Server.Models
{
    public class BarDataModel
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Country { get; set; }
        public int HotDog { get; set; }
        public string HotDogColor { get; set; }
        public int Burger { get; set; }
        public string BurgerColor { get; set; }
        public int Kebab { get; set; }
        public string KebabColor { get; set; }
        public int Donut { get; set; }
        public string DonutColor { get; set; }
    }
}
