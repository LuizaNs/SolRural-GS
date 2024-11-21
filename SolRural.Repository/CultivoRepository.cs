using MongoDB.Driver;
using SolRural.Models;

namespace SolRural.Repository
{
    public class CultivoRepository : IRepository<Cultivo>
    {
        private readonly IMongoCollection<Cultivo> _collection;

        public CultivoRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<Cultivo>("Cultivo");
        }

        public async Task<List<Cultivo>> GetAllAsync() =>
            await _collection.Find(FilterDefinition<Cultivo>.Empty).ToListAsync();

        public async Task<Cultivo> GetByIdAsync(string id)
        {
            var filter = Builders<Cultivo>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Cultivo entity) =>
            await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(string id, Cultivo entity)
        {
            var filter = Builders<Cultivo>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Cultivo>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
