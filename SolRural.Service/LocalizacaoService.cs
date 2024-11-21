using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SolRural.Data;
using SolRural.Models;

namespace SolRural.Service
{
    public class LocalizacaoService
    {
        private readonly IMongoCollection<Localizacao> _localizacaoCollection;

        public LocalizacaoService(IOptions<LocalizacaoDbSettings> localizacaoService)
        {
            var mongoClient = new MongoClient(localizacaoService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(localizacaoService.Value.DatabaseName);

            _localizacaoCollection = mongoDatabase.GetCollection<Localizacao>(localizacaoService.Value.LocalizacaoCollectionName);
        }
        public async Task<List<Localizacao>> GetAsync() => await _localizacaoCollection.Find(x => true).ToListAsync();

        public async Task<Localizacao> GetAsync(string Id) => await _localizacaoCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Localizacao localizacao) => await _localizacaoCollection.InsertOneAsync(localizacao);

        public async Task UpdateAsync(string Id, Localizacao localizacao) => await _localizacaoCollection.ReplaceOneAsync(x => x.Id == Id, localizacao);

        public async Task RemoveAsync(string Id) => await _localizacaoCollection.DeleteOneAsync(x => x.Id == Id);

    }
}



