using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft.Json;
using Pokemon.RabbitHandler;
using PokemonApi.DTO;

namespace PokemonApi.RabbitHandler
{
    public class RabbitClient : IRabbitClient
    {
        private readonly IBus bus;
        public RabbitClient()
        {
            bus = RabbitHutch.CreateBus("host=localhost");
        }
        
        public BaseResponse<string> SendRequest(BaseRequest<string> request)
        {
            try
            {
                return bus.Request<BaseRequest<string>, BaseResponse<string>>(request);
            }
            catch
            {
                return new BaseResponse<string>();
            }
        }
    }

    public class MyMessage
    {
        public string Text { get; set; }
    }
}