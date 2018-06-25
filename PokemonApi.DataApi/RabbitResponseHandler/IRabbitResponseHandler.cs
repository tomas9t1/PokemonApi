using System.Threading.Tasks;

namespace PokemonApi.DataApi.RabbitResponseHandler
{
    public interface IRabbitResponseHandler
    {
        Task InitiateListener();
    }
}