using System.Collections;
using UnityEngine;

public class BlinkOnCollision : MonoBehaviour
{
    private string targetTag = "Obstacle";
    private string targetLayerName = "Obstacle";
    public float blinkDuration = 3f; // Duración total del parpadeo
    public float blinkInterval = 0.2f; // Intervalo entre cada parpadeo

    private bool isOnPlay;

    private SkinnedMeshRenderer skinnedMeshRenderer;
    private bool isBlinking = false;

    private void Start()
    {
        // Obtenemos el SkinnedMeshRenderer del objeto actual
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer no encontrado en el objeto. Asegúrate de que tiene un componente SkinnedMeshRenderer.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si el objeto colisionado tiene el tag y layer especificados
        if (isOnPlay)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Choque con un obstaculo");
                StartCoroutine(InvulnerabilidadAnim());
            }
        }
    }
    IEnumerator InvulnerabilidadAnim()
        {
            SkinnedMeshRenderer mesh = GetComponent<SkinnedMeshRenderer>();
            mesh.enabled = false;
            yield return new WaitForSeconds(0.2f);
            mesh.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

    private IEnumerator Blink()
    {
        isBlinking = true;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            // Alterna la visibilidad del objeto
            skinnedMeshRenderer.enabled = !skinnedMeshRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        // Aseguramos que el objeto quede visible al finalizar
        skinnedMeshRenderer.enabled = true;
        isBlinking = false;
    }

    
}