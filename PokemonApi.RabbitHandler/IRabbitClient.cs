using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PokemonApi.DTO;

namespace Pokemon.RabbitHandler
{
    public interface IRabbitClient
    {
        BaseResponse<string> SendRequest(BaseRequest<string> request);
    }
}