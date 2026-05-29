using UnityEngine;

public class Interaccion : MonoBehaviour
{
    public GameObject canvasE;

    void Start()
    {
        canvasE.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasE.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasE.SetActive(false);
        }
    }
}
