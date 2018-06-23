using System.Collections.Generic;
using AutoMapper;
using PokemonApi.DTO;
using PokemonApi.Repository.MongoDB.Entities;

namespace PokemonApi.DataApi
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonModelDTO>().ReverseMap();
            CreateMap<List<Pokemon>, List<PokemonModelDTO>>();
            CreateMap<List<PokemonModelDTO>, List<Pokemon>>();
        }
    }
}