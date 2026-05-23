using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorRetorno : MonoBehaviour
{
    public string nombreEscenaCasa = "Casa";

    public void VolverALaCasa()
    {
        Time.timeScale = 1f;

        Cursor.visible = false;

        SceneManager.LoadScene(nombreEscenaCasa);
    }
}
