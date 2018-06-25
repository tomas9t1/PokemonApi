using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationService.BattleModule;
using ApplicationService.PokedexModule;
using Moq;
using NUnit.Framework;
using PokemonApi.DTO;
using PokemonAPI.Tests.Mocks;


namespace PokemonAPI.Tests.Tests
{
    public class PokemonFightTests
    {
        [Test]
        public void HappyPathPokemonFightTest()
        {
            Random randomInt = new Random();
            
            var pokemonLeft = new PokemonModelDTO
            {
                Name = "Test1",
                HP = randomInt.Next(20, 999),
                Attack = randomInt.Next(20, 999),
                SpAtk = randomInt.Next(20, 999)
            };
            
            var pokemonRight = new PokemonModelDTO
            {
                Name = "Test2",
                HP = randomInt.Next(20, 999),
                Attack = randomInt.Next(20, 999),
                SpAtk = randomInt.Next(20, 999)
            };
            var pokemonList = new List<PokemonModelDTO> {pokemonLeft, pokemonRight};
            var result = TestPokemonFightWithGivenPokemons(pokemonList);

            Assert.IsNotEmpty(result.FightLogs);
        }

        private static FightResponseDTO TestPokemonFightWithGivenPokemons(List<PokemonModelDTO> pokemonList)
        {
            var pokedexService = PokedexServiceMock.PokedexService(pokemonList);
            var battleService = new BattleService(pokedexService);
            var result = battleService.StartTheFight(new BaseRequest<FightConfigurationDTO>
            {
                Data = new FightConfigurationDTO
                {
                    PokemonLeft = pokemonList[1].Name,
                    PokemonRight = pokemonList[2].Name
                }
            });
            return result;
        }
    }
}