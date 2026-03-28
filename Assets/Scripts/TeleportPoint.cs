using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportPoint : MonoBehaviour
{
    
    public UnityEvent OnTeleportEnter;
    public UnityEvent OnTeleport;
    public UnityEvent OnTeleportExit; // Evento de Unity

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false); 
    } 

    public void OnPointerEnterXR() //Remplazar la posicion del punto de tepeo
    {
        
        OnTeleportEnter?.Invoke();
        
    }

    public void OnPointerClickXR()
    {
        ExecuteTeleportation();
        OnTeleport?.Invoke();
        TeleportManager.Instance.DisableTeleportPoint(gameObject);
    }

    public void OnPointerExitXR() //Desactiva una parte del teleport
    {
        OnTeleportExit?.Invoke();
    }

    private void ExecuteTeleportation()
    {
        GameObject player = TeleportManager.Instance.Player; // trae el jugador del teleport manager
        player.transform.position = transform.position; // Teleporta al jugador a la posición del punto de teletransporte
        Camera camera = player.GetComponentInChildren<Camera>(); // Obtiene la cámara del jugador
        float rotY = transform.rotation.eulerAngles.y - camera.transform.localEulerAngles.y; // Calcula la rotación en el eje Y para que el jugador mire en la dirección del punto de teletransporte
        player.transform.rotation = Quaternion.Euler(0, rotY, 0); // Aplica la rotación al jugador
    }



}

