using MongoDB.Driver;
using PokemonApi.Repository.MongoDB.Entities;

namespace PokemonApi.Repository.MongoDB
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("Pokedex");
        }

        public IMongoCollection<Pokemon> Pokemon => _database.GetCollection<Pokemon>("Pokemon");
    }
}