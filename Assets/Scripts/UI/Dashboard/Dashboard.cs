using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Dashboard : MonoBehaviour {
    // Needs to be a Singleton because its a MonoBehaviour
    public static Dashboard Instance { get; private set; }

    [SerializeField] TextMeshProUGUI trainerLevel;
    [SerializeField] TextMeshProUGUI experience;
    [SerializeField] TextMeshProUGUI money;
    [SerializeField] private ShopCardUI[] shopCards;
    [HideInInspector] public Trainer[] Trainers;
    [SerializeField] bool freeShop = false;
    [SerializeField] bool freeExperience = false;
    public ShopCardUI[] ShopCards { get; private set; }
    private Pokemon[] pokemons;
    const int shopCost = 2;
    const int experienceCost = 4;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }

    public string Level { get { return trainerLevel.text; } set { trainerLevel.text = value; } }
    public string Experience { get { return experience.text; } set { experience.text = value; } }
    public string Money { get { return money.text; } set { money.text = value; } }

    private void Start() {
        Pokemon.InitializeListOfPokemon(
            new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "abra" },
            (pokemon) => { if (Pokemon.CachedPokemon.Keys.Count == 5) RefreshShop(false); }
        );
    }

    void OnGUI() {
        if (GUI.Button(new Rect(Screen.width - 205, 5, 200, 30), "Fetch All Pokemon"))
            Pokemon.InitializeAllPokemon((pokemon) => { if (Pokemon.CachedPokemon.Keys.Count == Pokemon.TotalPokemon) RefreshShop(); });
        if (GUI.Button(new Rect(Screen.width - 205, 40, 200, 30), "Fetch 5 Pokemon"))
            Pokemon.InitializeListOfPokemon(
                new List<string>() { "bulbasaur", "squirtle", "charmander", "magnemite", "geodude" },
                (pokemon) => { if (Pokemon.CachedPokemon.Keys.Count == 5) RefreshShop(false); }
            );
    }

    public void BuyExperience() {
        if (!freeExperience) {
            if (TrainerManager.ActiveTrainer.Money < experienceCost) {
                Debug2.Log("Not enough money to refresh the shop!");
                return;
            }
            if (freeExperience) TrainerManager.ActiveTrainer.Money -= experienceCost;
            money.text = TrainerManager.ActiveTrainer.Money.ToString();
        }
    }

    public void RefreshShop(bool subtractMoney = true) {
        if (!freeShop) {
            if (TrainerManager.ActiveTrainer.Money < shopCost) {
                Debug2.Log("Not enough money to refresh the shop!");
                return;
            }
            if (subtractMoney) TrainerManager.ActiveTrainer.Money -= shopCost;
            money.text = TrainerManager.ActiveTrainer.Money.ToString();
        }
        if (this.pokemons != null) PokemonPool.Instance.Refund(this.pokemons);
        Pokemon[] pokemons = PokemonPool.Instance.Withdraw5();
        for (int i = 0; i < pokemons.Length; i++)
            shopCards[i].SetPokemon(pokemons[i]);
        this.pokemons = pokemons;
    }
}
