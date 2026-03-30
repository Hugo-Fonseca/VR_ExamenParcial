using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonCapture : MonoBehaviour
{
    GrabManager grabManager;

    void Start()
    {
        grabManager = FindObjectOfType<GrabManager>();
    }

    public void OnPointerClickXR()
    {
        Debug.Log("ME ESTÁN MIRANDO Y ACTIVANDO");
        if (grabManager.heldItem != null)
        {
            GrabObject grabObj = grabManager.heldItem.GetComponent<GrabObject>();

            if (grabObj != null && grabObj.type == "Objeto")
            {
                Debug.Log("Pokemon capturado");

                // eliminar pokeball
                Destroy(grabManager.heldItem);
                grabManager.heldItem = null;

                // eliminar pokemon
                gameObject.SetActive(false);

                // sumar punto
                if (GameManager.Instance != null)
                    GameManager.Instance.AddScore();
            }
        }
    }
}