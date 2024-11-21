using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SolRural.Models
{
    public class CultivoInstalacao
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CultivoId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string InstalacaoId { get; set; }
        public float Label { get; set; }
    }
}
