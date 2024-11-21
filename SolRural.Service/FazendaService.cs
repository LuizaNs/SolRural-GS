using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SolRural.Data;
using SolRural.Models;

namespace SolRural.Service
{
    public class FazendaService
    {
        private readonly IMongoCollection<Fazenda> _fazendaCollection;

        public FazendaService(IOptions<FazendaDbSettings> fazendaService)
        {
            var mongoClient = new MongoClient(fazendaService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(fazendaService.Value.DatabaseName);

            _fazendaCollection = mongoDatabase.GetCollection<Fazenda>(fazendaService.Value.FazendaCollectionName);
        }

        public async Task<List<Fazenda>> GetAsync() => await _fazendaCollection.Find(x => true).ToListAsync();

        public async Task<Fazenda> GetAsync(string Id) => await _fazendaCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Fazenda fazenda) => await _fazendaCollection.InsertOneAsync(fazenda);

        public async Task UpdateAsync(string Id, Fazenda fazenda) => await _fazendaCollection.ReplaceOneAsync(x => x.Id == Id, fazenda);

        public async Task RemoveAsync(string Id) => await _fazendaCollection.DeleteOneAsync(x => x.Id == Id);

    }
}


