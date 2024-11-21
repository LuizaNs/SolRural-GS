using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SolRural.Data;
using SolRural.Models;

namespace SolRural.Service
{
    public class CultivoService
    {
        private readonly IMongoCollection<Cultivo> _cultivoCollection;

        public CultivoService(IOptions<CultivoDbSettings> cultivoService)
        {
            var mongoClient = new MongoClient(cultivoService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(cultivoService.Value.DatabaseName);

            _cultivoCollection = mongoDatabase.GetCollection<Cultivo>(cultivoService.Value.CultivoCollectionName);
        }

        public async Task<List<Cultivo>> GetAsync() => await _cultivoCollection.Find(x => true).ToListAsync();

        public async Task<Cultivo> GetAsync(string Id) => await _cultivoCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Cultivo cultivo) => await _cultivoCollection.InsertOneAsync(cultivo);

        public async Task UpdateAsync(string Id, Cultivo cultivo) => await _cultivoCollection.ReplaceOneAsync(x => x.Id == Id, cultivo);

        public async Task RemoveAsync(string Id) => await _cultivoCollection.DeleteOneAsync(x => x.Id == Id);

    }
}

