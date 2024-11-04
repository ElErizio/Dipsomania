using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Destruible))]
public class Player : MonoBehaviour
{
    public int maxLives = 3; // Número máximo de vidas
    private int currentLives; // Vidas actuales del jugador
    private UI_Manager uiManager; // Referencia al UI_Manager para actualizar los corazones

    bool isOnPlay;
    Destruible destruible;
    Rigidbody rb;

    public string invulnerableLMName; // Nombre de la capa para invulnerabilidad
    public float invulnerableTime; // Tiempo de invulnerabilidad después de recibir daño
    bool isInvulnerable;
    LayerMask defaultLM;

    private void Start()
    {
        currentLives = maxLives;
        defaultLM = gameObject.layer;
        destruible = GetComponent<Destruible>();
        rb = GetComponent<Rigidbody>();
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;

        // Obtener referencia al UI_Manager y mostrar vidas iniciales
        uiManager = FindObjectOfType<UI_Manager>();
        if (uiManager != null)
        {
            uiManager.InitializeHearts(currentLives); // Inicializa los corazones en el UI
        }

        OnGameStateChanged(GameManager.GetInstance().currentGameState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isOnPlay)
        {
            if (!isInvulnerable && collision.gameObject.CompareTag("Obstacle"))
            {
                print("Me pegó");
                try
                {
                    LayerMask.NameToLayer(invulnerableLMName);
                    StartCoroutine(Invulnerabilidad());
                }
                catch
                {
                    print("Layer mask no encontrado, no funciona la invulnerabilidad");
                }
                TakeDamage(1); // Llamar a TakeDamage cuando el jugador recibe daño
            }
        }
    }

    private void TakeDamage(int damage)
    {
        currentLives -= damage;

        if (uiManager != null)
        {
            uiManager.RemoveHeart(); // Actualizar el UI eliminando un corazón
        }

        if (currentLives <= 0)
        {
            // Lógica de game over si las vidas llegan a cero
            Debug.Log("Game Over");
            GameManager.GetInstance().ChangeGameState(GAME_STATE.GAME_OVER);
        }
    }

    void OnGameStateChanged(GAME_STATE _gs)
    {
        isOnPlay = _gs == GAME_STATE.PLAY;

        if (!isOnPlay)
        {
            rb.velocity = Vector3.zero;
        }
    }

    IEnumerator Invulnerabilidad()
    {
        isInvulnerable = true;
        StartCoroutine(InvulnerabilidadAnim());
        yield return new WaitForSeconds(0.1f);
        gameObject.layer = LayerMask.NameToLayer(invulnerableLMName);
        yield return new WaitForSeconds(invulnerableTime);
        gameObject.layer = defaultLM;
        isInvulnerable = false;
    }

    IEnumerator InvulnerabilidadAnim()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
        yield return new WaitForSeconds(0.2f);
        mesh.enabled = true;
        yield return new WaitForSeconds(0.2f);
        if (isInvulnerable)
        {
            StartCoroutine(InvulnerabilidadAnim());
        }
    }
}
