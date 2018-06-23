using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonApi.Repository.MongoDB.Entities;

namespace PokemonApi.Repository.MongoDB
{
    public interface IPokemonRepository
    {
        Task InsertManyAsync(List<Pokemon> pokemonList);
        Task InsertOneAsync(Pokemon pokemon);
        Task<List<Pokemon>> GetAllAsync();
        Task<List<Pokemon>> GetAllLengedaryAsync();
        Task<List<Pokemon>> GetAllWithNameSearchStringAsync(string searchText);
        Task<List<Pokemon>> GetAllByTypeAsync(string type);
        Task<List<Pokemon>> GetAllByMultipleTypesAsync(List<string> types);
        Task WipeDatabaseAsync();

    }
}