using PokemonApi.DTO;

namespace ApplicationService.BattleModule
{
    public interface IBattleService
    {
        FightResponseDTO StartTheFight(BaseRequest<FightConfigurationDTO> request);
    }
}