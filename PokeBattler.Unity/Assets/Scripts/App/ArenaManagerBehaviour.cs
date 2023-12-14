using PokeBattler.Client.Services;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Common.Models;
using PokeBattler.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class ArenaManagerBehaviour : MonoInstaller<ArenaManagerBehaviour>
{
    IGameObjectService gameObjectService;
    public List<ArenaBehaviour> Arenas;

    public override void InstallBindings()
    {
        Container.BindInstance(this).AsSingle();
    }

    [Inject]
    public void Construct(IGameObjectService gameObjectService)
    {
        this.gameObjectService = gameObjectService;
        gameObjectService.OnPokemonCreated += OnPokemonCreated;
    }

    public ArenaBehaviour GetArena(Guid id)
    {
        var arena = Arenas.FirstOrDefault(arena => arena.Arena.OwnerId == id);
        return arena;
    }

    public void OnPokemonCreated(MovePokemonDTO dto)
    {
        var arena = Arenas.FirstOrDefault(arena => arena.Arena.OwnerId == dto.TrainerId);
        var pokeBehaviour = gameObjectService.Behaviours[dto.PokemonId] as PokemonBehaviour;
        arena.AddPokemonToContainer(dto.ContainerType, dto.PokeContainerIndex, pokeBehaviour);
    }
}
