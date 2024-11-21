using Microsoft.ML;
using Microsoft.ML.Trainers;
using SolRural.Models;

namespace SolRural.Factories
{
    public class MatrixFactorizationModelFactory : IRecommendationModelFactory
    {
        public ITransformer CreateModel(MLContext mlContext, IDataView trainingData)
        {
            var pipeline = mlContext.Transforms.Conversion
                .MapValueToKey(outputColumnName: "cultivoIdEncoded", inputColumnName: nameof(CultivoInstalacao.CultivoId))
                .Append(mlContext.Transforms.Conversion
                    .MapValueToKey(outputColumnName: "instalacaoIdEncoded", inputColumnName: nameof(CultivoInstalacao.InstalacaoId)))
                .Append(mlContext.Recommendation().Trainers.MatrixFactorization(new MatrixFactorizationTrainer.Options
                {
                    LabelColumnName = nameof(CultivoInstalacao.Label),
                    MatrixColumnIndexColumnName = "cultivoIdEncoded",
                    MatrixRowIndexColumnName = "instalacaoIdEncoded",
                    NumberOfIterations = 20,
                    ApproximationRank = 100
                }));

            return pipeline.Fit(trainingData);
        }
    }
}
