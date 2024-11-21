using MongoDB.Driver;
using SolRural.Models;

namespace SolRural.Repository
{
    public class InstalacaoRepository : IRepository<Instalacao>
    {
        private readonly IMongoCollection<Instalacao> _collection;

        public InstalacaoRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<Instalacao>("Instalacao");
        }

        public async Task<List<Instalacao>> GetAllAsync() =>
            await _collection.Find(FilterDefinition<Instalacao>.Empty).ToListAsync();

        public async Task<Instalacao> GetByIdAsync(string id)
        {
            var filter = Builders<Instalacao>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Instalacao entity) =>
            await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(string id, Instalacao entity)
        {
            var filter = Builders<Instalacao>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Instalacao>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
