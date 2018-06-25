using System.Collections.Generic;
using PokemonApi.DTO;

namespace ApplicationService.PokedexModule
{
    public interface IPokedexService
    {
        List<PokemonModelDTO> GetAllPokemons();
        List<PokemonModelDTO> GetAllLegendary();
        List<PokemonModelDTO> GetByName(string name);
        List<PokemonModelDTO> GetByType(string type);
        List<PokemonModelDTO> GetByMultipleTypes(string type1, string type2);
        Dictionary<string, List<string>> GetHeaders();
        List<PokemonModelDTO> GetEqualHeaders();
        PokemonModelDTO GetPokemonByName(string name);
    }
}