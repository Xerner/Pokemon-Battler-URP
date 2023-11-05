
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class Bench : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] TextMeshPro displayedName;
    [SerializeField] SnapTo snapTo;
    [SerializeField] SpriteRenderer type1;
    [SerializeField] SpriteRenderer type2;

    Vector3 normalPosition;
    Vector3 hoveredPosition { get { return new Vector3(normalPosition.x, normalPosition.y + hoverHeight, normalPosition.z); } }
    static float hoverHeight = 20f;
    static float glideTime = 0.5f;

    void Start() {
        normalPosition = transform.RectTransform().anchoredPosition;
    }

    public PokemonBehaviour Pokemon { 
        get {
            if (snapTo.SavedObject == null) return null;
            return snapTo.SavedObject?.GetComponent<PokemonBehaviour>(); 
        } 
    }
    
    public void Reset() => SetPokemon(null);

    public void SetPokemon(PokemonBehaviour pokemon)
    {
        if (snapTo != null) snapTo.SetPokemon(pokemon.gameObject);
        if (displayedName != null) displayedName.text = pokemon == null ? "" : pokemon.name;
        type1.sprite = StaticAssets.typeMiniSprites[pokemon.Pokemon.types[0].ToString()];
        type1.color = new Color(1f, 1f, 1f, 1f);
        if (pokemon.Pokemon.types[1] == EPokemonType.None) {
            type2.sprite = null;
            type2.color = new Color(1f, 1f, 1f, 0f);
        } else {
            type2.sprite = StaticAssets.typeMiniSprites[pokemon.Pokemon.types[1].ToString()];
            type2.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug2.Log("Cursor entered bench", LogLevel.All);
        if (LeanTween.isTweening(gameObject)) LeanTween.cancel(gameObject);
        LeanTween.move((RectTransform)gameObject.transform, hoveredPosition, glideTime).setEaseOutCubic();
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug2.Log("Cursor exited bench", LogLevel.All);
        if (LeanTween.isTweening(gameObject)) LeanTween.cancel(gameObject);
        LeanTween.move((RectTransform)gameObject.transform, normalPosition, glideTime).setEaseOutCubic();
    }
}