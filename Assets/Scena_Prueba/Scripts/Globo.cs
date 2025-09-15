using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globo : MonoBehaviour
{
    public AudioClip audioGlobo;
    public AudioSource audioSource;
    public GameObject controlador;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dardo") || collision.gameObject.CompareTag("Flecha"))
        {
            if (collision.gameObject.CompareTag("Dardo"))
            {
                controlador.GetComponent<Controlador>().AddScore(10);
            }
            else if (collision.gameObject.CompareTag("Flecha"))
            {
                controlador.GetComponent<Controlador>().AddScore(1);
            }
            audioSource.PlayOneShot(audioGlobo);

            // Eliminar el globo de la lista de globos instanciados
            SpawnerGlobo spawner = controlador.GetComponent<SpawnerGlobo>();
            if (spawner != null)
            {
                spawner.globosInstanciados.Remove(gameObject); // Eliminar de la lista
            }

            // Desactivar el globo
            gameObject.SetActive(false);
        }
    }
}

