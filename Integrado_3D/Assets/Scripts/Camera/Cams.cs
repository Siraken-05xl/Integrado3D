using UnityEngine;
using Cinemachine;

public class Cams : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cam1;

    private void Start()
    {
        cam1.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) cam1.Priority = 100;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) cam1.Priority = 0;
    }
}