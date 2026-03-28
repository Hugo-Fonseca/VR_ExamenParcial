using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;
    public GameObject Player;
    private GameObject lastTeleportPoint;

    private void Awake()
    {
        if(Instance != this && Instance != null) // Si ya existe una instancia y no es esta, destruye este objeto para mantener el singleton
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void DisableTeleportPoint(GameObject teleportPoint) // Desactiva el punto de teletransporte actual y reactiva el último punto de teletransporte
    {
        if(lastTeleportPoint != null)
        {
            lastTeleportPoint.SetActive(true);
        }

        teleportPoint.SetActive(false);
        lastTeleportPoint = teleportPoint;
        
        
#if UNITY_EDITOR
    Player.GetComponent<CardboardSimulator>().UpdatePlayerPositonSimulator();
#endif

    }

}
