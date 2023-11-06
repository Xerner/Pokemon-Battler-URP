using Poke.Core;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Poke.Unity
{
    [RequireComponent(typeof(Menu))]
    public class DebugPanelManager : MonoBehaviour
    {
        RectTransform panel;
        public DebugContent DebugPrefab;
        public Dictionary<string, DebugContent> DebugContents = new Dictionary<string, DebugContent>();
        private Dictionary<string, GameObject> debugContentPokemonGO = new Dictionary<string, GameObject>();

        void Start()
        {
            panel = gameObject.GetComponent<RectTransform>();
            Spawn("Pokemon PokemonPool");
            InitializePokemonPoolDebugContent(HostBehaviour.Instance.Host.Game.PokemonPool);
        }

        public DebugContent Spawn(string header, GameObject content)
        {
            DebugContent instance = Instantiate(DebugPrefab, panel);
            instance.Header.text = header;
            instance.Content = content;
            DebugContents.Add(header, instance);
            return instance;
        }

        public DebugContent Spawn(string header)
        {
            DebugContent instance = Instantiate(DebugPrefab, panel);
            instance.Header.text = header;
            DebugContents.Add(header, instance);
            return instance;
        }

        public void UpdateSize()
        {
            var csf = panel.gameObject.GetComponent<ContentSizeFitter>();
            csf.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        public void Destroy(string header) => Destroy(DebugContents[header]);

        public void InitializePokemonPoolDebugContent(PokemonPool pokemonPool)
        {
            DebugPrefab.Content.AddComponent<HorizontalLayoutGroup>().childControlHeight = false;
            foreach (var key in pokemonPool.TierToPokemonCounts.Keys)
            {
                var tierGO = CreateDebugContent($"Tier {key}", DebugPrefab.Content.transform, false);
                tierGO.AddComponent<VerticalLayoutGroup>();
                var tierTextGO = CreateDebugContent($"Tier {key}", tierGO.transform, false);
                var tmesh = tierTextGO.GetComponent<TextMeshProUGUI>();
                tmesh.text = $"<b>Tier {key}</b>";
                tmesh.fontSize = 18f;
                foreach (var pokemon in pokemonPool.TierToPokemonCounts[key])
                {
                    var pokemonGO = CreateDebugContent(pokemon.Key, tierGO.transform);
                    SetPokemonDebugContent(pokemonGO, pokemon.Key, pokemon.Value);
                }
            }
            UpdateSize();
        }

        private GameObject CreateDebugContent(string name, Transform parent, bool addToDict = true)
        {
            var debugContent = new GameObject(name);
            debugContent.transform.SetParent(parent);
            if (addToDict) debugContentPokemonGO.Add(name, debugContent);
            debugContent.AddComponent<CanvasRenderer>();
            var contentSizeFitter = debugContent.AddComponent<ContentSizeFitter>();
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            var tmesh = debugContent.AddComponent<TextMeshProUGUI>();
            tmesh.richText = true;
            tmesh.fontSize = 16f;
            return debugContent;
        }

        public void UpdatePokemonDebugContent(Pokemon[] pokemons, PokemonPool pokemonPool)
        {
            foreach (var pokemon in pokemons) UpdatePokemonDebugContent(pokemon, pokemonPool);
        }

        public void UpdatePokemonDebugContent(Pokemon pokemon, PokemonPool pokemonPool)
        {
            SetPokemonDebugContent(debugContentPokemonGO[pokemon.name], pokemon.name, pokemonPool.TierToPokemonCounts[pokemon.tier][pokemon.name]);
        }

        private void SetPokemonDebugContent(UnityEngine.GameObject gameObject, string name, int count)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = $"<b><color=#8888FF>{count}</b> <color=#FFFFFF>{name}";
        }
    }
}
