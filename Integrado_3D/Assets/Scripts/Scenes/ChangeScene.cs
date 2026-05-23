using UnityEngine;
using UnityEngine.InputSystem;

public class CambioEscena : MonoBehaviour
{
    public string nombreEscenaCargar;
    private bool jugadorEstaCerca = false;
    private SceneLoader gestor;

    private void Start()
    {
        GameObject obj = GameObject.Find("GestorTransiciones");
        if (obj != null)
        {
            gestor = obj.GetComponent<SceneLoader>();
        }
        else
        {
            Debug.LogError("¡Cuidado! No se encontró el objeto 'GestorTransiciones' en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) jugadorEstaCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jugadorEstaCerca = false;
    }

    public void OnInteractuar(InputAction.CallbackContext context)
    {
        if (context.started && jugadorEstaCerca && gestor != null)
        {
            gestor.CambiarEscena(nombreEscenaCargar);
        }
        else if (context.started && jugadorEstaCerca && gestor == null)
        {
            Debug.LogWarning("Intento de cambio de escena, pero el GestorTransiciones no está asignado.");
        }
    }
}
