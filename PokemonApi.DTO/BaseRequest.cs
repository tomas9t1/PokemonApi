namespace PokemonApi.DTO
{
    public class BaseRequest<T>
    {
        public T Data { get; set; }   
        public RequestType RequestType { get; set; }
    }
    
    public enum RequestType
    {
        GetAllPokemons,
        GetPokemonsByName,
        GetLengedaryPokemons,
        GetPokemonsByType,
        GetPokemonsByMultipleTypes,
        GetAllHeaders,
        GetEqualOrMoreHeaders
    }
}