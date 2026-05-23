using UnityEngine;

public class RecogerFlor : MonoBehaviour
{
    private bool estaCerca = false;

    void Update()
    {
        if (estaCerca && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("GestorJuego").GetComponent<GestorObjetivos>().SiguienteFase("Flor de campo");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) estaCerca = true; }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) estaCerca = false; }
}
