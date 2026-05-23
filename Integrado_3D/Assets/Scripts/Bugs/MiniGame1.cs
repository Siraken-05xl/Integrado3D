using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class MinijuegoCruzFinal : MonoBehaviour
{
    [Header("Referencias UI")]
    public RectTransform jugador;
    public RectTransform bicho;
    public TextMeshProUGUI timerTxt;
    public Slider barraProgreso;
    public GameObject contenidoJuego;

    [Header("Panel de Feedback")]
    public GameObject panelResultado;
    public TextMeshProUGUI textoResultado;
    public Button botonContinuar;

    [Header("Ajustes de Movimiento")]
    public float limX = 200f;
    public float limY = 200f;
    public float anchoB = 80f;
    public float velocidadBicho = 300f;

    [Header("Tiempos de Juego")]
    public float tLimite = 15f;
    public float tNecesario = 3f;

    private float cGlobal, cAcumulado;
    private bool juegoTerminado;
    private ControlesPlayer inputs;

    private Vector2 destinoBicho;
    private float tCambioDestino;

    void Awake()
    {
        inputs = new ControlesPlayer();
    }

    void OnEnable()
    {
        inputs.Enable();
        cGlobal = tLimite;
        cAcumulado = 0f;
        juegoTerminado = false;
        tCambioDestino = 0f;

        if (panelResultado) panelResultado.SetActive(false);
        if (contenidoJuego) contenidoJuego.SetActive(true);
        if (timerTxt) timerTxt.gameObject.SetActive(true);

        if (barraProgreso)
        {
            barraProgreso.gameObject.SetActive(true);
            barraProgreso.maxValue = tNecesario;
            barraProgreso.value = 0f;
        }

        if (jugador) jugador.anchoredPosition = Vector2.zero;
        if (bicho) bicho.anchoredPosition = Vector2.zero;

        destinoBicho = Vector2.zero;
    }

    void OnDisable()
    {
        inputs.Disable();
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

        if (cGlobal <= 0)
        {
            FinalizarInmediato(false);
            return;
        }

        Vector2 inputMov = inputs.Player.Move.ReadValue<Vector2>();
        MoverJugador(inputMov);

        tCambioDestino -= Time.unscaledDeltaTime;
        if (tCambioDestino <= 0f)
        {
            ElegirNuevoDestinoEnCruz();
            tCambioDestino = Random.Range(1.0f, 2.5f);
        }

        if (bicho)
        {
            Vector2 nuevaPos = Vector2.MoveTowards(bicho.anchoredPosition, destinoBicho, velocidadBicho * Time.unscaledDeltaTime);

            if (Mathf.Abs(nuevaPos.x) > Mathf.Abs(nuevaPos.y))
            {
                nuevaPos.y = 0;
            }
            else
            {
                nuevaPos.x = 0;
            }

            bicho.anchoredPosition = nuevaPos;
        }

        if (bicho && jugador && Vector2.Distance(jugador.anchoredPosition, bicho.anchoredPosition) <= anchoB)
        {
            cAcumulado += Time.unscaledDeltaTime;
            if (barraProgreso) barraProgreso.value = cAcumulado;

            if (cAcumulado >= tNecesario)
            {
                FinalizarInmediato(true);
            }
        }
    }

    void MoverJugador(Vector2 dir)
    {
        if (jugador == null) return;
        Vector2 pos = jugador.anchoredPosition;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            pos.x += dir.x * 400f * Time.unscaledDeltaTime;
            pos.y = 0;
        }
        else if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
        {
            pos.y += dir.y * 400f * Time.unscaledDeltaTime;
            pos.x = 0;
        }

        pos.x = Mathf.Clamp(pos.x, -limX, limX);
        pos.y = Mathf.Clamp(pos.y, -limY, limY);
        jugador.anchoredPosition = pos;
    }

    void ElegirNuevoDestinoEnCruz()
    {
        if (bicho == null) return;

        if (Mathf.Abs(bicho.anchoredPosition.x) < 5f && Mathf.Abs(bicho.anchoredPosition.y) < 5f)
        {
            if (Random.value > 0.5f)
            {
                destinoBicho = new Vector2(Random.Range(-limX, limX), 0f);
            }
            else
            {
                destinoBicho = new Vector2(0f, Random.Range(-limY, limY));
            }
        }

        else if (Mathf.Abs(bicho.anchoredPosition.x) > Mathf.Abs(bicho.anchoredPosition.y))
        {
            destinoBicho = new Vector2(Random.Range(-limX, limX), 0f);
        }

        else
        {
            destinoBicho = new Vector2(0f, Random.Range(-limY, limY));
        }
    }

    void FinalizarInmediato(bool victoria)
    {
        juegoTerminado = true;
        if (contenidoJuego) contenidoJuego.SetActive(false);
        if (timerTxt) timerTxt.gameObject.SetActive(false);
        if (barraProgreso) barraProgreso.gameObject.SetActive(false);

        if (panelResultado && textoResultado)
        {
            textoResultado.text = victoria ? "¡Insecto atrapado con éxito!" : "El tiempo se ha agotado...";
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
