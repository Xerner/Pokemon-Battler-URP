using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokeBattler.Client.Services
{
    public interface IGameObjectService
    {
        public Dictionary<Guid, MonoBehaviour> Behaviours { get; }
        public Action<MovePokemonDTO> OnPokemonCreated { get; set; }
        public PokemonBehaviour Create(Guid id, Pokemon pokemon);
        public void Destroy(Guid id);
    }

    public class GameObjectService : IGameObjectService
    {
        public Action<MovePokemonDTO> OnPokemonCreated { get; set; }
        public Dictionary<Guid, MonoBehaviour> Behaviours { get; private set; } = new();

        public GameObjectService(IShopService shopService)
        {
            shopService.OnPokemonBought += OnPokemonBought;
        }

        public PokemonBehaviour Create(Guid id, Pokemon pokemon)
        {
            PokemonBehaviour pokemonGO;
            if (Behaviours.ContainsKey(id))
            {
                pokemonGO = Behaviours[id] as PokemonBehaviour;
            } 
            else
            {
                pokemonGO = PokemonBehaviour.Spawn(pokemon);
                pokemonGO.Id = id;
                Behaviours.Add(id, pokemonGO);
            }
            return pokemonGO;
        }

        public void Destroy(Guid id)
        {
            var go = Behaviours[id];
            UnityEngine.Object.Destroy(go);
            Behaviours.Remove(id);
        }

        public void OnPokemonBought(BuyPokemonDTO dto)
        {
            foreach (var pokemonId in dto.PokemonToDestroy)
            {
                Destroy(pokemonId);
            }
            var go = Create(dto.Pokemon.Id, dto.Pokemon);
            OnPokemonCreated.Invoke(dto.Move);
        }
    }
}