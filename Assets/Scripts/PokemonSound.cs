using System.Collections;
using UnityEngine;

public class PokemonSound : MonoBehaviour
{
    public AudioClip[] sounds; // lista de sonidos del pokemon
    public float minTime = 7f;
    public float maxTime = 15f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        
        audioSource.spatialBlend = 1f; 
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        StartCoroutine(PlayRandomSound());
    }

    IEnumerator PlayRandomSound()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            if (sounds.Length > 0)
            {
                AudioClip clip = sounds[Random.Range(0, sounds.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
    }
}