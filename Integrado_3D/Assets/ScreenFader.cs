using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance;
    private Animator anim;

    void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void CerrarCirculo()
    {
        anim.Play("EntrarMinijuego");
    }

    public void AbrirCirculo(GameObject objetoADesactivar)
    {
        StartCoroutine(AnimarYSalir(objetoADesactivar));
    }

    private IEnumerator AnimarYSalir(GameObject objetoADesactivar)
    {
        anim.Play("SalirMinijuego");
        yield return new WaitForSecondsRealtime(1.1f);

        objetoADesactivar.SetActive(false);

        anim.Play("Ninguno");
    }
}
