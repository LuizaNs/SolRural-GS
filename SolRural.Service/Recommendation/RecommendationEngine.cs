using Microsoft.ML;
using SolRural.Factories;
using SolRural.Models;

namespace SolRural.Service.Recommendation
{
    public class RecommendationEngine
    {
        private readonly MLContext _mlContext = new MLContext();
        private ITransformer _model;

        private readonly IRecommendationModelFactory _factory;

        public RecommendationEngine(IRecommendationModelFactory factory)
        {
            _factory = factory;
        }

        public void PrepareTrainModel(IEnumerable<CultivoInstalacao> cultivoInstalacoes)
        {
            var treinoDados = new List<CultivoInstalacao>();
            foreach (var item in cultivoInstalacoes)
            {
                treinoDados.Add(new CultivoInstalacao
                {
                    CultivoId = item.CultivoId,
                    InstalacaoId = item.InstalacaoId,
                    Label = item.Label
                });
            }

            TrainModel(treinoDados);
        }

        public void TrainModel(IEnumerable<CultivoInstalacao> cultivoInstalacoes)
        {
            var trainingData = _mlContext.Data.LoadFromEnumerable(cultivoInstalacoes);
            _model = _factory.CreateModel(_mlContext, trainingData);
        }

        public float Predict(string cultivoId, string instalacaoId)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<CultivoInstalacao, CultivoPrediction>(_model);

            var prediction = predictionEngine.Predict(new CultivoInstalacao
            {
                CultivoId = cultivoId,
                InstalacaoId = instalacaoId
            });

            return prediction.Score;
        }
    }
    public class CultivoPrediction
    {
        public float Score { get; set; }
    }
}





