
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class Bench : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] TextMeshPro displayedName;
    [SerializeField] SnapTo snapTo;

    private Vector3 closedPosition;
    private Vector3 openPosition { get { return new Vector3(closedPosition.x, closedPosition.y + hoverHeight, closedPosition.z); } }
    static float hoverHeight = 20f;
    static float glideTime = 0.5f;

    void Start() {
        closedPosition = transform.position;
    }

    Ray ray;
    RaycastHit hit;

    void Update() {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit)) {
            print(hit.collider.name);
        }
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
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Debug2.Log("Cursor entered bench"); // , LogLevel.All);
        if (LeanTween.isTweening(gameObject)) {
            LeanTween.cancel(gameObject);
            LeanTween.move(gameObject, openPosition, glideTime).setEaseOutCubic();
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug2.Log("Cursor exited bench"); // , LogLevel.All);
        if (LeanTween.isTweening(gameObject)) {
            LeanTween.cancel(gameObject);
            LeanTween.move(gameObject, closedPosition, glideTime).setEaseOutCubic();
        }
    }
}