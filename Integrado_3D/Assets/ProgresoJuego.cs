using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgresoJuego : MonoBehaviour
{
    public static ProgresoJuego instance;

    public bool minijuego1Completado = false;
    public bool minijuego2Completado = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarcarComoCompletado(int idMinijuego)
    {
        if (idMinijuego == 1) minijuego1Completado = true;
        if (idMinijuego == 2) minijuego2Completado = true;

        if (minijuego1Completado && minijuego2Completado)
        {
            Debug.Log("¡Ambos minijuegos completados! Cargando escena final...");
            SceneManager.LoadScene("SCN_Final");
        }
    }
}
