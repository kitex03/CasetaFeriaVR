using UnityEngine;

public class BallestaRaycast : MonoBehaviour
{
    public float indicatorLength = 0.5f;  // Tama�o corto del rayo
    private LineRenderer lineRenderer;

    void Start()
    {
        // A�adimos el LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Configuraci�n del rayo
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.001f;

        // Color azul sin gradiente
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = Color.red;

        // Definir que tiene dos puntos (inicio y fin)
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Definir punto final (rayo corto)
        Vector3 endPoint = transform.position + transform.forward * indicatorLength;

        // Dibujar la l�nea
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPoint);
    }
}
