using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class MinijuegoCruzFinal : MonoBehaviour
{
    [Header("Referencias UI")]
    public RectTransform jugador, bicho;
    public TextMeshProUGUI timerTxt;
    public GameObject contenidoJuego; // Arrastra aquí el objeto que agrupa el fondo, bicho y cruz

    [Header("Panel de Feedback")]
    public GameObject panelResultado;
    public TextMeshProUGUI textoResultado;
    public Button botonContinuar; // Arrastra aquí el botón de continuar

    [Header("Configuración")]
    public float limX = 280f;
    public float limY = 400f;
    public float anchoB = 50f;
    public float tLimite = 20f;
    public float tNecesario = 3f;
    public float vJug = 600f;
    public float vBic = 350f;

    private float cGlobal, cAcumulado, tB;
    private Vector2 dirB;
    private bool juegoTerminado = false;

    void OnEnable()
    {
        cGlobal = tLimite;
        cAcumulado = 0;
        juegoTerminado = false;

        if (panelResultado) panelResultado.SetActive(false);
        if (contenidoJuego) contenidoJuego.SetActive(true); // El juego debe verse al empezar

        jugador.anchoredPosition = bicho.anchoredPosition = Vector2.zero;
        GetDir();
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

        Move(jugador, new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized, vJug);
        MoveBicho();

        if (Vector2.Distance(jugador.anchoredPosition, bicho.anchoredPosition) < 80f)
        {
            cAcumulado += Time.unscaledDeltaTime;
            if (cAcumulado >= tNecesario) FinalizarInmediato(true);
        }
    }

    // --- LÓGICA DE MOVIMIENTO (Igual que antes) ---
    void Move(RectTransform t, Vector2 d, float v)
    {
        Vector2 p = t.anchoredPosition + d * v * Time.unscaledDeltaTime;
        if (Mathf.Abs(p.y) > anchoB) { p.x = 0; p.y = Mathf.Clamp(p.y, -limY, limY); }
        else if (Mathf.Abs(p.x) > anchoB) { p.y = 0; p.x = Mathf.Clamp(p.x, -limX, limX); }
        else { p.x = Mathf.Clamp(p.x, -anchoB, anchoB); p.y = Mathf.Clamp(p.y, -anchoB, anchoB); }
        t.anchoredPosition = p;
    }

    void MoveBicho()
    {
        tB -= Time.unscaledDeltaTime;
        Vector2 pre = bicho.anchoredPosition;
        Move(bicho, dirB, vBic);
        if (tB <= 0 || Vector2.Distance(pre, bicho.anchoredPosition) < 0.1f)
        {
            dirB = bicho.anchoredPosition.magnitude > anchoB ? -bicho.anchoredPosition.normalized : GetDir();
            tB = Random.Range(0.5f, 1.2f);
        }
    }

    Vector2 GetDir() => dirB = (new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right })[Random.Range(0, 4)];

    // --- NUEVA LÓGICA DE FINALIZACIÓN ---

    void FinalizarInmediato(bool victoria)
    {
        juegoTerminado = true;

        // 1. Apagamos el contenido del minijuego para que solo quede el panel
        if (contenidoJuego) contenidoJuego.SetActive(false);
        if (timerTxt) timerTxt.gameObject.SetActive(false);

        // 2. Mostramos el panel de resultado
        if (panelResultado && textoResultado)
        {
            textoResultado.text = victoria ? "¡Insecto capturado!" : "Ouw.. ¡Parece que se te ha escapado!";
            panelResultado.SetActive(true);
        }

        // 3. Preparamos el botón de continuar
        if (botonContinuar)
        {
            botonContinuar.onClick.RemoveAllListeners();
            botonContinuar.onClick.AddListener(CerrarMinijuego);
        }
    }

    public void CerrarMinijuego()
    {
        // Apaga el panel del minijuego completo
        this.gameObject.SetActive(false);
    }
}
