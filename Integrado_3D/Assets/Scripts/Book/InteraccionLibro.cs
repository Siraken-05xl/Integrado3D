using UnityEngine;
using UnityEngine.InputSystem;
public class InteraccionLibro : MonoBehaviour
{
    public GameObject libroUI;
    private bool jugadorCerca = false;
    void Update()
    {
        if (jugadorCerca && Keyboard.current.eKey.wasPressedThisFrame)
        {
            AbrirLibro();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Cerca del escritorio. Pulsa E para leer.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
    public void OnInteractuar(InputAction.CallbackContext context)
    {
        if (context.started && jugadorCerca)
        {
            AbrirLibro();
        }
    }

    void AbrirLibro()
    {
        if (libroUI == null)
        {
            Debug.LogError("¡ERROR! El objeto 'LibroCompleto' no está arrastrado en el Inspector del script. Por favor, arrástralo.");
            return;
        }

        libroUI.SetActive(true);
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        FindObjectOfType<GestorObjetivos>().SiguienteFase();
    }
}
