using MongoDB.Driver;
using SolRural.Models;

namespace SolRural.Repository
{
    public class FazendaRepository : IRepository<Fazenda>
    {
        private readonly IMongoCollection<Fazenda> _collection;

        public FazendaRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<Fazenda>("Fazenda");
        }

        public async Task<List<Fazenda>> GetAllAsync() =>
            await _collection.Find(FilterDefinition<Fazenda>.Empty).ToListAsync();

        public async Task<Fazenda> GetByIdAsync(string id)
        {
            var filter = Builders<Fazenda>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Fazenda entity) =>
            await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(string id, Fazenda entity)
        {
            var filter = Builders<Fazenda>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Fazenda>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}

