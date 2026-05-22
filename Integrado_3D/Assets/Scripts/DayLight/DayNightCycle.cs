using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("Grados por segundo que rota el sol")]
    public float velocidadRotacion = 1f;

    void Update()
    {
        // Rotamos la luz sobre el eje X para simular el paso del tiempo
        transform.Rotate(Vector3.right * velocidadRotacion * Time.deltaTime);
    }
}
