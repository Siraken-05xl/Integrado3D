using UnityEngine;

public class RecogerFlor : MonoBehaviour
{
    private bool estaCerca = false;

    void Update()
    {
        if (estaCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (NotificadorObjetos.instance != null)
            {
                NotificadorObjetos.instance.MostrarIcono(0);
            }

            GameObject.Find("GestorJuego").GetComponent<GestorObjetivos>().SiguienteFase("");
            InteraccionMaceta.tieneFlor = true;

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) estaCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) estaCerca = false;
    }
}
