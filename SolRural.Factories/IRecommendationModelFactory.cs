using Microsoft.ML;

namespace SolRural.Factories
{
    public interface IRecommendationModelFactory
    {
        ITransformer CreateModel(MLContext mlContext, IDataView trainingData);
    }
}

