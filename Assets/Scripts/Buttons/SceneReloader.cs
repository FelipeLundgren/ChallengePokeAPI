using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    Pokemon pokemon;

    public void RecarregarCenaAtual()
    {
        Scene cenaAtual = SceneManager.GetActiveScene();


        SceneManager.LoadScene(cenaAtual.buildIndex);
    }
}