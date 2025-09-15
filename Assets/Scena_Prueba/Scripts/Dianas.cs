using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diana : MonoBehaviour
{
    public GameObject centerObject; // Empty GameObject to calculate the center of the target
    public AudioClip audioDardo;
    public AudioSource audioSource;

    public GameObject controlador;
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected on {gameObject.name} with {collision.gameObject.name}, Tag: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("Dardo") || collision.gameObject.CompareTag("Flecha"))
        {
            // Save original scale before parenting
            Vector3 originalScale = collision.transform.localScale;

            Debug.Log("Valid projectile detected, calculating score...");

            // Calculate the center of the target's face (top of the cylinder)
            CapsuleCollider collider = GetComponent<CapsuleCollider>();

            // Calculate the center of the target using the empty GameObject
            Vector3 centroDiana = collider.bounds.center;

            // Calculate impact distance from the center
            float distancia = Vector3.Distance(centroDiana, collision.transform.position);
            Debug.Log($"Impact Distance from Center: {distancia}");

            // Stop the projectile movement
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            // Stick the projectile to the exact hit position
            collision.transform.SetParent(transform, true);
            collision.transform.position = collision.contacts[0].point;
            if (collision.gameObject.CompareTag("Flecha"))
            {
                collision.transform.rotation = Quaternion.LookRotation(-transform.forward);
            }
            else if (collision.gameObject.CompareTag("Dardo"))
            {
                collision.transform.rotation = Quaternion.Euler(new Vector3(45, 90, 270));
                collision.transform.localScale = new Vector3(1.5f, 150f, 1.5f);
            }


            // Restore original scale
            //collision.transform.localScale = originalScale;

            // Assign score based on distance (lower distance = higher score)
            int score = 0;
            if (distancia < 0.08f) score = 10;
            else if (distancia < 0.16f) score = 5;
            else if (distancia < 0.26f) score = 3;
            else if (distancia < 0.35f) score = 2;
            else if (distancia < 0.45f) score = 1;
            
            
            controlador.GetComponent<Controlador>().AddScore(score);
            
            audioSource.PlayOneShot(audioDardo);
        }
    }
}
