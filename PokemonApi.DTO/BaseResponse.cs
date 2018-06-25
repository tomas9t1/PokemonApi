namespace PokemonApi.DTO
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}