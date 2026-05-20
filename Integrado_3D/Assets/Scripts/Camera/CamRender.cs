using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class CamRender : MonoBehaviour
{
    #region Constants

    private const string BENDING_FEATURE = "ENABLE_BENDING";

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        if (Application.isPlaying)
        {
            Shader.EnableKeyword(BENDING_FEATURE);
        }
        else         
        {
            Shader.DisableKeyword(BENDING_FEATURE);
        }
    }

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    #endregion

    #region Methods
    private static void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        camera.cullingMatrix = Matrix4x4.Ortho(-99, 99, -99, 99, 0.001f, 99) * camera.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        camera.ResetCullingMatrix();
    }

    #endregion
}