using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVR : MonoBehaviour
{
    public string escenaInicial = "Inicio";

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(escenaInicial);
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}