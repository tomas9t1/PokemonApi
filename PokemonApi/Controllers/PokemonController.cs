using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationService.PokedexModule;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokemon.RabbitHandler;
using PokemonApi.DTO;

namespace PokemonApi.Controllers
{
    [Route("api/[controller]")]
    public class PokemonController : Controller
    {
        private readonly IPokedexService _pokedexService;

        public PokemonController(IPokedexService pokedexService)
        {
            _pokedexService = pokedexService;
        }

        [HttpGet]
        [Route("All")]
        public BaseResponse<List<PokemonModelDTO>> GetAll()
        {
            return ExecuteRequest(_pokedexService.GetAllPokemons());
        }

        [HttpGet]
        [Route("Legendary")]
        public BaseResponse<List<PokemonModelDTO>> GetAllLegendary()
        {
            return ExecuteRequest(_pokedexService.GetAllLegendary());
        }
        
        [HttpGet]
        [Route("ByName")]
        public BaseResponse<List<PokemonModelDTO>> GetByName([FromQuery] string name)
        {
            return ExecuteRequest(_pokedexService.GetByName(name));
        }
        
        [HttpGet]
        [Route("ByType")]
        public BaseResponse<List<PokemonModelDTO>> GetByType([FromQuery] string type)
        {
            return ExecuteRequest(_pokedexService.GetByType(type));
        }
        
        [HttpGet]
        [Route("ByMultipleTypes")]
        public BaseResponse<List<PokemonModelDTO>> GetByMultipleTypes([FromQuery] string type1, string type2)
        {
            return ExecuteRequest(_pokedexService.GetByMultipleTypes(type1, type2));

        }
        
        [HttpGet]
        [Route("Headers")]
        public BaseResponse<Dictionary<string, List<string>>> GetHeaders()
        {
            return ExecuteRequest(_pokedexService.GetHeaders());

        }
        
        [HttpGet]
        [Route("EqualHeaders")]
        public BaseResponse<List<PokemonModelDTO>> GetEqualHeaders()
        {
            return ExecuteRequest(_pokedexService.GetEqualHeaders());
        }

        #region Private methods

        private BaseResponse<T> ExecuteRequest<T>(T response)
        {
            try
            {
                return new BaseResponse<T>
                {
                    Data = response,
                    Success = true
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<T>
                {
                    Message = exception.Message,
                    Success = false
                };
            }
        }

        #endregion
    }
}