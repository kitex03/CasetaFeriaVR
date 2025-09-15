using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnerProyectil : MonoBehaviour
{
    public GameObject proyectil;
    public Transform spawner;
    public float speed;
    
    public Transform gatillo;

    private XRGrabInteractable grabInteractable;
    private List<IXRSelectInteractor> interactorList = new List<IXRSelectInteractor>();

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Suscribimos los eventos de agarre
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        // Desuscribimos para evitar errores
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Agregar la mano que ha agarrado el objeto
        if (!interactorList.Contains(args.interactorObject))
        {
            interactorList.Add(args.interactorObject);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Quitar la mano que ha soltado el objeto
        if (interactorList.Contains(args.interactorObject))
        {
            interactorList.Remove(args.interactorObject);
        }
    }

    public void Fire(ActivateEventArgs args)
    {
        float distance = Vector3.Distance(args.interactorObject.transform.position, gatillo.position);
        Debug.Log("distancia al gatillo" + distance);
        // Solo dispara si hay dos manos sujetando
        if (interactorList.Count >= 2 && distance < 0.1f)
        {
            Quaternion ajusteRotacion = Quaternion.Euler(0, 90, 0);  // Ajusta seg�n el prefab
            GameObject newObject = Instantiate(proyectil, spawner.position, spawner.rotation * ajusteRotacion, null);

            if (newObject.TryGetComponent(out Rigidbody rigibody))
            {
                Vector3 Fuerza = spawner.forward * speed;
                rigibody.AddForce(Fuerza);
            }
        }
    }
}
