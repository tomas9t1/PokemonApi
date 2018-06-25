using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Pokemon.RabbitHandler;
using PokemonApi.DTO;

namespace ApplicationService.PokedexModule
{
    public class PokedexService : IPokedexService
    {
        private readonly IRabbitClient _rabbitClient;

        public PokedexService(IRabbitClient rabbitClient)
        {
            _rabbitClient = rabbitClient;
        }

        public List<PokemonModelDTO> GetAllPokemons()
        {
            var request = new BaseRequest<string>{ RequestType = RequestType.GetAllPokemons};
            var result = _rabbitClient.SendRequest(request);
            return JsonConvert.DeserializeObject<List<PokemonModelDTO>>(result.Data);
        }
        
        public List<PokemonModelDTO> GetAllLegendary()
        {
            var request = new BaseRequest<string>{ RequestType = RequestType.GetLengedaryPokemons};
            var result = _rabbitClient.SendRequest(request);
            return JsonConvert.DeserializeObject<List<PokemonModelDTO>>(result.Data);
        }
        
        public List<PokemonModelDTO> GetByName(string name)
        {
            var request = new BaseRequest<string>{ RequestType = RequestType.GetPokemonsByName, Data = name};
            var result = _rabbitClient.SendRequest(request);
            return JsonConvert.DeserializeObject<List<PokemonModelDTO>>(result.Data);
        }
        
        public List<PokemonModelDTO> GetByType(string type)
        {
            var request = new BaseRequest<string>{ RequestType = RequestType.GetPokemonsByType, Data = type};
            var result = _rabbitClient.SendRequest(request);
            return JsonConvert.DeserializeObject<List<PokemonModelDTO>>(result.Data);
        }
        
        public List<PokemonModelDTO> GetByMultipleTypes(string type1, string type2)
        {
            var request = new BaseRequest<string>{ RequestType = RequestType.GetPokemonsByMultipleTypes, Data = type1 + ',' + type2};
            var result = _rabbitClient.SendRequest(request);
            return JsonConvert.DeserializeObject<List<PokemonModelDTO>>(result.Data);
        }
        
        public Dictionary<string, List<string>> GetHeaders()
        {
            var request = new BaseRequest<string>{ RequestType = RequestType.GetAllHeaders};
            var result = _rabbitClient.SendRequest(request);
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(result.Data);
        }
        
        public List<PokemonModelDTO> GetEqualHeaders()
        {
            var request = new BaseRequest<string>{ RequestType = RequestType.GetEqualOrMoreHeaders};
            var result = _rabbitClient.SendRequest(request);
            return JsonConvert.DeserializeObject<List<PokemonModelDTO>>(result.Data);
        }

        public PokemonModelDTO GetPokemonByName(string name)
        {
            return GetByName(name).FirstOrDefault();
        }
    }
}