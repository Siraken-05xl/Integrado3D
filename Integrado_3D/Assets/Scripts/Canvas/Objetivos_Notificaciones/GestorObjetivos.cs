using UnityEngine;
using TMPro;
using System.Collections;

public class GestorObjetivos : MonoBehaviour
{
    public TextMeshProUGUI textoObjetivo;
    public TextMeshProUGUI textoNotificacion;
    public static int faseActual = 0;

    void Start()
    {
        ActualizarObjetivo();
    }

    public void SiguienteFase(string mensajeNotificacion = "")
    {
        faseActual++;
        ActualizarObjetivo();

        if (mensajeNotificacion != "")
        {
            StartCoroutine(MostrarNotificacionTemporal(mensajeNotificacion));
        }
    }

    IEnumerator MostrarNotificacionTemporal(string msg)
    {
        textoNotificacion.text = "[Objeto conseguido] " + msg;
        yield return new WaitForSeconds(3f);
        textoNotificacion.text = "";
    }

    void ActualizarObjetivo()
    {
        switch (faseActual)
        {
            case 0: textoObjetivo.text = ""; break;
            case 1: textoObjetivo.text = ""; break;
            case 2: textoObjetivo.text = ""; break;
            case 3: textoObjetivo.text = ""; break;
        }
    }
}
