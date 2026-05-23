using UnityEngine;

public class LogicaMinijuego : MonoBehaviour
{
    public void FallarMinijuego()
    {
        this.gameObject.SetActive(false);

        Time.timeScale = 1f;

        Debug.Log("¡Fallaste! Inténtalo de nuevo en la maceta.");
    }
}
