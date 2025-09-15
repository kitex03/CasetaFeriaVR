using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;  // Necesario para manejar los botones
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public Button restartButton; // El bot�n de reinicio
    public GameObject menu; // El objeto del men� existente
	public GameObject menuInstrucciones; // El objeto del

    [Header("Dardos")]
    public GameObject[] dardos;
    private Transform[] originalParents;
    private Vector3[] originalPositions;

    private int score = 0;
    private int maxScore = 0;

    [Header("Temporizador")]
    public float tiempoRestante = 10f; // Tiempo l�mite de la partida (60 segundos)
    private bool juegoActivo = true;

    [Header("Sonido")]
    public AudioClip alarmaClip; // Clip de sonido de la alarma
    private AudioSource audioSource; // Componente AudioSource para reproducir sonidos

    void Start()
    {
        // Inicializar valores
        scoreText.text = score.ToString();
        timerText.text = tiempoRestante.ToString("F1") + "s";

        // Inicializar AudioSource
        audioSource = GetComponent<AudioSource>();

        // Ocultar el bot�n de reiniciar al inicio
        restartButton.gameObject.SetActive(false);

        // Guardar posiciones y padres originales de los dardos
        originalPositions = new Vector3[dardos.Length];
        originalParents = new Transform[dardos.Length];

        for (int i = 0; i < dardos.Length; i++)
        {
            originalPositions[i] = dardos[i].transform.position;
            originalParents[i] = dardos[i].transform.parent;
        }

        // Iniciar la cuenta regresiva
        //StartCoroutine(Temporizador());
    }

	public void IniciarJuego()
    {
		menuInstrucciones.SetActive(false);
        StartCoroutine(Temporizador());
    }

    IEnumerator Temporizador()
    {
        while (tiempoRestante > 0)
        {
            yield return new WaitForSeconds(1f);
            tiempoRestante--;
            timerText.text = tiempoRestante.ToString("F1") + "s";
        }

        // Fin del tiempo
        FinDelJuego();
    }

    public void AddScore(int points)
    {
        if (!juegoActivo) return; // No sumar puntos si el juego ha terminado

        score += points;
        scoreText.text = score.ToString();
        if (score > maxScore)
        {
            maxScore = score;
        }
    }

    public void FinDelJuego()
    {
        juegoActivo = false; // Bloquear interacciones
        timerText.text = "Tiempo terminado!";

        // Reproducir el sonido de alarma
        audioSource.PlayOneShot(alarmaClip);

        // Mostrar el bot�n de reiniciar
        restartButton.gameObject.SetActive(true);
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena
    }

    public void Reset()
    {
        for (int i = 0; i < dardos.Length; i++)
        {
            dardos[i].GetComponent<Rigidbody>().isKinematic = false;
            dardos[i].transform.position = originalPositions[i];
            dardos[i].transform.rotation = Quaternion.Euler(new Vector3(45, 90, 270));
            dardos[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            dardos[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            dardos[i].transform.SetParent(originalParents[i]);
            dardos[i].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            
        }

        /*score = 0;
        scoreText.text = score.ToString();*/
    }
}



