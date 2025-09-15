using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{ 
    public Material material1, material2;

    public void Putmaterial1()
    {
        GetComponent<Renderer>().material = material1;
    }

    public void Putmaterial2()
    {
        GetComponent<Renderer>().material = material2;
    }
}
    
