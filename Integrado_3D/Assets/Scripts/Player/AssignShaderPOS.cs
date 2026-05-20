using UnityEngine;

public class AsignarPosicionShader : MonoBehaviour
{
    void Update()
    {
        Shader.SetGlobalVector("_PlayerPos", transform.position);
    }
}
