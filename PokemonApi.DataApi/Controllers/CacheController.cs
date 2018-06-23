using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokemonApi.DataApi.CacheHandler;
using PokemonApi.DataApi.CSVReader;
using PokemonApi.DTO;

namespace PokemonApi.DataApi.Controllers
{
    [Route("api/[controller]")]
    public class CacheController : Controller
    {

        private readonly IPokemonDataService _pokemonData;
        private readonly ICSVReader _csvReader;

        public CacheController(IPokemonDataService pokemonData, ICSVReader csvReader)
        {
            _pokemonData = pokemonData;
            _csvReader = csvReader;
        }

        // GET api/values/5
        [HttpGet("{key}")]
        public void Get(string key)
        {
//            return _pokemonData.GetItem(key).ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromQuery] string key, string value)
        {
//            _pokemonData.AddItem(key, value);
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
        
        [HttpGet]
        [Route("Headers")]
        public async Task<Dictionary<string, List<string>>> GetHeaders()
        {
            return await _pokemonData.GetAllHeadersAsync();
        }
        
        [HttpGet]
        [Route("WithEqualHeaders")]
        public async Task<List<PokemonModelDTO>> GetPokemonsWithEqualHeaders()
        {
            return await _pokemonData.GetPokemonsWithEqualHeadersAsync();
        }
    }
}