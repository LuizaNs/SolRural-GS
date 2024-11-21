using MongoDB.Driver;
using SolRural.Models;

namespace SolRural.Repository
{
    public class MedicaoEnergRepository : IRepository<MedicaoEnerg>
    {
        private readonly IMongoCollection<MedicaoEnerg> _collection;

        public MedicaoEnergRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<MedicaoEnerg>("MedicaoEnerg");
        }

        public async Task<List<MedicaoEnerg>> GetAllAsync() =>
            await _collection.Find(FilterDefinition<MedicaoEnerg>.Empty).ToListAsync();

        public async Task<MedicaoEnerg> GetByIdAsync(string id)
        {
            var filter = Builders<MedicaoEnerg>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(MedicaoEnerg entity) =>
            await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(string id, MedicaoEnerg entity)
        {
            var filter = Builders<MedicaoEnerg>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<MedicaoEnerg>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
