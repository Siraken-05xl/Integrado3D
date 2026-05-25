using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float velocidadCaminar = 5f;
    public float velocidadCorrer = 8f;
    public float velocidadRotacion = 15f;

    [Header("Referencias de Animación")]
    private Animator animator;

    private Vector2 inputMovimiento;
    private bool estaCorriendo;
    private bool estaCazando = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("¡Ojo! No se ha encontrado el componente Animator en ningún hijo de " + gameObject.name);
        }
    }

    public void OnMover(InputAction.CallbackContext context)
    {
        inputMovimiento = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !estaCazando)
        {
            StartCoroutine(EjecutarCaza());
        }

        if (estaCazando) return;

        estaCorriendo = Keyboard.current.leftShiftKey.isPressed;

        float x = inputMovimiento.x;
        float z = inputMovimiento.y;

        Vector3 direccionInput = new Vector3(x, 0, z).normalized;

        if (direccionInput.magnitude >= 0.1f)
        {
            float anguloObjetivo = Mathf.Atan2(direccionInput.x, direccionInput.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            Quaternion rotacionObjetivo = Quaternion.Euler(0f, anguloObjetivo, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);

            Vector3 direccionMovimiento = Quaternion.Euler(0f, anguloObjetivo, 0f) * Vector3.forward;

            float velocidadActual = estaCorriendo ? velocidadCorrer : velocidadCaminar;
            controller.Move(direccionMovimiento.normalized * velocidadActual * Time.deltaTime);

            if (animator != null)
            {
                animator.SetBool("IsWalking", !estaCorriendo);
                animator.SetBool("IsRunning", estaCorriendo);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
            }
        }

        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 9.81f * Time.deltaTime);
        }
    }
    private System.Collections.IEnumerator EjecutarCaza()
    {
        estaCazando = true;
        animator.SetTrigger("UsarCaza");

        yield return new WaitForSeconds(0.8f);

        estaCazando = false;
    }
}