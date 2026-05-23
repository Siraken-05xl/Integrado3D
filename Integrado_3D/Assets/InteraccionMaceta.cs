using UnityEngine;
using UnityEngine.SceneManagement;

public class InteraccionMaceta : MonoBehaviour
{
    public string nombreEscenaMinijuego = "MinijuegoCruz";

    public static bool tieneFlor = false;

    void OnMouseDown()
    {
        if (tieneFlor)
        {
            Debug.Log("Flor detectada. Cargando minijuego...");
            SceneManager.LoadScene(nombreEscenaMinijuego);
        }
        else
        {
            Debug.Log("Necesitas una flor para interactuar.");
        }
    }
}
