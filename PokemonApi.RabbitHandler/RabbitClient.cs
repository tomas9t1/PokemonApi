using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Pokemon.RabbitHandler
{
    public class RabbitClient : IRabbitClient
    {
        public async Task CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory();
// "guest"/"guest" by default, limited to localhost connections
                factory.UserName = "guest";
                factory.Password = "guest";
                factory.VirtualHost = "/";
                factory.HostName = "localhost";
                factory.Port = 5672;

                IConnection conn = factory.CreateConnection();
            }
            catch
            {
                return;
            }
        }
    }
}