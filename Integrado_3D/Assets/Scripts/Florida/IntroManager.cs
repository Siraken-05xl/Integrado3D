using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    void Start()
    {
        Invoke("CambiarEscena", 5f);
    }

    void CambiarEscena()
    {
        SceneManager.LoadScene(1);
    }
}
