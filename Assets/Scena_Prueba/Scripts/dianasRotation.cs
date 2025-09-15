using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;

public class dianasRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private float angle = 0f;
 

    // Update is called once per frame
    void Update()
    {
            angle += rotationSpeed * Time.deltaTime;
            transform.DORotate(new Vector3(0, 0, angle), 0.1f);
        
    }
    
    public void ChangeSpeed(float speed)
    {
        rotationSpeed = speed;
    }
}
