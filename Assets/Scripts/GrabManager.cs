using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    public GameObject[] interactables;
    public List<GameObject> Objetos;

    public GameObject heldItem;
    void Start()
    {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject item in interactables)
        {
            if (item.GetComponent<GrabObject>())
            {
                Objetos.Add(item);
            }
        }
    }

    void FixedUpdate()
    {
        if (heldItem != null)
        {
            GameObject pointer = GameObject.Find("GazePointer");
            Vector3 pointerPos = pointer.transform.position;
            heldItem.transform.position = new Vector3(pointerPos.x, pointerPos.y + 0.5f, pointerPos.z);
        }
    }
}
