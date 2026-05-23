using UnityEngine;

public class CerrarLibro : MonoBehaviour
{
    public GameObject libroCompleto;

    public void Cerrar()
    {
        libroCompleto.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
