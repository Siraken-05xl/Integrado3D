using UnityEngine;

public class LogicaLibro : MonoBehaviour
{
    public GameObject panelLibro;

    public void BotonCerrar()
    {
        panelLibro.SetActive(false);
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
