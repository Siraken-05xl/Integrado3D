using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToGame : MonoBehaviour
{
    public string nombreEscenaMinijuego = "MiniGameTopo";
    void OnMouseUpAsButton()
    {
        Debug.Log("¡Clic detectado en: " + gameObject.name);
        SceneManager.LoadScene(nombreEscenaMinijuego);
    }
}
