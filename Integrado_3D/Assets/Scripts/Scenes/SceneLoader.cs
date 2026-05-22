using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Animator fadeAnimator;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeAnimator.Play("FadeIn");
    }

    public void CambiarEscena(string nombreEscena)
    {
        StartCoroutine(EjecutarTransicion(nombreEscena));
    }

    IEnumerator EjecutarTransicion(string nombreEscena)
    {
        fadeAnimator.SetTrigger("StartFadeOut");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nombreEscena);
    }
}
