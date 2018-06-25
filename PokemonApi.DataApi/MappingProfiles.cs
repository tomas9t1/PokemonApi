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
            CreateMap<Repository.MongoDB.Entities.Pokemon, PokemonModelDTO>().ReverseMap();
            CreateMap<List<Repository.MongoDB.Entities.Pokemon>, List<PokemonModelDTO>>();
            CreateMap<List<PokemonModelDTO>, List<Repository.MongoDB.Entities.Pokemon>>();
        }
    }
}