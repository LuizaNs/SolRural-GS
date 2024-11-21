using MongoDB.Driver;
using SolRural.Models;

namespace SolRural.Repository
{
    public class ProprietarioRepository : IRepository<Proprietario>
    {
        private readonly IMongoCollection<Proprietario> _collection;

        public ProprietarioRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _collection = database.GetCollection<Proprietario>("Proprietario");
        }

        public async Task<List<Proprietario>> GetAllAsync() =>
            await _collection.Find(FilterDefinition<Proprietario>.Empty).ToListAsync();

        public async Task<Proprietario> GetByIdAsync(string id)
        {
            var filter = Builders<Proprietario>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Proprietario entity) =>
            await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(string id, Proprietario entity)
        {
            var filter = Builders<Proprietario>.Filter.Eq("Id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Proprietario>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
