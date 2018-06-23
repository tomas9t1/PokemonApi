using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonApi.DTO;

namespace PokemonApi.DataApi.CacheHandler
{
    public interface IPokemonDataService
    {
        Task<List<PokemonModelDTO>> GetAllPokemonsAsync();
        Task FillPokedexAsync(List<PokemonModelDTO> pokemonList);
        Task<List<PokemonModelDTO>> SearchPokemonByNameAsync(string searchText);
        Task<List<PokemonModelDTO>> GetLegendaryPokemonsAsync();
        Task<List<PokemonModelDTO>> GetPokemonsByTypeAsync(string type);
        Task<List<PokemonModelDTO>> GetPokemonsByMulyipleTypesAsync(List<string> types);
        Task<Dictionary<string, List<string>>> GetAllHeadersAsync();
        Task<List<PokemonModelDTO>> GetPokemonsWithEqualHeadersAsync();
    }
}