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
    public GameObject menuToClose;
    public bool player;
    public List<TextMeshProUGUI> moveNameFields;
    public List<TextMeshProUGUI> moveTypeFields;
    public List<TextMeshProUGUI> movePPFields;
    public List<TextMeshProUGUI> pokemonInfo;
    public List<Pokemon> listaPokemon;
    
    
    
    

    public void NewPokemon()
    {
        StartCoroutine(LoadApi());
   
    }
    private IEnumerator LoadApi()
    {
        for (int i = 0; i < 7; i++)
        {
            
        }

        int id = Random.Range(1, 152);
        
        yield return PokeApi.PokemonFetch(id);
        
        Pokemon pokemon = PokeApi.pokemon;
        
        pokemonName.text = pokemon.name;
        
        //Calculando e atribuindo HP---------------------------------------------
        int baseHp = 0;
        int pokemonLevel = Random.Range(50, 101);
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
            yield break;
        }

        
        float hpValue = 
            (2 * baseHp + IV_MAX + (EV_MAX / 4)) * pokemonLevel;
    
        
        int calculatedHp = 
            (int)Math.Floor(hpValue / 100.0f) + pokemonLevel + 10;
        pokemonInfo[0].text = $"Lv{pokemonLevel}";
        pokemonInfo[1].text = $"{calculatedHp}/{calculatedHp}";
        //-------------------------------------------------------------------------------------
        
        //Pegando os 4 primeiros movimentos e atribuindo---------------------------------------
        var primeirosQuatroMovimentos = pokemon.moves.Take(4).ToList();
        for (int i = 0; i < moveNameFields.Count; i++)
        {
            if (i < primeirosQuatroMovimentos.Count)
            {
                string nomeDoMovimento = primeirosQuatroMovimentos[i].move.name;
                moveNameFields[i].text = nomeDoMovimento;
            
                string urlDoGolpe = primeirosQuatroMovimentos[i].move.url;
            
                //Buscar  detalhes do golpe
                yield return StartCoroutine(FetchAndDisplayMoveDetails(urlDoGolpe, i));
                
            }
            else
            {
                moveNameFields[i].text = "-";
            }
        }
        //-------------------------------------------------------------------------------------
        //Atualizar as Sprites-----------------------------------------------------------------
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
            pokemonSprite.texture = texture;
            pokemonSprite.SetNativeSize();
        }
        //---------------------------------------------------------------------------------------
        menuToClose.SetActive(false);
    }
    
    //procurar detalhes do golpe, fazendo uma segunda consulta para pegar o Tipo de golpe e os PP-------------
    private IEnumerator FetchAndDisplayMoveDetails(string url, int slotIndex)
    {
        MoveDetails fetchedDetails = null;
        
        
        yield return MovesApi.FetchMoveDetails(url, (details) => {
            fetchedDetails = details;
        });

        
        if (fetchedDetails != null)
        {
            
            string tipoDoGolpe = fetchedDetails.type.name;
            int ppDoGolpe = fetchedDetails.pp; 
            
            
            moveTypeFields[slotIndex].text = tipoDoGolpe;
            movePPFields[slotIndex].text =  $"{ppDoGolpe}/{ppDoGolpe}"; 
            
        }
        else
        {
            moveTypeFields[slotIndex].text = "Erro/Falha";
        }
    }
    //------------------------------------------------------------------------------------------------------
    
}
