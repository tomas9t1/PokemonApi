using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokemonApi.DataApi.CacheHandler;
using PokemonApi.DataApi.CSVReader;
using PokemonApi.DTO;

namespace PokemonApi.DataApi.Controllers
{
    [Route("api/[controller]")]
    public class PokedexController : Controller
    {

        private readonly IPokemonDataService _pokemonData;
        private readonly ICSVReader _csvReader;

        public PokedexController(IPokemonDataService pokemonData, ICSVReader csvReader)
        {
            _pokemonData = pokemonData;
            _csvReader = csvReader;
        }

        [HttpPost]
        [Route("Fill")]
        public void FillData()
        {
            var pokedexData = _csvReader.ConvertCSVToObject("pokemons.csv");
            _pokemonData.FillPokedexAsync(pokedexData);
        }

        [HttpGet]
        [Route("All")]
        public async Task<List<PokemonModelDTO>> GetAll()
        {
            return await _pokemonData.GetAllPokemonsAsync();
        }

    }
}