using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Poke.Core;

namespace Poke.Unity
{
    [RequireComponent(typeof(Menu))]
    public class DebugPanelManager : MonoBehaviour
    {
        RectTransform rectTransform;
        public DebugContentBehaviour DebugPrefab;
        public Dictionary<string, DebugContentBehaviour> DebugContents = new Dictionary<string, DebugContentBehaviour>();
        private Dictionary<string, GameObject> debugContentPokemonGO = new Dictionary<string, GameObject>();

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            HostBehaviour.Instance.Host.OnGameCreated += HandleGameCreated;
        }

        public void HandleGameCreated(Game game)
        {
            game.OnPokemonDataLoaded += HandlePokemonDataLoaded;
        }

        public void HandlePokemonDataLoaded(PokemonPool pool) 
        {
            var defugPrefab = Spawn("Pokemon PokemonPool");
            InitializePokemonPoolDebugContent(pool, defugPrefab);
        }

        public DebugContentBehaviour Spawn(string header, GameObject content)
        {
            DebugContentBehaviour instance = Instantiate(DebugPrefab, rectTransform);
            instance.Header.text = header;
            instance.Content = content;
            DebugContents.Add(header, instance);
            return instance;
        }

        public DebugContentBehaviour Spawn(string header)
        {
            DebugContentBehaviour instance = Instantiate(DebugPrefab, rectTransform);
            instance.Header.text = header;
            DebugContents.Add(header, instance);
            return instance;
        }

        public void UpdateSize()
        {
            var csf = rectTransform.gameObject.GetComponent<ContentSizeFitter>();
            if (csf == null)
            {
                return;
            }
            csf.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }

        public void Destroy(string header) => Destroy(DebugContents[header]);

        public void InitializePokemonPoolDebugContent(PokemonPool pokemonPool, DebugContentBehaviour debugPrefab)
        {
            //var horizLayout = DebugPrefab.Content.AddComponent<HorizontalLayoutGroup>();
            //horizLayout.childControlHeight = false;
            foreach (var key in pokemonPool.TierToPokemonCounts.Keys)
            {
                var tierGO = CreateDebugContent($"Tier {key}", debugPrefab.Content.transform, false);
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
            var debugContent = Instantiate(DebugPrefab.Content, parent);
            debugContent.transform.SetParent(parent);
            if (addToDict) 
                debugContentPokemonGO.Add(name, debugContent);

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

        private void SetPokemonDebugContent(GameObject gameObject, string name, int count)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = $"<b><color=#8888FF>{count}</b> <color=#FFFFFF>{name}";
        }
    }
}
