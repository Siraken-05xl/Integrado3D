using UnityEngine;

public class InteraccionMaceta : MonoBehaviour
{
    public GameObject minijuego;
    public GestorObjetivos gestor;

    bool tieneFlor = false;

    public void OnInteractuar()
    {
        if (tieneFlor) { /* abrir juego */ }
        else { /* mostrar mensaje: "Necesito una flor" */ }

        minijuego.SetActive(true);
        Time.timeScale = 0f;

        gestor.SiguienteFase();
    }
}
