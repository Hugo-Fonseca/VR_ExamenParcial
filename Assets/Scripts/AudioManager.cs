using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;

    public AudioClip musicaInicio;
    public AudioClip musicaJuego;
    public AudioClip musicaFinal;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CambiarMusica(scene.name);
    }

    void CambiarMusica(string nombreEscena)
    {
        if (nombreEscena == "Inicio")
        {
            PlayMusic(musicaInicio);
        }
        else if (nombreEscena == "Mundo1" || nombreEscena == "Mundo2")
        {
            PlayMusic(musicaJuego);
        }
        else if (nombreEscena == "EscenaFinal")
        {
            PlayMusic(musicaFinal);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true; 
        musicSource.Play();
    }
}
