/*using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DartPhysics : MonoBehaviour
{
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private float velocidad = 10f;
    private float vel_angular = 0.3f;
    private float drag = 0f;
    private float a_drag = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Listen for release event
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {

        rb.MoveRotation(Quaternion.Euler(45f, 90f, 270f));

        // Get controller velocity & angular velocity at release
        rb.velocity = args.interactorObject.transform.forward * velocidad; // Adjust speed multiplier as needed
        //rb.angularVelocity = args.interactorObject.transform.right * 5f; // Simulate dart spin
        rb.angularVelocity *= vel_angular;

        rb.drag = drag;
        rb.angularDrag = a_drag; 

        rb.useGravity = true;
    }
}*/

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DartPhysics : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    private float velocidad = 12f; // Aumenta la velocidad inicial
    private float vel_angular = 0.3f;
    private float drag = 0.05f;
    private float a_drag = 0.05f;
    private float torque = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Escuchar eventos de agarrar y soltar
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Desbloquear todas las restricciones al agarrar el dardo
        rb.constraints = RigidbodyConstraints.None;
        rb.AddTorque(rb.transform.up * (torque * 0.1f), ForceMode.Impulse);
        Debug.Log("Dardo agarrado!");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Aplicar velocidad en la dirección del lanzamiento
        // rb.velocity = args.interactorObject.transform.forward * velocidad;

        // Reducir rotación en el aire
        rb.angularVelocity = Vector3.zero;

        // Aplicar un giro en el eje Y para simular el giro del dardo
        ///rb.angularVelocity = rb.transform.up * 10f;
        rb.AddTorque(rb.transform.up * torque, ForceMode.Impulse);

        // Reducir resistencia
        rb.drag = drag;
        rb.angularDrag = a_drag;

        // Activar gravedad
        rb.useGravity = true;

        // Fijar orientación del dardo al soltarlo
        rb.MoveRotation(Quaternion.Euler(45f, 90f, 270f));

        // Aplicar una ligera fuerza extra hacia adelante para estabilidad
        rb.AddForce(rb.transform.forward * 2f, ForceMode.Impulse);

        // Bloquear solo rotaciones en X y Z para simular un giro de dardo
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        

        Debug.Log("Dardo lanzado!");
    }
}



