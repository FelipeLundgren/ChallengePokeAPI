using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class MovesApi : MonoBehaviour
{
    public static IEnumerator FetchMoveDetails(string moveUrl, Action<MoveDetails> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(moveUrl))
        {
            // Espera a requisição terminar
            yield return request.SendWebRequest(); 

            if (request.result == UnityWebRequest.Result.ConnectionError || 
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Erro ao carregar detalhes do golpe ({moveUrl}): {request.error}");
                callback?.Invoke(null); 
            }
            else
            {
                string json = request.downloadHandler.text;
                
                MoveDetails moveDetails = JsonUtility.FromJson<MoveDetails>(json);
                callback?.Invoke(moveDetails);
            }
        }
    }
}





