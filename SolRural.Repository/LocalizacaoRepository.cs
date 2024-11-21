using MongoDB.Driver;
using SolRural.Models;

namespace SolRural.Repository
{
    public class LocalizacaoRepository : IRepository<Localizacao>
    {
        private readonly IMongoCollection<Localizacao> _collection;

        public LocalizacaoRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<Localizacao>("Localizacao");
        }

        public async Task<List<Localizacao>> GetAllAsync() =>
            await _collection.Find(FilterDefinition<Localizacao>.Empty).ToListAsync();

        public async Task<Localizacao> GetByIdAsync(string id)
        {
            var filter = Builders<Localizacao>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Localizacao entity) =>
            await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(string id, Localizacao entity)
        {
            var filter = Builders<Localizacao>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Localizacao>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
