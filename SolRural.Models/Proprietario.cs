using MongoDB.Bson.Serialization.Attributes;

namespace SolRural.Models
{
    public class Proprietario
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome")]
        public required string Nome { get; set; }

        [BsonElement("telefone")]
        public double Telefone { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}
