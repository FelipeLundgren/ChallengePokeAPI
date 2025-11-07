using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using Random = UnityEngine.Random;
using System.Linq;
using System.Collections.Generic;

public class PokemonToShow : MonoBehaviour
{
    public RawImage pokemonSprite;
    public TextMeshProUGUI pokemonName;
    public bool player;
    public List<TextMeshProUGUI> moveNameFields;
    public List<TextMeshProUGUI> moveTypeFields;
    public List<TextMeshProUGUI> movePPFields;
    public List<TextMeshProUGUI> pokemonInfo;
    public List<Pokemon> listaPokemon;
    public Pokemon activePokemon;
    public PokemonManager manager;
    private int activeIndex = 0;



    public IEnumerator pokemonLoader(List<Pokemon> pokemons)
    {
        listaPokemon = pokemons;
        yield return LoadPokemons();
    
    }

    private IEnumerator LoadPokemons()
    {
        
        foreach (var pokemon in listaPokemon)
        {
            pokemonName.text = pokemon.name;

            GetPokemonHp(pokemon);

            yield return StartCoroutine(GetPokemonMoves(pokemon));
            
            yield return StartCoroutine(GetSprite(pokemon));
        }
        ActivePokemon(0);


    }
    
    public void ActivePokemon(int index)
    {
        activePokemon = listaPokemon[index];
        SetInterface();
        Debug.Log(listaPokemon.Count);


    }
    public void NewActivePokemon(int id)
    {
        
        ActivePokemon(id);


    }
  



    private void SetInterface()
    {
            
            pokemonName.text = activePokemon.name;
            pokemonInfo[0].text = $"Lv{activePokemon.pokemonLevel}";
            pokemonInfo[1].text = $"{activePokemon.hp}/{activePokemon.hp}"; 
            pokemonSprite.texture = activePokemon.sprite;
            pokemonSprite.SetNativeSize();
        if (!player)
            return;
        for (int i = 0; i < 4; i++)
            {
                if (i < activePokemon.movesData.Count)
                {
                    moveNameFields[i].text = activePokemon.movesData[i].name;
                    moveTypeFields[i].text = activePokemon.movesData[i].type.name;
                    movePPFields[i].text = $"{activePokemon.movesData[i].pp}/{activePokemon.movesData[i].pp}";
                }
                else
                {
                    moveNameFields[i].text = "-";
                    moveTypeFields[i].text = "-";
                    movePPFields[i].text = "-";
                }
        }
    }
    private void GetPokemonHp(Pokemon pokemon)
    {
        int baseHp = 0;
        const int IV_MAX = 31;
        const int EV_MAX = 252;
        PokemonStatWrapper hpStat = pokemon.stats.FirstOrDefault(s => s.stat.name == "hp");

        if (hpStat != null)
        {
            baseHp = hpStat.base_stat;
        }
        else
        {
            Debug.LogError("Base Stat de HP não encontrado.");
        }


        float hpValue =
            (2 * baseHp + IV_MAX + (EV_MAX / 4)) * pokemon.pokemonLevel;


        int calculatedHp =
            (int)Math.Floor(hpValue / 100.0f) + pokemon.pokemonLevel + 10;
        pokemon.hp = calculatedHp;
        // pokemon.pokemonLevel = pokemon.pokemonLevel;

        pokemonInfo[0].text = $"Lv{pokemon.pokemonLevel}";
        pokemonInfo[1].text = $"{calculatedHp}/{calculatedHp}";
    }

    private IEnumerator GetPokemonMoves(Pokemon pokemon)
    {
        var primeirosQuatroMovimentos = pokemon.moves.Take(4).ToList();
        for (int i = 0; i <primeirosQuatroMovimentos.Count; i++)
        {
            {
                string nomeDoMovimento = primeirosQuatroMovimentos[i].move.name;
                

                string urlDoGolpe = primeirosQuatroMovimentos[i].move.url;

                //Buscar  detalhes do golpe
                yield return MovesApi.FetchMoveDetails(urlDoGolpe, (details) => {
                    pokemon.movesData.Add(details);
                });
            }
        }
    }

    private IEnumerator GetSprite(Pokemon pokemon )
    {
        string pokemonposition = String.Empty;
        if (player)
        {
            pokemonposition = pokemon.sprites.back_default;
        }
        else
        {
            pokemonposition = pokemon.sprites.front_default;
        }
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(pokemonposition);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao carregar sprite: " + request.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            texture.filterMode = FilterMode.Point;
            pokemon.sprite = texture;
            
        }
        
    }

    
    

    
}
