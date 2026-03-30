using System.Collections;
using UnityEngine;

public class PokeballSpawner : MonoBehaviour
{
    public GameObject pokeballPrefab;
    public float respawnTime = 5f;

    private GameObject currentPokeball;

    void Start()
    {
        SpawnPokeball();
    }

    void Update()
    {
        if (currentPokeball == null)
        {
            StartCoroutine(Respawn());
        }
    }

    void SpawnPokeball()
    {
        currentPokeball = Instantiate(pokeballPrefab, transform.position, transform.rotation);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnPokeball();
    }
}