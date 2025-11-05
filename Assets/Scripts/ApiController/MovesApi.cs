using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class MovesApi : MonoBehaviour
{
    // Método que busca os detalhes de um golpe e executa um 'callback'
    // quando a busca termina.
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
                callback?.Invoke(null); // Retorna nulo em caso de erro
            }
            else
            {
                string json = request.downloadHandler.text;
                // Usa JsonUtility para desserializar no objeto MoveDetails (que usamos antes)
                MoveDetails moveDetails = JsonUtility.FromJson<MoveDetails>(json);
                callback?.Invoke(moveDetails); // Executa a função de retorno com os dados
            }
        }
    }
}





