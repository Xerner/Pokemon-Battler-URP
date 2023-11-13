using Poke.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Poke.Unity
{
    [AddComponentMenu("Poke Battler/Bench Spot")]
    [RequireComponent(typeof(BoxCollider))]
    public class BenchBehaviour : PokeContainerBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Transform moveToSpot;
        public Transform MoveToSpot { get => moveToSpot; }
        [SerializeField] TextMeshPro displayedName;
        [SerializeField] SpriteRenderer type1;
        [SerializeField] SpriteRenderer type2;

        Vector3 normalPosition;
        Vector3 hoveredPosition { get { return new Vector3(normalPosition.x, normalPosition.y, normalPosition.z - hoverHeight); } }

        static float hoverHeight = 20f;
        static float glideTime = 0.5f;

        void Start()
        {
            normalPosition = transform.RectTransform().anchoredPosition;
        }

        public override bool SetPokemon(PokemonBehaviour pokemon)
        {
            Pokemon = pokemon;
            
            // Set type sprite
            if (displayedName != null) 
                displayedName.text = pokemon == null ? "" : pokemon.name;
            type1.sprite = StaticAssets.typeMiniSprites[pokemon.Pokemon.types[0].ToString()];
            type1.color = new Color(1f, 1f, 1f, 1f);

            // Set second type sprite
            if (pokemon.Pokemon.types[1] == EPokemonType.None)
            {
                type2.sprite = null;
                type2.color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                type2.sprite = StaticAssets.typeMiniSprites[pokemon.Pokemon.types[1].ToString()];
                type2.color = new Color(1f, 1f, 1f, 1f);
            }

            // Move pokemon
            if (pokemon != null)
            {
                pokemon.MoveTo.MoveTo(MoveToSpot, true);
                return false;
            }
            return true;
        }

        #region Events

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug2.Log("Cursor entered bench", LogLevel.All);
            if (LeanTween.isTweening(gameObject)) LeanTween.cancel(gameObject);
            LeanTween.move((RectTransform)gameObject.transform, hoveredPosition, glideTime).setEaseOutCubic();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug2.Log("Cursor exited bench", LogLevel.All);
            if (LeanTween.isTweening(gameObject)) LeanTween.cancel(gameObject);
            LeanTween.move((RectTransform)gameObject.transform, normalPosition, glideTime).setEaseOutCubic();
        }

        #endregion
    }
}
