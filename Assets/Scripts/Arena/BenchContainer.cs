
using TMPro;

public class BenchContainer : PokeContainer
{
    private TextMeshProUGUI displayedName;

    public override void Reset()
    {
        displayedName.text = "";
    }

    public override void SetPokemon(PokemonBehaviour pokemon)
    {
        displayedName.text = pokemon.name;
        
    }
}