using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PokemonManager : MonoBehaviour
{
    public GameObject menuToClose;

    [SerializeField] private PokemonToShow pokemonPlayer;
    [SerializeField] private PokemonToShow pokemonEnemy;
    public List<Pokemon> pokemonPlayerTeam = new List<Pokemon>();
    public List<Pokemon> pokemonEnemyTeam = new List<Pokemon>();
    
    public GameObject StartButton;


    public IEnumerator FetchPokemon()
    {
        

        for (int i = 1; i <= 12; i++)
        {
            int id = Random.Range(1, 152);
            yield return PokeApi.PokemonFetch(id);
            Pokemon pokemon = PokeApi.pokemon;
            if (i % 2 != 0)
            {
                pokemonPlayerTeam.Add(pokemon);
            }
            else
            {
                pokemonEnemyTeam.Add(pokemon);
            }
           
        }
        yield return this.pokemonPlayer.pokemonLoader(pokemonPlayerTeam);
        yield return this.pokemonEnemy.pokemonLoader(pokemonEnemyTeam);
    }

    public IEnumerator StartGame()
    { 
        StartButton.SetActive(false);
        yield return FetchPokemon();
        menuToClose.SetActive(false);
    }
    
    public void OnClickStart()
    {
        StartCoroutine(StartGame());
        
    }
    
    

    

}
