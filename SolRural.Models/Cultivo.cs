using MongoDB.Bson.Serialization.Attributes;

namespace SolRural.Models
{
    public class Cultivo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("tipoCultivo")]
        public required string TipoCultivo { get; set; }

        [BsonElement("sazonalidade")]
        public required string Sazonalidade { get; set; }

        [BsonElement("areaOcupada")]
        public required double AreaOcupada { get; set; }

        public Fazenda Fazenda { get; set; }
    }
}
