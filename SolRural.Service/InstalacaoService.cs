using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SolRural.Data;
using SolRural.Models;

namespace SolRural.Service
{
    public class InstalacaoService
    {
        private readonly IMongoCollection<Instalacao> _instalacaoCollection;

        public InstalacaoService(IOptions<InstalacaoDbSettings> instalacaoService)
        {
            var mongoClient = new MongoClient(instalacaoService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(instalacaoService.Value.DatabaseName);

            _instalacaoCollection = mongoDatabase.GetCollection<Instalacao>(instalacaoService.Value.InstalacaoCollectionName);
        }

        public async Task<List<Instalacao>> GetAsync() => await _instalacaoCollection.Find(x => true).ToListAsync();

        public async Task<Instalacao> GetAsync(string Id) => await _instalacaoCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Instalacao instalacao) => await _instalacaoCollection.InsertOneAsync(instalacao);

        public async Task UpdateAsync(string Id, Instalacao instalacao) => await _instalacaoCollection.ReplaceOneAsync(x => x.Id == Id, instalacao);

        public async Task RemoveAsync(string Id) => await _instalacaoCollection.DeleteOneAsync(x => x.Id == Id);

    }
}

