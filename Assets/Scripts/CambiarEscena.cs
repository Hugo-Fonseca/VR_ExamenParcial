using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CargarEscena(int Nescene)
    {
        SceneManager.LoadScene(Nescene);
    }
}
