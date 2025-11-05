using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class PokeApi
{
    public static Pokemon pokemon;
    public static IEnumerator PokemonFetch(int id)
    {
        
        UnityWebRequest request = UnityWebRequest.Get($"https://pokeapi.co/api/v2/pokemon/{id}");
        yield return request.SendWebRequest(); 

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao carregar sprite: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            pokemon=JsonUtility.FromJson<Pokemon>(json);
            
        }
    }
  
}
    
