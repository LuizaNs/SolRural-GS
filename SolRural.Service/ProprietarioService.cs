using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SolRural.Data;
using SolRural.Models;

namespace SolRural.Service
{
    public class ProprietarioService
    {
        private readonly IMongoCollection<Proprietario> _proprietarioCollection;

        public ProprietarioService(IOptions<ProprietarioDbSettings> proprietarioService)
        {
            var mongoClient = new MongoClient(proprietarioService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(proprietarioService.Value.DatabaseName);

            _proprietarioCollection = mongoDatabase.GetCollection<Proprietario>(proprietarioService.Value.ProprietarioCollectionName);
        }

        public async Task<List<Proprietario>> GetAsync() => await _proprietarioCollection.Find(x => true).ToListAsync();

        public async Task<Proprietario> GetAsync(string Id) => await _proprietarioCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Proprietario proprietario) => await _proprietarioCollection.InsertOneAsync(proprietario);

        public async Task UpdateAsync(string Id, Proprietario proprietario) => await _proprietarioCollection.ReplaceOneAsync(x => x.Id == Id, 
            proprietario);

        public async Task RemoveAsync(string Id) => await _proprietarioCollection.DeleteOneAsync(x => x.Id == Id);

    }
}

