using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class MinigameHojasFinal : MonoBehaviour
{
    [Header("Referencias UI")]
    public RectTransform insecto;
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI contadorGolpesTxt;
    public GameObject contenidoJuego;

    [Header("Panel de Feedback")]
    public GameObject panelResultado;
    public TextMeshProUGUI textoResultado;  
    public Button botonContinuar;

    [Header("Resultado UI")]
    public GameObject imagenVictoria;
    public GameObject textoDerrota;

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

        ActualizarTextoContador();
        if (contadorGolpesTxt) contadorGolpesTxt.gameObject.SetActive(true);

        ScreenFader.instance.CerrarCirculo();
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
        ActualizarTextoContador();

        if (hits >= metaGolpes)
        {
            FinalizarInmediato(true);
        }
    }

    void ActualizarTextoContador()
    {
        if (contadorGolpesTxt)
        {
            contadorGolpesTxt.text = "Hits: " + hits + " / " + metaGolpes;
        }
    }

    void FinalizarInmediato(bool victoria)
    {
        juegoTerminado = true;
        Ocultar();

        if (contenidoJuego) contenidoJuego.SetActive(false);
        if (timerTxt) timerTxt.gameObject.SetActive(false);
        if (contadorGolpesTxt) contadorGolpesTxt.gameObject.SetActive(false);

        if (panelResultado)
        {
            panelResultado.SetActive(true);

            if (victoria)
            {
                imagenVictoria.SetActive(true);
                textoDerrota.SetActive(false);
                ProgresoJuego.instance.MarcarComoCompletado(1);
                NotificadorObjetos.instance.MostrarIcono(2);
            }
            else
            {
                imagenVictoria.SetActive(false);
                textoDerrota.SetActive(true);
            }
        }

        if (botonContinuar)
        {
            botonContinuar.onClick.RemoveAllListeners();
            botonContinuar.onClick.AddListener(CerrarMinijuego);
        }
    }

    public void CerrarMinijuego()
    {
        Time.timeScale = 1f;
        ScreenFader.instance.AbrirCirculo(this.gameObject);
    }
}
