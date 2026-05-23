using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class CamRender : MonoBehaviour
{
    private const string BENDING_FEATURE = "ENABLE_BENDING";

    private void Awake()
    {
        if (Application.isPlaying) Shader.EnableKeyword(BENDING_FEATURE);
        else Shader.DisableKeyword(BENDING_FEATURE);
    }

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    private static void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        // Esto evita que los objetos desaparezcan al curvar el mundo
        Matrix4x4 orthoMatrix = Matrix4x4.Ortho(-500, 500, -500, 500, 0.001f, 5000);
        camera.cullingMatrix = orthoMatrix * camera.worldToCameraMatrix;
    }
}