using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeShake : MonoBehaviour
{
    [Header("Configuración del Árbol")]
    public float shakeIntensity = 0.5f;
    public float shakeDuration = 1.0f;
    public ParticleSystem leafParticles;

    [Header("Montones de hojas")]
    public List<GameObject> listaMontones = new List<GameObject>();

    [Tooltip("Tiempo en segundos que tarda el montón en aparecer")]
    public float tiempoAparicion = 1.5f;

    private bool isPlayerInside = false;
    private bool isShaking = false;
    private Vector3 originalRotation;

    void Start()
    {
        originalRotation = transform.eulerAngles;

        foreach (GameObject monton in listaMontones)
        {
            if (monton != null) monton.transform.localScale = Vector3.zero;
        }

        if (leafParticles == null)
        {
            leafParticles = GetComponent<ParticleSystem>();
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E) && !isShaking)
        {
            StartCoroutine(ShakeRoutine());
        }
    }

    IEnumerator ShakeRoutine()
    {
        isShaking = true;
        if (leafParticles != null) leafParticles.Play();

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float z = Random.Range(-1f, 1f) * shakeIntensity;
            transform.eulerAngles = originalRotation + new Vector3(0, 0, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = originalRotation;

        foreach (GameObject monton in listaMontones)
        {
            if (monton != null)
            {
                StartCoroutine(GrowMonton(monton));
            }
        }

        isShaking = false;
    }

    IEnumerator GrowMonton(GameObject monton)
    {
        float timer = 0f;
        Vector3 targetScale = Vector3.one;

        while (timer < tiempoAparicion)
        {
            timer += Time.deltaTime;
            monton.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, timer / tiempoAparicion);
            yield return null;
        }
        monton.transform.localScale = targetScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerInside = false;
    }
}
