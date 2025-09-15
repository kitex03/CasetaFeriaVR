using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloquearPosicion : MonoBehaviour
{
    private bool JuegoEmpezado = false;
    private Vector3 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(JuegoEmpezado);
        if(!JuegoEmpezado)
        {
            transform.position = posicionInicial;
        }
    }
    
    public void EmpenzarJuego(bool juegoempezado)
    {
        JuegoEmpezado = juegoempezado;
    }
}
