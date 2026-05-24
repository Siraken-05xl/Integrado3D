using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ControladorPaginas : MonoBehaviour
{
    public Image imagenPagina;
    public List<Sprite> paginas;
    private int indice = 0;

    void OnEnable()
    {
        indice = 0;
        ActualizarPagina();
    }

    public void Siguiente()
    {
        if (indice < paginas.Count - 1) { indice++; ActualizarPagina(); }
    }

    public void Anterior()
    {
        if (indice > 0) { indice--; ActualizarPagina(); }
    }

    void ActualizarPagina()
    {
        if (paginas.Count > 0) imagenPagina.sprite = paginas[indice];
    }
}
