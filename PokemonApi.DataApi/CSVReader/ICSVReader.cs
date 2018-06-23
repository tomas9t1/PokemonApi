using System.Collections.Generic;
using PokemonApi.DTO;

namespace PokemonApi.DataApi.CSVReader
{
    public interface ICSVReader
    {
        List<PokemonModelDTO> ConvertCSVToObject(string filePath);
    }
}