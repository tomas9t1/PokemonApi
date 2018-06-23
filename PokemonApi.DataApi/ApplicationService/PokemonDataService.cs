using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PokemonApi.DTO;
using PokemonApi.Repository.MongoDB;
using PokemonApi.Repository.MongoDB.Entities;

namespace PokemonApi.DataApi.CacheHandler
{
    public class PokemonDataService : IPokemonDataService
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonDataService(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }

        public async Task<List<PokemonModelDTO>> GetAllPokemonsAsync()
        {
            var pokemonList = await _pokemonRepository.GetAllAsync();
            return pokemonList.Select(x => _mapper.Map<PokemonModelDTO>(x)).ToList();
        }

        public async Task FillPokedexAsync(List<PokemonModelDTO> pokemonList)
        {
            var pokemons = await _pokemonRepository.GetAllAsync();
            if (pokemons.Any())
                await _pokemonRepository.WipeDatabaseAsync();
            pokemons = pokemonList.Select(x => _mapper.Map<Pokemon>(x)).ToList();
            await _pokemonRepository.InsertManyAsync(pokemons);
        }

        public async Task<List<PokemonModelDTO>> SearchPokemonByNameAsync(string searchText)
        {
            var pokemonList = await _pokemonRepository.GetAllWithNameSearchStringAsync(searchText);
            return pokemonList.Select(x => _mapper.Map<PokemonModelDTO>(x)).ToList();
        }

        public async Task<List<PokemonModelDTO>> GetLegendaryPokemonsAsync()
        {
            var pokemonList = await _pokemonRepository.GetAllLengedaryAsync();
            return pokemonList.Select(x => _mapper.Map<PokemonModelDTO>(x)).ToList();
        }

        public async Task<List<PokemonModelDTO>> GetPokemonsByTypeAsync(string type)
        {
            var pokemonList = await _pokemonRepository.GetAllByTypeAsync(type);
            return pokemonList.Select(x => _mapper.Map<PokemonModelDTO>(x)).ToList();
        }

        public async Task<List<PokemonModelDTO>> GetPokemonsByMulyipleTypesAsync(List<string> types)
        {
            var pokemonList = await _pokemonRepository.GetAllByMultipleTypesAsync(types);
            return pokemonList.Select(x => _mapper.Map<PokemonModelDTO>(x)).ToList();
        }

        public async Task<Dictionary<string, List<string>>> GetAllHeadersAsync()
        {
            var headersDictionary = new Dictionary<string, List<string>>();

            var pokemonList = await _pokemonRepository.GetAllAsync();
            if (pokemonList.Any())
            {
                headersDictionary.Add("Name",
                    pokemonList.OrderBy(x => x.Name).GroupBy(x => x.Name).Select(x => x.FirstOrDefault()?.Name)
                        .ToList());
                headersDictionary.Add("Type1",
                    pokemonList.OrderBy(x => x.Type1).GroupBy(x => x.Type1).Select(x => x.FirstOrDefault()?.Type1)
                        .ToList());
                headersDictionary.Add("Type2",
                    pokemonList.OrderBy(x => x.Type2).GroupBy(x => x.Type2)
                        .Select(x => x.FirstOrDefault()?.Type2 ?? "None").ToList());
                headersDictionary.Add("Total",
                    pokemonList.OrderBy(x => x.Total).GroupBy(x => x.Total)
                        .Select(x => x.FirstOrDefault()?.Total.ToString()).ToList());
                headersDictionary.Add("HP",
                    pokemonList.OrderBy(x => x.HP).GroupBy(x => x.HP).Select(x => x.FirstOrDefault()?.HP.ToString())
                        .ToList());
                headersDictionary.Add("Attack",
                    pokemonList.OrderBy(x => x.Attack).GroupBy(x => x.Attack)
                        .Select(x => x.FirstOrDefault()?.Attack.ToString()).ToList());
                headersDictionary.Add("Defense",
                    pokemonList.OrderBy(x => x.Defense).GroupBy(x => x.Defense)
                        .Select(x => x.FirstOrDefault()?.Defense.ToString()).ToList());
                headersDictionary.Add("SpAtk",
                    pokemonList.OrderBy(x => x.SpAtk).GroupBy(x => x.SpAtk)
                        .Select(x => x.FirstOrDefault()?.SpAtk.ToString()).ToList());
                headersDictionary.Add("SpDef",
                    pokemonList.OrderBy(x => x.SpDef).GroupBy(x => x.SpDef)
                        .Select(x => x.FirstOrDefault()?.SpDef.ToString()).ToList());
                headersDictionary.Add("Speed",
                    pokemonList.OrderBy(x => x.Speed).GroupBy(x => x.Speed)
                        .Select(x => x.FirstOrDefault()?.Speed.ToString()).ToList());
                headersDictionary.Add("Generation",
                    pokemonList.OrderBy(x => x.Generation).GroupBy(x => x.Generation)
                        .Select(x => x.FirstOrDefault()?.Generation.ToString()).ToList());
                headersDictionary.Add("Legendary",
                    pokemonList.OrderBy(x => x.Legendary).GroupBy(x => x.Legendary)
                        .Select(x => x.FirstOrDefault()?.Legendary.ToString()).ToList());
            }
            return headersDictionary;
        }

        public async Task<List<PokemonModelDTO>> GetPokemonsWithEqualHeadersAsync()
        {
            var pokemonList = await _pokemonRepository.GetAllAsync();
            var resultList = new List<Pokemon>();
            if (pokemonList.Any())
            {
                Parallel.ForEach(pokemonList, pokemon =>
                {
                    var checkResult = ArePropertiesOfObjectEqualOrMore(pokemon);

                    if (checkResult)
                        resultList.Add(pokemon);
                }); 
            }
            return pokemonList.Select(x => _mapper.Map<PokemonModelDTO>(x)).ToList();
        }

        private bool ArePropertiesOfObjectEqualOrMore(Pokemon pokemon)
        {
            var objectType = pokemon.GetType();
            var properties = objectType.GetProperties();
            var stringPropertiesList = new List<string>();
            var intPropertiesList = new List<int>();
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(pokemon, null);

                TypeCode typeCode = Type.GetTypeCode(value.GetType());

                switch (typeCode)
                {
                    case TypeCode.Int32:
                        if (intPropertiesList.Any())
                        {
                            foreach (var intProperty in intPropertiesList)
                            {
                                var parsedValue = (int) value;
                                if (intProperty == parsedValue || intProperty > parsedValue)
                                    return true;
                                intPropertiesList.Add(parsedValue);
                            }
                        }
                        break;

                    case TypeCode.String:
                        if (stringPropertiesList.Contains(value))
                            return true;
                        else
                        {
                            stringPropertiesList.Add(value as string);
                        }
                        break;

                    default:
                        return false;
                }
            }
            return false;
        }
    }
}