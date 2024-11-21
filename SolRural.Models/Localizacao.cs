using MongoDB.Bson.Serialization.Attributes;

namespace SolRural.Models
{
    public class Localizacao
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("latitude")]
        public required double Latitude { get; set; }

        [BsonElement("longitude")]
        public required double Longitude { get; set; }
    }
}
