using UnityEngine;

public class MovimientoJugadorNodoFinal : MonoBehaviour
{
    public Transform centro, arriba, abajo, izq, der;
    public float velocidad = 10f;
    private Transform objetivo;

    void Start() { objetivo = centro; }

    void Update()
    {
        // 1. INPUT: Cambiar de objetivo
        if (objetivo == centro)
        {
            if (Input.GetKeyDown(KeyCode.W)) objetivo = arriba;
            else if (Input.GetKeyDown(KeyCode.S)) objetivo = abajo;
            else if (Input.GetKeyDown(KeyCode.A)) objetivo = izq;
            else if (Input.GetKeyDown(KeyCode.D)) objetivo = der;
        }
        else // Si estoy en un brazo, solo puedo volver al centro
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                objetivo = centro;
            }
        }

        // 2. MOVER: Ir hacia el objetivo
        transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
    }
}
