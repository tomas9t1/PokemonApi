using System;
using System.Collections.Generic;
using ApplicationService.BattleModule;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokemon.RabbitHandler;
using PokemonApi.DTO;

namespace PokemonApi.Controllers
{
    public class BattleController : Controller
    {
        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [HttpPost]
        [Route("Fight")]
        public BaseResponse<FightResponseDTO> GetAll([FromBody] BaseRequest<FightConfigurationDTO> request)
        {
            return ExecuteRequest(_battleService.StartTheFight(request));
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