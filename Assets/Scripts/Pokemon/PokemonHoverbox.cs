using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>A reused UI element that graphically shows a Pokemons basic info</summary>
public class PokemonHoverbox : MonoBehaviour, IPointerExitHandler {
    #region Variables
    // Needs to be a Singleton and not a static class because its a MonoBehaviour
    public static PokemonHoverbox Instance { get; private set; }

    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] ResourceBar hp;
    [SerializeField] ResourceBar pp;
    [SerializeField] MoveBox move1;
    [SerializeField] MoveBox move2;
    [SerializeField] Image type1;
    [SerializeField] Image type2;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI specialAttack;
    [SerializeField] TextMeshProUGUI defense;
    [SerializeField] TextMeshProUGUI specialDefense;
    [SerializeField] TextMeshProUGUI range;
    [SerializeField] TextMeshProUGUI attackSpeed;
    #endregion

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    /// <summary>Show the Hoverbox, and set its UI elements</summary>
    public void Show(PokemonBehaviour pokemon) {
        Set(pokemon);
        Vector2 newPos = RectTransformUtility.WorldToScreenPoint(Camera.main, pokemon.transform.position);
        gameObject.transform.position = new Vector2(newPos.x, newPos.y + (transform.RectTransform().rect.height / 2) + 20);
        gameObject.SetActive(true);
    }

    /// <summary>Sets all the UI elements to the Pokemons information</summary>
    public void Set(PokemonBehaviour pokemon) {
        pokemonName.text = pokemon.Pokemon.name;
        hp.TotalResource = pokemon.Stats.HP;
        hp.Set(pokemon.Stats.HP);
        pp.TotalResource = pokemon.Stats.PP;
        pp.Set(pokemon.Stats.PP);
        //move1
        //move2
        type1.sprite = StaticAssets.typeSprites[pokemon.Pokemon.types[0].ToString()];
        if (pokemon.Pokemon.types[1] != EPokemonType.None) {
            type2.gameObject.SetActive(true);
            type2.sprite = StaticAssets.typeSprites[pokemon.Pokemon.types[1].ToString()];
        } else type2.gameObject.SetActive(false);
        attack.text = pokemon.Stats.Attack.ToString();
        specialAttack.text = pokemon.Stats.SpecialAttack.ToString();
        defense.text = pokemon.Stats.Defense.ToString();
        specialDefense.text = pokemon.Stats.SpecialDefense.ToString();
        range.text = pokemon.Stats.Range.ToString();
        attackSpeed.text = pokemon.Stats.AttackSpeed.ToString();
    }

    /// <summary>For use from outside sources. i.e. PokemonBehaviour</summary>
    public void OnPointerExit() {
        Instance.gameObject.SetActive(false);
    }

    /// <summary>Turns the Hoverbox off when the cursor leaves its collider</summary>
    public void OnPointerExit(PointerEventData eventData) {
        Instance.gameObject.SetActive(false);
    }

    [Serializable]
    private class MoveBox {
        public TextMeshProUGUI Text;
        public Image Image;
    }
}
