using UnityEngine;

public class Seguidor : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    void Update()
    {
        transform.position = objetivo.position + offset;
    }
}
