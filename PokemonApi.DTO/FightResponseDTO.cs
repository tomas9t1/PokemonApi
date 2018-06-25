using System.Collections.Generic;

namespace PokemonApi.DTO
{
    public class FightResponseDTO
    {
        public PokemonModelDTO PokemonLeft { get; set; }
        public PokemonModelDTO PokemonRight { get; set; }
        public List<FightLog> FightLogs { get; set; }
    }

    public class FightLog
    {
        public List<string> OutComes { get; set; }
    }
}