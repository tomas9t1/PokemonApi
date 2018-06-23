using System.Threading.Tasks;

namespace Pokemon.RabbitHandler
{
    public interface IRabbitClient
    {
        Task CreateConnection();
    }
}