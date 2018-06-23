using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PokemonApi.DTO;

namespace PokemonApi.DataApi.CSVReader
{
    public class CSVReader : ICSVReader
    {
        public List<PokemonModelDTO> ConvertCSVToObject(string filePath)
        {
            return File.ReadAllLines(filePath)
                .Skip(1)
                .Select(x => x.Split(','))
                .Select(x => new PokemonModelDTO
                {
                    Name = x[1],
                    Type1 = x[2],
                    Type2 = x[3],
                    Total = int.Parse(x[4]),
                    HP = int.Parse(x[5]),
                    Attack = int.Parse(x[6]),
                    Defense = int.Parse(x[7]),
                    SpAtk = int.Parse(x[8]),
                    SpDef = int.Parse(x[9]),
                    Speed = int.Parse(x[10]),
                    Generation = int.Parse(x[11]),
                    Legendary = Boolean.Parse(x[12])
                }).ToList();
        }
    }
}