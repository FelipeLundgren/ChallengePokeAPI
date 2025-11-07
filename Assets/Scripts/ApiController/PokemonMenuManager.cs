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
    public List<TextMeshProUGUI> pokemonsNames;
    public List<RawImage> pokemonsIcons;
    public List<TextMeshProUGUI> pokemonsHP;
    public List<TextMeshProUGUI> pokemonsLevel;


    public void GetPokemonsMenuInfo()
    {
        GetPokemonsNames();
        GetPokemonsHp();
        GetPokemonslevel();
        StartCoroutine(LoadIconsSequence());
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
            hp.text = $"{pokemonManager.pokemonPlayerTeam[idHp].hp}/{pokemonManager.pokemonPlayerTeam[idHp].hp}";
            idHp++;
        }
    }

    public void GetPokemonslevel()
    {
        int idLevel = 0;
        foreach (var level in pokemonsLevel)
        {
            level.text = $"Lv{pokemonManager.pokemonPlayerTeam[idLevel].pokemonLevel}";
            idLevel++;
        }
    }

    //IA-------------------------------------------
    private IEnumerator LoadIconsSequence()
    {
        List<Pokemon> team = pokemonManager.pokemonPlayerTeam;

        if (team.Count == 0)
        {
            Debug.LogError("O time do jogador está vazio. Ícones não podem ser carregados.");
            yield break;
        }

        int limit = Mathf.Min(pokemonsIcons.Count, team.Count);

        for (int i = 0; i < limit; i++)
        {
            Pokemon pokemon = team[i];

            // 1. CORREÇÃO: Declare 'targetSlot' UMA ÚNICA VEZ
            RawImage targetSlot = pokemonsIcons[i];

            // --- Lógica para forçar o sprite frontal (limpa o cache se já existia) ---
            // Se o sprite já está salvo, limpamos ele para garantir que o frontal seja baixado/exibido
            pokemon.sprite = null;

            // --- Início da Busca Assíncrona ---
            string iconUrl = pokemon.sprites.front_default; // URL do sprite frontal

            // Chama a Coroutine para buscar e ESPERA o download
            yield return StartCoroutine(FetchIconAndAssign(iconUrl, pokemon, targetSlot));
        }
    }

    private IEnumerator FetchIconAndAssign(string url, Pokemon pokemonToSave, RawImage targetImage)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest(); // PAUSA AQUI! Espera o download da imagem

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                texture.filterMode = FilterMode.Point;

                // 1. SALVA NA CLASSE POKÉMON: A imagem é salva no campo 'pokemon.sprite'
                pokemonToSave.sprite = texture;

                // 2. ATRIBUI À UI: Exibe a imagem no slot
                targetImage.texture = texture;
                targetImage.SetNativeSize();
            }
            else
            {
                Debug.LogError($"Erro ao carregar ícone de {url}: {request.error}");
                // Opcional: Atribuir um sprite de erro aqui
            }
        }
    }
    //-------------------------------------------
}