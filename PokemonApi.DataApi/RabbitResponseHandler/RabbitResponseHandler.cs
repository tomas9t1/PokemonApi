using System;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft.Json;
using PokemonApi.DataApi.CacheHandler;
using PokemonApi.DTO;

namespace PokemonApi.DataApi.RabbitResponseHandler
{
    public class RabbitResponseHandler : IRabbitResponseHandler
    {
        private readonly IBus _bus;
        private readonly IPokemonDataService _pokemonDataService;

        public RabbitResponseHandler(IPokemonDataService pokemonDataService)
        {
            _pokemonDataService = pokemonDataService;
            _bus = RabbitHutch.CreateBus("host=localhost");
        }

        public Task InitiateListener()
        {
            _bus.RespondAsync<BaseRequest<string>, BaseResponse<string>>(request =>
                Task.Factory.StartNew(() => BaseResponse(request)));
            return Task.CompletedTask;
        }

        private BaseResponse<string> BaseResponse(BaseRequest<string> request)
        {
            var response = string.Empty;
            switch (request.RequestType)
            {
                case RequestType.GetAllPokemons:

                    response = JsonConvert.SerializeObject(_pokemonDataService.GetAllPokemonsAsync().GetAwaiter()
                        .GetResult());
                    break;
                case RequestType.GetPokemonsByName:
                    response = JsonConvert.SerializeObject(_pokemonDataService.SearchPokemonByNameAsync(request.Data)
                        .GetAwaiter().GetResult());
                    break;
                case RequestType.GetLengedaryPokemons:
                    response = JsonConvert.SerializeObject(_pokemonDataService.GetLegendaryPokemonsAsync().GetAwaiter()
                        .GetResult());
                    break;
                case RequestType.GetPokemonsByType:
                    response = JsonConvert.SerializeObject(_pokemonDataService.GetPokemonsByTypeAsync(request.Data)
                        .GetAwaiter().GetResult());
                    break;
                case RequestType.GetPokemonsByMultipleTypes:
                    var typesList = request.Data.Split(',').ToList();
                    response = JsonConvert.SerializeObject(_pokemonDataService
                        .GetPokemonsByMultipleTypesAsync(typesList).GetAwaiter().GetResult());
                    break;
                case RequestType.GetAllHeaders:
                    response = JsonConvert.SerializeObject(_pokemonDataService.GetAllHeadersAsync().GetAwaiter()
                        .GetResult());
                    break;
                case RequestType.GetEqualOrMoreHeaders:
                    response = JsonConvert.SerializeObject(_pokemonDataService.GetPokemonsWithEqualHeadersAsync()
                        .GetAwaiter().GetResult());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new BaseResponse<string> {Data = response};
        }
    }
}