using System;
using System.Collections.Generic;
using ApplicationService.PokedexModule;
using PokemonApi.DTO;

namespace ApplicationService.BattleModule
{
    public class BattleService : IBattleService
    {
        private readonly IPokedexService _pokedexService;

        public BattleService(IPokedexService pokedexService)
        {
            _pokedexService = pokedexService;
        }

        public FightResponseDTO StartTheFight(BaseRequest<FightConfigurationDTO> request)
        {
            var pokemonLeft = _pokedexService.GetPokemonByName(request.Data.PokemonLeft);
            var pokemonRight = _pokedexService.GetPokemonByName(request.Data.PokemonRight);

            if(pokemonLeft == null || pokemonRight == null)
                throw new Exception("One or more pokemons are missing");
            
            var result = new FightResponseDTO
            {
                PokemonLeft = pokemonLeft,
                PokemonRight = pokemonRight,
                FightLogs = new List<FightLog>()
            };

            var fight = true;

            while (fight)
            {
                var round = new List<string>();

                if (pokemonLeft.Speed > pokemonRight.Speed)
                {
                    Fight(round, pokemonLeft, pokemonRight);
                }

                else if (pokemonLeft.Speed < pokemonRight.Speed)
                {
                    Fight(round, pokemonRight, pokemonLeft);
                }

                else if (pokemonLeft.SpAtk > pokemonRight.SpAtk)
                {
                    Fight(round, pokemonLeft, pokemonRight);
                }

                else if (pokemonLeft.SpAtk < pokemonRight.SpAtk)
                {
                    Fight(round, pokemonRight, pokemonLeft);
                }

                fight = CheckIfFightContinues(pokemonLeft, pokemonRight, round);
                result.FightLogs.Add(new FightLog {OutComes = round});
            }

            return result;
        }

        #region Private methods

        
        private bool CheckIfFightContinues(PokemonModelDTO pokemonLeft, PokemonModelDTO pokemonRight,
            List<string> round)
        {
            if (pokemonLeft.HP <= 0)
            {
                round.Add("Winner is: " + pokemonRight.Name);
                return false;
            }

            if (pokemonRight.HP <= 0)
            {
                round.Add("Winner is: " + pokemonLeft.Name);
                return false;
            }

            return true;
        }

        private void Fight(List<string> round, PokemonModelDTO fasterPokemon, PokemonModelDTO slowerPokemon)
        {
            round.Add(GenerateStartText(fasterPokemon, slowerPokemon));
            round.Add(GenerateFightText(fasterPokemon, slowerPokemon));
            slowerPokemon.HP = slowerPokemon.HP - fasterPokemon.Attack + slowerPokemon.Defense / 10;
            if (slowerPokemon.HP <= 0)
                return;

            round.Add(GenerateFightText(slowerPokemon, fasterPokemon));
            fasterPokemon.HP = fasterPokemon.HP - slowerPokemon.Attack + fasterPokemon.Defense / 10;
        }

        private string GenerateStartText(PokemonModelDTO fasterPokemon, PokemonModelDTO slowerPokemon)
        {
            return fasterPokemon.Name + "starts since " + fasterPokemon.Speed + " > " + slowerPokemon.Speed;
        }

        private string GenerateFightText(PokemonModelDTO attacker, PokemonModelDTO attacked)
        {
            return attacked.Name + " health updated: " + attacked.HP + " - " + attacker.Attack + " = " +
                   (attacked.HP + attacked.Defense / 10 - attacker.Attack) + " absorbed(" + attacked.Defense / 10 + ")";
        }

        #endregion
    }
}