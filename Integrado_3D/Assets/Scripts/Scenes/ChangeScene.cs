using UnityEngine;
using UnityEngine.InputSystem;

public class CambioEscena : MonoBehaviour
{
    public string nombreEscenaCargar;
    private bool jugadorEstaCerca = false;

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
        if (context.started && jugadorEstaCerca)
        {
            GameObject.Find("GestorTransiciones").GetComponent<SceneLoader>().CambiarEscena(nombreEscenaCargar);
        }
    }
}
