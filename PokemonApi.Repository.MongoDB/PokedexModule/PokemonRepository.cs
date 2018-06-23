using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using PokemonApi.DTO;
using PokemonApi.Repository.MongoDB.Entities;

namespace PokemonApi.Repository.MongoDB
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly MongoContext _mongoContext;

        public PokemonRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task InsertManyAsync(List<Pokemon> pokemonList)
        {
            await _mongoContext.Pokemon.InsertManyAsync(pokemonList.AsEnumerable());
        }

        public async Task InsertOneAsync(Pokemon pokemon)
        {
            await _mongoContext.Pokemon.InsertOneAsync(pokemon);
        }

        public async Task<List<Pokemon>> GetAllAsync()
        {
            var filter = Builders<Pokemon>.Filter.Where(_ => true);
            return await _mongoContext.Pokemon.Find(filter).ToListAsync();
        }
        
        public async Task<List<Pokemon>> GetAllLengedaryAsync()
        {
            var filter = Builders<Pokemon>.Filter.Where(x => x.Legendary);
            return await _mongoContext.Pokemon.Find(filter).ToListAsync();
        }
        
        public async Task<List<Pokemon>> GetAllWithNameSearchStringAsync(string searchText)
        {
            var filter = Builders<Pokemon>.Filter.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
            return await _mongoContext.Pokemon.Find(filter).ToListAsync();
        }
        
        public async Task<List<Pokemon>> GetAllByTypeAsync(string type)
        {
            var filter = Builders<Pokemon>.Filter.Where(x => x.Type1.ToLower().Contains(type.ToLower()) || x.Type2.ToLower().Contains(type.ToLower()) );
            return await _mongoContext.Pokemon.Find(filter).ToListAsync();
        }
        
        public async Task<List<Pokemon>> GetAllByMultipleTypesAsync(List<string> types)
        {
            var filter = Builders<Pokemon>.Filter.Where(x => types.Contains(x.Type1) || types.Contains(x.Type2));
            return await _mongoContext.Pokemon.Find(filter).ToListAsync();
        }

        public async Task WipeDatabaseAsync()
        {
            var filter = Builders<Pokemon>.Filter.Where(_ => true);
            await _mongoContext.Pokemon.DeleteManyAsync(filter);
        }
        
    }
}