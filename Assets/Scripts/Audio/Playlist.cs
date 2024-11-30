using UnityEngine;
using System.Collections.Generic;

public class Playlist : MonoBehaviour
{
    // Lista de canciones asignada desde el inspector
    public List<AudioClip> canciones; 
    private AudioSource audioSource;
    private int cancionActual;

    void Awake()
    {
        // Asegurarse de que el objeto no se destruya entre escenas
        // DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        if (canciones.Count > 0)
        {
            // Elegir una canci�n aleatoria al inicio
            cancionActual = Random.Range(0, canciones.Count);
            audioSource.clip = canciones[cancionActual];
            audioSource.Play();
        }
    }

    void Update()
    {
        // Si termina la canci�n actual, reproducir la siguiente
        if (!audioSource.isPlaying && canciones.Count > 0)
        {
            SiguienteCancion();
        }
    }

    void SiguienteCancion()
    {
        // Seleccionar la siguiente canci�n
        cancionActual = (cancionActual + 1) % canciones.Count;
        audioSource.clip = canciones[cancionActual];
        audioSource.Play();
    }

    public void RecargarEscena()
    {
        // Elegir una nueva canci�n aleatoria
        if (canciones.Count > 0)
        {
            cancionActual = Random.Range(0, canciones.Count);
            audioSource.clip = canciones[cancionActual];
            audioSource.Play();
        }
    }
}
