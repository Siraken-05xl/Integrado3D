using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CambioEscena : MonoBehaviour
{
    public string nombreEscenaCargar;
    private bool jugadorEstaCerca = false;

    public void OnInteractuar(InputAction.CallbackContext context)
    {
        if (context.started && jugadorEstaCerca)
        {
            CargarNuevaEscena();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEstaCerca = true;
            Debug.Log("Jugador en zona. Pulsa E para entrar.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEstaCerca = false;
        }
    }

    public void CargarNuevaEscena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscenaCargar);
    }
}
