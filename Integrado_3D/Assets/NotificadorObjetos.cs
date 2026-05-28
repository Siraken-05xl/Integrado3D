using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NotificadorObjetos : MonoBehaviour
{
    public static NotificadorObjetos instance;
    public Image iconoNotificacion;
    public List<Sprite> misIconos;
    public float tiempoVisible = 3f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (iconoNotificacion != null) iconoNotificacion.gameObject.SetActive(false);
    }

    public void MostrarIcono(int idIcono)
    {
        if (iconoNotificacion == null)
        {
            Debug.LogError("¡No has asignado la imagen en el Inspector de NotificadorObjetos!");
            return;
        }

        if (idIcono >= 0 && idIcono < misIconos.Count && misIconos[idIcono] != null)
        {
            iconoNotificacion.sprite = misIconos[idIcono];
            StopAllCoroutines();
            StartCoroutine(AnimarNotificacion());
        }
        else
        {
            Debug.LogWarning("El icono ID " + idIcono + " no existe o no tiene Sprite asignado.");
        }
    }

    private IEnumerator AnimarNotificacion()
    {
        iconoNotificacion.gameObject.SetActive(true);
        yield return new WaitForSeconds(tiempoVisible);
        iconoNotificacion.gameObject.SetActive(false);
    }
}
