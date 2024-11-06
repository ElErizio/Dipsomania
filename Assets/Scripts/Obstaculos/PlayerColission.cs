using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject collisionParticlesPrefab; // Prefab de partículas a instanciar

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity);
        }
    }
}

