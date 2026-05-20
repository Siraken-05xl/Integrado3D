using UnityEngine;
using TMPro;

public class MinijuegoHojas : MonoBehaviour
{
    [Header("Referencias UI")]
    public RectTransform insecto;
    public TextMeshProUGUI timerTxt;

    [Header("Posiciones (Donde asoma el bicho)")]
    public Vector2[] posHojas;

    [Header("Ajustes de Dificultad")]
    public float tiempoLimite = 20f;
    public float velocidadSalida = 0.8f;
    public float intervaloOculto = 1.0f;
    public int golpesParaGanar = 3;

    private float cGlobal, tSalto;
    private int hits;
    private bool estaAsomando;

    void OnEnable()
    {
        cGlobal = tiempoLimite;
        hits = 0;
        estaAsomando = false;
        tSalto = intervaloOculto;

        if (insecto != null)
        {
            insecto.gameObject.SetActive(false);
        }

        Debug.Log("Minijuego Hojas: Iniciado");
    }

    void Update()
    {
        cGlobal -= Time.unscaledDeltaTime;
        if (timerTxt != null)
        {
            int m = Mathf.FloorToInt(cGlobal / 60);
            int s = Mathf.FloorToInt(cGlobal % 60);
            timerTxt.text = string.Format("{0:00}:{1:00}", m, s);
        }

        if (cGlobal <= 0) FinalizarJuego(false);

        tSalto -= Time.unscaledDeltaTime;

        if (tSalto <= 0)
        {
            if (!estaAsomando) AparecerBicho();
            else OcultarBicho();
        }
    }

    void AparecerBicho()
    {
        if (posHojas.Length == 0 || insecto == null) return;

        int indiceAleatorio = Random.Range(0, posHojas.Length);
        insecto.anchoredPosition = posHojas[indiceAleatorio];

        insecto.gameObject.SetActive(true);
        estaAsomando = true;
        tSalto = velocidadSalida;
    }

    void OcultarBicho()
    {
        if (insecto != null) insecto.gameObject.SetActive(false);
        estaAsomando = false;
        tSalto = intervaloOculto;
    }

    public void AlGolpearInsecto()
    {
        if (!estaAsomando) return;

        hits++;
        Debug.Log("¡Golpe al bicho! Total acumulado: " + hits);

        OcultarBicho();

        if (hits >= golpesParaGanar)
        {
            FinalizarJuego(true);
        }
    }

    void FinalizarJuego(bool victoria)
    {
        if (victoria)
        {
            Debug.Log("¡MINIJUEGO COMPLETADO! Insecto capturado.");
        }
        else
        {
            Debug.Log("¡TIEMPO AGOTADO! El insecto escapó.");
        }

        this.gameObject.SetActive(false);
    }
}
