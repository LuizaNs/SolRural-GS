using MongoDB.Bson.Serialization.Attributes;

namespace SolRural.Models
{
    public class Instalacao
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("dataInstalacao")]
        public required DateOnly DataInstalacao { get; set; }

        [BsonElement("status")]
        public required string Status { get; set; }

        public Fazenda Fazenda { get; set; }
    }
}
