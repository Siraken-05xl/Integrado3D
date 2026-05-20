using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class MinijuegoHojasFinal : MonoBehaviour
{
    [Header("Referencias UI")]
    public RectTransform insecto;
    public TextMeshProUGUI timerTxt;
    public GameObject contenidoJuego; // Arrastra aquí el objeto que agrupa el fondo, las hojas y el bicho

    [Header("Panel de Feedback")]
    public GameObject panelResultado;
    public TextMeshProUGUI textoResultado;
    public Button botonContinuar; // Arrastra el botón de continuar aquí

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
        if (contenidoJuego) contenidoJuego.SetActive(true); // Se muestra el juego al empezar
        if (timerTxt) timerTxt.gameObject.SetActive(true);
        if (insecto) insecto.gameObject.SetActive(false);
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

    // Se vincula al componente Button del propio Insecto
    public void AlGolpearInsecto()
    {
        if (!estaAsomando || juegoTerminado) return;

        hits++;
        Ocultar();

        if (hits >= metaGolpes)
        {
            FinalizarInmediato(true);
        }
    }

    void FinalizarInmediato(bool victoria)
    {
        juegoTerminado = true;
        Ocultar();

        // 1. Apagamos los elementos visuales del juego por detrás
        if (contenidoJuego) contenidoJuego.SetActive(false);
        if (timerTxt) timerTxt.gameObject.SetActive(false);

        // 2. Mostramos el panel de resultado impecable
        if (panelResultado && textoResultado)
        {
            textoResultado.text = victoria ? "¡Insecto capturado!" : "Ouw.. ¡Parece que se te ha escapado!";
            panelResultado.SetActive(true);
        }

        // 3. Vinculamos el botón de continuar
        if (botonContinuar)
        {
            botonContinuar.onClick.RemoveAllListeners();
            botonContinuar.onClick.AddListener(CerrarMinijuego);
        }
    }

    public void CerrarMinijuego()
    {
        this.gameObject.SetActive(false); // Apaga el minijuego completo
    }
}
