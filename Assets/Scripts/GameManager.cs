using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int totalPokemons = 11; // cambia si tienes otro número

    public GameObject pantallaFinal; // UI final
    public GameObject escenarioFinal; // opcional (sala final)

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    public void AddScore()
    {
        score++;
        Debug.Log("Capturados: " + score);

        if (score >= totalPokemons)
        {
            CompleteGame();
        }
    }

    void CompleteGame()
    {
        SceneManager.LoadScene("EscenaFinal");
    }
}