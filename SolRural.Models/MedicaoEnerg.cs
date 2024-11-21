using MongoDB.Bson.Serialization.Attributes;

namespace SolRural.Models
{
    public class MedicaoEnerg
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("dataMedicao")]
        public required DateOnly DataMedicao { get; set; }

        [BsonElement("energGerada")]
        public required double EnergGerada { get; set; }

        [BsonElement("energConsumida")]
        public required double EnergConsumida { get; set; }

        [BsonElement("energExcedida")]
        public required double EnergExcedida { get; set; }

        public Instalacao Instalacao { get; set; }
    }
}
