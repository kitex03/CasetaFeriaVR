using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGlobo : MonoBehaviour
{
    public GameObject spawner;
    public GameObject prefabGlobo;
    public float velocidadGlobos = 1.0f;
    public AudioClip audioGlobo;
    public AudioSource audioSource;
    public GameObject controlador;
    
    private int numGlobosTotal = 6;
    private int numGlobos = 0;
    public List<GameObject> globosInstanciados = new List<GameObject>();
    private float tiempo = 0.0f;

    void Update()
    {
        if (numGlobos < numGlobosTotal && tiempo > 1.0f)
        {
            tiempo = 0.0f;
            Vector3 randomOffset = new Vector3(Random.Range(-1, 2), 0, 0);
            GameObject nuevoGlobo = Instantiate(prefabGlobo, spawner.transform.position + randomOffset, Quaternion.identity);
            nuevoGlobo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            if (nuevoGlobo != null)
            {
                Globo globoComponent = nuevoGlobo.GetComponent<Globo>();
                if (globoComponent != null)
                {
                    globoComponent.audioGlobo = audioGlobo;
                    globoComponent.audioSource = audioSource;
                    globoComponent.controlador = controlador;
                    nuevoGlobo.transform.SetParent(spawner.transform);
                    globosInstanciados.Add(nuevoGlobo);
                    numGlobos++;
                }
                else
                {
                    Debug.LogError("Globo component is missing on the instantiated object.");
                }
            }
            else
            {
                Debug.LogError("Failed to instantiate prefabGlobo");
            }
        }
        moveGlobos();
        tiempo += Time.deltaTime;
    }

    private void moveGlobos()
    {
        List<GameObject> globosABorrar = new List<GameObject>();

        foreach (GameObject globo in globosInstanciados)
        {
            // Mueve el globo hacia arriba
            globo.transform.position += Vector3.up * Time.deltaTime * velocidadGlobos;

            // Si el globo sale fuera del �rea, lo agregamos a la lista de eliminaci�n
            if (globo.transform.position.y > 6)
            {
                globosABorrar.Add(globo);
            }
        }

        // Eliminar los globos de la lista y destruirlos
        foreach (GameObject globo in globosABorrar)
        {
            globosInstanciados.Remove(globo); // Eliminar de la lista
            //Destroy(globo); // Destruir el globo
            numGlobos--; // Actualizar el contador de globos
        }
    }
    
    public void ChangeNumGlobos(int numGlobos)
    {
        numGlobosTotal = numGlobos;
    }

}