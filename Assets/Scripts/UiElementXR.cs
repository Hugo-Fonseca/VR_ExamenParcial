using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UiElementXR : MonoBehaviour
{
    public UnityEvent  OnXRPointerEnter;
    public UnityEvent OnXRPointerExit;
    private Camera xRCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xRCamera = CameraPointerManager.Instance.gameObject.GetComponent<Camera>(); // traer el componente camera
    }

   public void OnPointerClickXR(){
        PointerEventData pointerEvent = PlacePointer();
        ExecuteEvents.Execute(this.gameObject,pointerEvent,ExecuteEvents.pointerClickHandler);
   }
   public void OnPointerEnterXR()
    { // Método que se llama cuando el puntero entra en el área del objeto interactivo
        GazeManager.Instance.SetUpGaze(1.5f); // Configura el tiempo necesario para completar la selección por mirada
        OnXRPointerEnter?.Invoke();// Invoca el evento de entrada al puntero
        PointerEventData pointerEvent = PlacePointer();// Coloca el puntero en la posición del objeto interactivo
        ExecuteEvents.Execute(this.gameObject,pointerEvent,ExecuteEvents.pointerDownHandler); // Ejecuta el evento de pulsación del puntero en el objeto interactivo
    }
   public void OnPointerExitXR(){ 
    GazeManager.Instance.SetUpGaze(2.5f);
    OnXRPointerExit?.Invoke();
    PointerEventData pointerEvent = PlacePointer();
    ExecuteEvents.Execute(this.gameObject,pointerEvent,ExecuteEvents.pointerUpHandler);
   }
   private PointerEventData PlacePointer(){
    Vector3 screenPos = xRCamera.WorldToScreenPoint(CameraPointerManager.Instance.hitPoint);
    var pointer = new PointerEventData(EventSystem.current);
    pointer.position = new Vector2 (screenPos.x, screenPos.y);
    return pointer;
   }
}
