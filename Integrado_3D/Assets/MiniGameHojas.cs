using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class MinijuegoHojasFinal : MonoBehaviour
{
    [Header("Referencias UI")]
    public RectTransform insecto;
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI contadorGolpesTxt; // Arrastra aquí tu nuevo texto de golpes
    public GameObject contenidoJuego;

    [Header("Panel de Feedback")]
    public GameObject panelResultado;
    public TextMeshProUGUI textoResultado;
    public Button botonContinuar;

    [Header("Posiciones (Donde asoma)")]
    public Vector2[] posHojas;

    [Header("Ajustes de Juego")]
    public float tLimite = 20f;
    public float vSalida = 0.8f;
    public float vOculto = 1.0f;
    public int metaGolpes = 3;

    private float cGlobal, tSalto;
    private int hits;
    private bool estaAsomando;
    private bool juegoTerminado;

    void OnEnable()
    {
        cGlobal = tLimite;
        hits = 0;
        estaAsomando = false;
        juegoTerminado = false;
        tSalto = vOculto;

        if (panelResultado) panelResultado.SetActive(false);
        if (contenidoJuego) contenidoJuego.SetActive(true);
        if (timerTxt) timerTxt.gameObject.SetActive(true);
        if (insecto) insecto.gameObject.SetActive(false);

        // Inicializamos el texto del contador
        ActualizarTextoContador();
        if (contadorGolpesTxt) contadorGolpesTxt.gameObject.SetActive(true);
    }

    void Update()
    {
        if (juegoTerminado) return;

        cGlobal -= Time.unscaledDeltaTime;

        if (timerTxt)
        {
            int m = Mathf.FloorToInt(cGlobal / 60);
            int s = Mathf.FloorToInt(cGlobal % 60);
            timerTxt.text = string.Format("{0:00}:{1:00}", m, s);
        }

        if (cGlobal <= 0) FinalizarInmediato(false);

        tSalto -= Time.unscaledDeltaTime;
        if (tSalto <= 0)
        {
            if (!estaAsomando) Aparecer();
            else Ocultar();
        }
    }

    void Aparecer()
    {
        if (posHojas.Length == 0 || insecto == null) return;

        int indice = Random.Range(0, posHojas.Length);
        insecto.anchoredPosition = posHojas[indice];

        insecto.gameObject.SetActive(true);
        estaAsomando = true;
        tSalto = vSalida;
    }

    void Ocultar()
    {
        if (insecto) insecto.gameObject.SetActive(false);
        estaAsomando = false;
        tSalto = vOculto;
    }

    public void AlGolpearInsecto()
    {
        if (!estaAsomando || juegoTerminado) return;

        hits++;
        Ocultar();
        ActualizarTextoContador(); // Actualiza el texto en pantalla al golpear

        if (hits >= metaGolpes)
        {
            FinalizarInmediato(true);
        }
    }

    void ActualizarTextoContador()
    {
        if (contadorGolpesTxt)
        {
            contadorGolpesTxt.text = "Golpes: " + hits + " / " + metaGolpes;
        }
    }

    void FinalizarInmediato(bool victoria)
    {
        juegoTerminado = true;
        Ocultar();

        if (contenidoJuego) contenidoJuego.SetActive(false);
        if (timerTxt) timerTxt.gameObject.SetActive(false);
        if (contadorGolpesTxt) contadorGolpesTxt.gameObject.SetActive(false); // Ocultamos el contador al terminar

        if (panelResultado && textoResultado)
        {
            textoResultado.text = victoria ? "¡Insecto capturado!" : "Ouw.. ¡Parece que se te ha escapado!";
            panelResultado.SetActive(true);
        }

        if (botonContinuar)
        {
            botonContinuar.onClick.RemoveAllListeners();
            botonContinuar.onClick.AddListener(CerrarMinijuego);
        }
    }

    public void CerrarMinijuego()
    {
        this.gameObject.SetActive(false);
    }
}
