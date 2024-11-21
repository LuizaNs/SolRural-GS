using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SolRural.Data;
using SolRural.Models;

namespace SolRural.Service
{
    public class MedicaoEnergService
    {
        private readonly IMongoCollection<MedicaoEnerg> _medicaoEnergCollection;

        public MedicaoEnergService(IOptions<MedicaoEnergDbSettings> medicaoEnergService)
        {
            var mongoClient = new MongoClient(medicaoEnergService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(medicaoEnergService.Value.DatabaseName);

            _medicaoEnergCollection = mongoDatabase.GetCollection<MedicaoEnerg>(medicaoEnergService.Value.MedicaoEnergCollectionName);
        }

        public async Task<List<MedicaoEnerg>> GetAsync() => await _medicaoEnergCollection.Find(x => true).ToListAsync();

        public async Task<MedicaoEnerg> GetAsync(string Id) => await _medicaoEnergCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(MedicaoEnerg medicaoEnerg) => await _medicaoEnergCollection.InsertOneAsync(medicaoEnerg);

        public async Task UpdateAsync(string Id, MedicaoEnerg medicaoEnerg) => await _medicaoEnergCollection.ReplaceOneAsync(x => x.Id == Id, 
            medicaoEnerg);

        public async Task RemoveAsync(string Id) => await _medicaoEnergCollection.DeleteOneAsync(x => x.Id == Id);

    }
}

