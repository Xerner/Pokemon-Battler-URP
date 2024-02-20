using PokeBattler.Common.Models;
using PokeBattler.Common.Models.DTOs;
using PokeBattler.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PokeBattler.Client.Services
{
    /// <summary>
    /// Service for managing game objects that should exist between clients
    /// </summary>
    public interface ITrackedObjectService
    {
        /// <summary>
        /// Contains all the tracked behaviours that should exist on all clients
        /// </summary>
        public Dictionary<Guid, MonoBehaviour> TrackedBehaviours { get; }
        public Action<MovePokemonDTO> OnPokemonCreated { get; set; }
        public PokemonBehaviour Create(Guid id, Pokemon pokemon);
        public void Destroy(Guid id);
    }

    public class TrackedObjectService : ITrackedObjectService
    {
        public Action<MovePokemonDTO> OnPokemonCreated { get; set; }
        public Dictionary<Guid, MonoBehaviour> TrackedBehaviours { get; private set; } = new();

        public TrackedObjectService(IShopService shopService)
        {
            shopService.OnPokemonBought += OnPokemonBought;
        }

        public PokemonBehaviour Create(Guid id, Pokemon pokemon)
        {
            PokemonBehaviour pokemonGO;
            if (TrackedBehaviours.ContainsKey(id))
            {
                pokemonGO = TrackedBehaviours[id] as PokemonBehaviour;
            } 
            else
            {
                pokemonGO = PokemonBehaviour.Spawn(pokemon);
                pokemonGO.Id = id;
                TrackedBehaviours.Add(id, pokemonGO);
            }
            return pokemonGO;
        }

        public void Destroy(Guid id)
        {
            var go = TrackedBehaviours[id];
            UnityEngine.Object.Destroy(go);
            TrackedBehaviours.Remove(id);
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