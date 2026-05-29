using UnityEngine;

public class MovimientoInsectoNodos : MonoBehaviour
{
    public Transform[] nodos; // Arrastra aquí tus 4 objetos vacíos (Arriba, Abajo, Izq, Der)
    public Transform centro;  // Un objeto vacío en el centro de la cruz
    public float velocidad = 300f;

    private Transform destinoActual;
    private bool debePasarPorCentro = false;

    void Start() { destinoActual = nodos[Random.Range(0, nodos.Length)]; }

    void Update()
    {
        // Mover hacia el destino actual
        transform.position = Vector3.MoveTowards(transform.position, destinoActual.position, velocidad * Time.deltaTime);

        // Si llega al destino
        if (Vector3.Distance(transform.position, destinoActual.position) < 1f)
        {
            if (!debePasarPorCentro)
            {
                // Ahora forzamos a que vaya al centro
                destinoActual = centro;
                debePasarPorCentro = true;
            }
            else
            {
                // Ya pasó por el centro, ahora elige uno de los 4 extremos
                destinoActual = nodos[Random.Range(0, nodos.Length)];
                debePasarPorCentro = false;
            }
        }
    }
}