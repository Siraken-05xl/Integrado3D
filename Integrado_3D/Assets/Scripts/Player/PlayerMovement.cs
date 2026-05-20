using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float velocidad = 5f;
    public float velocidadRotacion = 15f;

    private Vector2 inputMovimiento;

    public void OnMover(InputAction.CallbackContext context)
    {
        inputMovimiento = context.ReadValue<Vector2>();
    }

    void Update()
    {
        float x = inputMovimiento.x;
        float z = inputMovimiento.y;

        Vector3 direccionInput = new Vector3(x, 0, z).normalized;

        if (direccionInput.magnitude >= 0.1f)
        {
            float anguloObjetivo = Mathf.Atan2(direccionInput.x, direccionInput.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            Quaternion rotacionObjetivo = Quaternion.Euler(0f, anguloObjetivo, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);

            Vector3 direccionMovimiento = Quaternion.Euler(0f, anguloObjetivo, 0f) * Vector3.forward;
            controller.Move(direccionMovimiento.normalized * velocidad * Time.deltaTime);
        }

        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * 9.81f * Time.deltaTime);
        }
    }
}
