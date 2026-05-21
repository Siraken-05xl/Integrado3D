using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float velocidad = 5f;
    public float velocidadRotacion = 15f;

    [Header("Referencias de Animación")]
    private Animator animator; // Variable interna para el componente Animator

    private Vector2 inputMovimiento;

    void Start()
    {
        // CAMBIO CLAVE: Buscamos el Animator en los hijos (donde está el modelo de Blender)
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
        float x = inputMovimiento.x;
        float z = inputMovimiento.y;

        Vector3 direccionInput = new Vector3(x, 0, z).normalized;

        // Comprobamos si nos estamos moviendo
        if (direccionInput.magnitude >= 0.1f)
        {
            float anguloObjetivo = Mathf.Atan2(direccionInput.x, direccionInput.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            Quaternion rotacionObjetivo = Quaternion.Euler(0f, anguloObjetivo, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);

            Vector3 direccionMovimiento = Quaternion.Euler(0f, anguloObjetivo, 0f) * Vector3.forward;
            controller.Move(direccionMovimiento.normalized * velocidad * Time.deltaTime);

            // ¡ANIMACIÓN!: Activamos el booleano del Animator en el hijo
            if (animator != null)
            {
                animator.SetBool("IsWalking", true);
            }
        }
        else
        {
            // ¡ANIMACIÓN!: Desactivamos el booleano al detenernos
            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
            }
        }

        // Aplicar gravedad si no está tocando el suelo
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 9.81f * Time.deltaTime);
        }
    }
}
