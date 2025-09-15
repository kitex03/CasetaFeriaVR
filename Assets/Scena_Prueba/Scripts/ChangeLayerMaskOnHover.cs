using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ChangeLayerMaskOnHover : MonoBehaviour
{
    public NearFarInteractor InteractorLeft;
    public NearFarInteractor InteractorRight;

    public InteractionLayerMask UILayerMask;
    public InteractionLayerMask defaultLayer;

    // Start is called before the first frame update
    void Start()
    {
        if (InteractorLeft == null || InteractorRight == null)
        {
            Debug.LogError("No se detectan los mandos");
        }
    }

    public void InteractorMaskChange(bool UI)
    {
        if (UI)
        {
            InteractorLeft.interactionLayers = UILayerMask;
            InteractorRight.interactionLayers = UILayerMask;
        }
        else
        {
            InteractorLeft.interactionLayers = defaultLayer;
            InteractorRight.interactionLayers = defaultLayer;
        }

    }
}
