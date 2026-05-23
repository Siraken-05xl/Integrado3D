using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Minigame1 : MonoBehaviour
{
    [Header("Objetos a ocultar")]
    public GameObject grupoJuego; // Arrastra aquí un objeto padre que contenga al jugador, insecto y cruz

    [Header("Referencias UI")]
    public RectTransform insecto;
    public GameObject panelResultado;
    public TextMeshProUGUI textoResultado;
    public Slider barraProgreso;
    public TextMeshProUGUI textoTimer;

    [Header("Configuración")]
    public float tiempoTotal = 30f;
    public float tiempoParaGanar = 3f;

    private float tiempoRestante;
    private float tiempoAcumulado = 0f;
    private Transform jugadorTransform;
    private bool juegoTerminado = false;

    void Start()
    {
        tiempoRestante = tiempoTotal;
        panelResultado.SetActive(false);

        barraProgreso.minValue = 0f;
        barraProgreso.maxValue = 1f;
        barraProgreso.value = 0f;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) jugadorTransform = playerObj.transform;
    }

    void Update()
    {
        if (juegoTerminado) return;

        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            TerminarJuego(false);
        }
        textoTimer.text = string.Format("{0:00}:{1:00}", (int)tiempoRestante / 60, (int)tiempoRestante % 60);

        if (jugadorTransform != null)
        {
            float dist = Vector2.Distance(jugadorTransform.GetComponent<RectTransform>().anchoredPosition, insecto.anchoredPosition);

            if (dist < 60f)
                tiempoAcumulado += Time.deltaTime;
            else
                tiempoAcumulado = Mathf.Max(0, tiempoAcumulado - Time.deltaTime);

            float progreso = tiempoAcumulado / tiempoParaGanar;

            if (progreso >= 0.95f)
            {
                barraProgreso.value = 1f;
                TerminarJuego(true);
            }
            else
            {
                barraProgreso.value = progreso;
            }
        }
    }

    void TerminarJuego(bool victoria)
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        if (grupoJuego != null) grupoJuego.SetActive(false);

        Time.timeScale = 0f;
        panelResultado.SetActive(true);
        textoResultado.text = victoria ? "¡Insecto capturado!" : "Se te ha escapado el insecto";
    }
}
