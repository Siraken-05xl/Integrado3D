using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cams : MonoBehaviour
{
    #region Inspector

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera cam1;

    #endregion

    #region Monobehaviour

    private void Start()
    {
        cam1.gameObject.SetActive(false);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam1.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam1.gameObject.SetActive(false);
        }
    }

    #endregion
}