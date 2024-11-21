using MongoDB.Bson.Serialization.Attributes;

namespace SolRural.Models
{
    public class Fazenda
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("area")]
        public required double Area { get; set; }

        public Proprietario Proprietario { get; set; }
        public Localizacao Localizacao { get; set; }
    }
}
