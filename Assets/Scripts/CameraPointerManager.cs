using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointerManager : MonoBehaviour
{
     public static CameraPointerManager Instance;

    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f; // Distancia máxima del puntero desde la cámara
    [Range (0,1)] 
    [SerializeField] private float disPointerObject = 0.95f; // Distancia relativa entre el puntero y el objeto interactivo (0 = en la cámara, 1 = en el objeto)


    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;

    private readonly string interactableTag = "Interactable"; // Etiqueta para identificar objetos interactivos
    private float scaleSize = 0.025f;
    [HideInInspector] 
    public Vector3 hitPoint;

    private void Awake() 
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; // Asigna la instancia actual a la variable estática

        }
    }

    private void Start ()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;
    }

    private void GazeSelection() // Método que se llama cuando se completa la selección por mirada
    {
        _gazedAtObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver); // Envía el mensaje de clic al objeto interactivo
    }

   public void Update()
   {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance)) // Realiza un raycast desde la posición de la cámara hacia adelante para detectar objetos
        {
           hitPoint = hit.point;

            if (_gazedAtObject != hit.transform.gameObject) // Si el objeto que estamos mirando ha cambiado
            {
                
                _gazedAtObject?.SendMessage("OnPointerExitXR",  null, SendMessageOptions.DontRequireReceiver); // Envía el mensaje de salida al objeto anterior
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnterXR", null, SendMessageOptions.DontRequireReceiver); // Envía el mensaje de entrada al nuevo objeto interactivo
                GazeManager.Instance.StartGazeSelection(); // Inicia la selección por mirada para el nuevo objeto interactivo
            }

            if (hit.transform.CompareTag(interactableTag)) // Si el objeto tiene la etiqueta de interactivo
            {
                PointerOnGaze(hit.point);
            }
            else
            {
                PointerOutGaze(); // Si el objeto no es interactivo, restablece el puntero a su posición y escala predeterminadas
            }
        }
        else
        {
            
            _gazedAtObject?.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver); // Envía el mensaje de salida al objeto anterior si no se está mirando a ningún objeto
            _gazedAtObject = null;
        }

        
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);
        }

          }

        private void PointerOnGaze(Vector3 hitPoint) // Método para actualizar la posición y escala del puntero cuando se mira un objeto interactivo

    {
            float scaleFactor = scaleSize * Vector3.Distance(transform.position, hitPoint); // Calcula el factor de escala en función de la distancia entre la cámara y el punto de impacto
        pointer.transform.localScale  = Vector3.one * scaleFactor;
            pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, disPointerObject);

        }

        private void PointerOutGaze() // Método para restablecer la posición y escala del puntero cuando no se está mirando a ningún objeto interactivo
    {
            pointer.transform.localScale = Vector3.one * 0.1f; // Restablece la escala del puntero a un valor predeterminado
        pointer.transform.parent.transform.localPosition = new Vector3(0,0, maxDistancePointer); // Coloca el puntero a una distancia fija frente a la cámara
        pointer.transform.parent.parent.transform.rotation = transform.rotation;
            GazeManager.Instance.CancelGazeSelection();
        }

        private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t) // Método para calcular la posición del puntero entre la cámara y el punto de impacto utilizando interpolación lineal
    {
            float x = p0.x + t * (p1.x - p0.x);
            float y = p0.y + t * (p1.y - p0.y);
            float z = p0.z + t * (p1.z - p0.z);

            return new Vector3(x,y,z);
        }
}
