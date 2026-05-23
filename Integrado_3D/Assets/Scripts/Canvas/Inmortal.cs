using UnityEngine;

public class PersistenciaGestor : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Gestor");
        if (objs.Length > 1) { Destroy(this.gameObject); }

        DontDestroyOnLoad(this.gameObject);
    }
}
