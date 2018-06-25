using MongoDB.Driver;
using PokemonApi.Repository.MongoDB.Entities;

namespace PokemonApi.Repository.MongoDB
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext()
        {
            var client = new MongoClient("mongodb://172.18.0.1:27017");
            _database = client.GetDatabase("Pokedex");
        }

        public IMongoCollection<Pokemon> Pokemon => _database.GetCollection<Pokemon>("Pokemon");
    }
}