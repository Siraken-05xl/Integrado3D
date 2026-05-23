using UnityEngine;
using Cinemachine;

public class Cams : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam1;

    private void Start()
    {
        // En lugar de apagarla, le damos prioridad 0 para que no sea la activa
        cam1.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) cam1.Priority = 100; // Prioridad alta = Se activa
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) cam1.Priority = 0; // Prioridad baja = Se apaga
    }
}