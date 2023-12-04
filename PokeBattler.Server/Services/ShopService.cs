using Microsoft.Extensions.Logging;
using PokeBattler.Common.Models;
using PokeBattler.Common.Models.Interfaces;

namespace PokeBattler.Server.Services;

public interface IShopService
{
    public const bool FreePokemon = false;
    public int GetCost(Pokemon pokemon);
    public void BuyPokemon(Trainer trainer, Pokemon pokemon);
}
public class ShopService(ILogger<ShopService> logger, ITrainerService trainersService) : IShopService
{

    public int GetCost(Pokemon pokemon)
    {
        return pokemon.tier;
    }

    public void BuyPokemon(Trainer trainer, Pokemon pokemon)
    {
        var cost = GetCost(pokemon);
        if (trainer.Money < cost && !IShopService.FreePokemon)
        {
            logger.LogInformation("Not enough money", LogLevel.Detailed);
            return;
        }
        IPokeContainer bench = arenaService.GetAvailableBench();
        // Trainer bought a Pokemon, but the bench is full, BUT the purchased Pokemon can be used in an evolution to free up space
        if (!BenchHasValidContainer(bench))
            return;
        //else
        //container = TrainerManager.ActiveTrainer.ActivePokemon[pokemon.name][0].MoveTo;
        trainer.Money -= cost;
        //dashboard.Money = trainer.Money.ToString();
        logger.LogInformation($"bought a {pokemon.name} for {pokemon.tier}", LogLevel.Detailed);
        //var pokemonBehaviour = await arenaService.AddPokemonToBench(pokemon);
        if (pokemonBehaviour != null)
        {
            Reset();
        }

        bool BenchHasValidContainer(IPokeContainer bench, Trainer trainer)
        {
            return bench != null || (bench == null && trainersService.GetTrainersPokemon(trainer.Id).IsAboutToEvolve(pokemon));
        }
    }
}
