using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    GrabManager grabManager;
    BoxCollider boxCollider;
    Vector3 spawnerPosition;
    Quaternion spawnerRotation;

    public AudioClip soundGrab;
    public AudioClip soundPlace;

    [SerializeField] public string type = "Objeto";
    [SerializeField] public GameObject spawner;

    private AudioSource player;

    void Start()
    {
        player = GetComponent<AudioSource>();

        if (spawner != null)
        {
            spawnerPosition = spawner.transform.position;
            spawnerRotation = spawner.transform.rotation;
        }

        boxCollider = GetComponent<BoxCollider>();
        grabManager = FindObjectOfType<GrabManager>();
    }

    void Update()
    {
        // Solo verificar captura si esta pokeball está en la mano
        if (grabManager != null && grabManager.heldItem == this.gameObject)
        {
            CheckPokemonCapture();
        }
    }

    public void Grab()
    {
        if (soundGrab != null && player != null)
            player.PlayOneShot(soundGrab);

        if (grabManager.heldItem != null)
        {
            grabManager.heldItem.GetComponent<GrabObject>().Drop();
        }

        grabManager.heldItem = transform.gameObject;
        boxCollider.enabled = false;
    }

    public void Drop()
    {
        transform.position = spawnerPosition;
        transform.rotation = spawnerRotation;
        grabManager.heldItem = null;
        boxCollider.enabled = true;
    }

    public void Delete()
    {
        transform.position = spawnerPosition;
        transform.rotation = spawnerRotation;
        grabManager.heldItem = null;
        boxCollider.enabled = true;
        transform.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        transform.position = spawnerPosition;
        transform.rotation = spawnerRotation;
        boxCollider.enabled = true;
        transform.gameObject.SetActive(true);
    }

    public void Place(Vector3 position)
    {
        if (soundPlace != null && player != null)
            player.PlayOneShot(soundPlace);

        transform.position = position;
        grabManager.heldItem = null;
        boxCollider.enabled = true;
    }

    public void OnPointerClickXR()
    {
        Grab();
    }

    void CheckPokemonCapture()
    {
        float radius = 0.6f; 

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in hits)
        {
            if (col.CompareTag("Pokemon"))
            {
                Debug.Log("Pokemon capturado");

                col.gameObject.SetActive(false); // pokemon desaparece
                Destroy(this.gameObject);

                if (GameManager.Instance != null)
                    GameManager.Instance.AddScore();

                break;
            }
        }
    }
}