using UnityEngine;

public class InsectoClick : MonoBehaviour
{
    private bool jugadorCerca = false;
    public GameObject canvasMinijuego;
    void Update()
    {
        if (jugadorCerca && Input.GetMouseButtonDown(0))
        {
            IniciarMinijuego();
        }
    }

    void IniciarMinijuego()
    {
        Debug.Log("¡Minijuego activado!");
        canvasMinijuego.SetActive(true);

        Time.timeScale = 0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            Debug.Log("Jugador cerca del insecto. ¡Puedes atraparlo!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}
