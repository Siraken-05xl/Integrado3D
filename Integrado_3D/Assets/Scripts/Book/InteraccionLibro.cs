using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class InteraccionLibro : MonoBehaviour
{
    public GameObject libroUI;
    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            AbrirLibro();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = false;
    }

    void AbrirLibro()
    {
        if (ProgresoJuego.instance != null &&
            ProgresoJuego.instance.minijuego1Completado &&
            ProgresoJuego.instance.minijuego2Completado)
        {
            Debug.Log("¡Ambos minijuegos completados! Saltando a la escena final...");
            SceneManager.LoadScene("SCN_Final");
        }
        else
        {
            libroUI.SetActive(true);
            Time.timeScale = 0f;

            GestorObjetivos gestor = FindObjectOfType<GestorObjetivos>();
            if (gestor != null)
            {
                gestor.SiguienteFase();
            }
        }
    }
}
