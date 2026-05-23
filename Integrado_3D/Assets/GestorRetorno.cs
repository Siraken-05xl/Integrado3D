using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorRetorno : MonoBehaviour
{
    public string nombreEscenaCasa = "Casa";

    public void VolverALaCasa() 
    {
        // ¡ESTO ES LO QUE TE FALTA!
        Time.timeScale = 1f;

        SceneManager.LoadScene(nombreEscenaCasa);
    }
}
