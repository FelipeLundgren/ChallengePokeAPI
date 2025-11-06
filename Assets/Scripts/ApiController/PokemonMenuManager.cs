using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Random = UnityEngine.Random;
using System.Linq;
using System.Collections.Generic;

public class PokemonMenuManager : MonoBehaviour
{
    public PokemonManager pokemonManager;
    //pokemon Names
    
    public List<TextMeshProUGUI> pokemonsNames;
    public List<RawImage> pokemonsIcons;
    public List<TextMeshProUGUI> pokemonsHP;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPokemonsMenuInfo()
    {
        GetPokemonsNames();
    }

    public void GetPokemonsNames()
    {
        int idName = 0;
        foreach (var name in pokemonsNames)
        {
            name.text = pokemonManager.pokemonPlayerTeam[idName].name;
            idName++;
        }
    }

    public void GetPokemonsHp()
    {
        int idHp = 0;
        foreach (var hp in pokemonsHP)
        {
            hp.text = pokemonManager.pokemonPlayerTeam[idHp].hp.ToString();
            idHp++;
        }
    }
}
