using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject collisionParticlesPrefab; // Prefab de partículas a instanciar
    public AudioClip collisionSound; // Sonido que se reproducirá al colisionar

    private AudioSource audioSource;

    private void Start()
    {
        // Configuramos el AudioSource si no está ya en el GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Instanciar las partículas
            if (collisionParticlesPrefab != null)
            {
                Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity);
            }

            // Reproducir el sonido
            if (collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }
}
