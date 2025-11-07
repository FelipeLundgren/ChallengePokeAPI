using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PokeSummary : MonoBehaviour
{
    public PokemonToShow pokemonToShow;
    public TextMeshProUGUI pokeNameText;
    public TextMeshProUGUI pokeLevel;
    public RawImage pokeImage;
    public int index;
    



    
    private void Summary(int index)
    {
        pokeNameText.text = pokemonToShow.listaPokemon[index].name;
        pokeLevel.text = pokemonToShow.listaPokemon[index].pokemonLevel.ToString();
        pokeImage.texture = pokemonToShow.listaPokemon[index].sprite;
    }

    public void SummaryOnClick()
    {
        index = pokemonToShow.activeIndex;
        Summary(index);
    }
    
    
    
}
