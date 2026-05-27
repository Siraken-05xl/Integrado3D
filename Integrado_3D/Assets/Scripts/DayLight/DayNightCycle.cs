using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("Grados por segundo que rota el sol")]
    public float velocidadRotacion = 1f;

    void Update()
    {
        transform.Rotate(Vector3.right * velocidadRotacion * Time.deltaTime);
    }
}
