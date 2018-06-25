using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using ApplicationService.PokedexModule;
using Moq;
using PokemonApi.DTO;

namespace PokemonAPI.Tests.Mocks
{
    public class PokedexServiceMock
    {
        public static IPokedexService PokedexService(List<PokemonModelDTO> pokemonList)
        {
            var pokedexServiceMock = new Mock<IPokedexService>();
            foreach (var pokemon in pokemonList)
            {
                pokedexServiceMock.Setup(x => x.GetPokemonByName(pokemon.Name))
                    .Returns(pokemonList.FirstOrDefault(x => x.Name == pokemon.Name));
            }

            return pokedexServiceMock.Object;
        }
    }
}