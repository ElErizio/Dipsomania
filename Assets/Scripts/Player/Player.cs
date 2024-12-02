using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Destruible))]
public class Player : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    private UI_Manager uiManager;

    bool isOnPlay;
    Destruible destruible;
    Rigidbody rb;

    public string invulnerableLMName;
    public float invulnerableTime;
    public float totalDistance;
    bool isInvulnerable;
    LayerMask defaultLM;

    public GameObject victoryMenu;
    public GameObject pauseButton;
    public GameObject lifeLeft;
    public GameObject tutorialPanel;
    public UIDocument progressBarDocument;

    private void Start()
    {
        currentLives = maxLives;
        defaultLM = gameObject.layer;
        destruible = GetComponent<Destruible>();
        rb = GetComponent<Rigidbody>();
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;

        uiManager = FindObjectOfType<UI_Manager>();
        if (uiManager != null)
        {
            uiManager.InitializeHearts(currentLives);
        }

        totalDistance = PlayerPrefs.GetFloat("TotalDistance", 0f);

        OnGameStateChanged(GameManager.GetInstance().currentGameState);

        if (uiManager != null)
        {
            uiManager.UpdateDistanceText(totalDistance);
        }
    }

    private void Update()
    {
        if (isOnPlay)
        {
            totalDistance += rb.velocity.magnitude * Time.deltaTime * 200;

            PlayerPrefs.SetFloat("TotalDistance", totalDistance);
            PlayerPrefs.Save();

            if (uiManager != null)
            {
                uiManager.UpdateDistanceText(totalDistance);
            }
        }
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
                TakeDamage(1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOnPlay && other.CompareTag("WinTrigger"))
        {
            WinGame();
        }
    }

    private void TakeDamage(int damage)
    {
        currentLives -= damage;

        if (uiManager != null)
        {
            uiManager.RemoveHeart();
        }

        if (currentLives <= 0)
        {
            Debug.Log("Game Over");
            GameManager.GetInstance().ChangeGameState(GAME_STATE.GAME_OVER);

            if (uiManager != null)
            {
                uiManager.ShowGameOverPanel();
            }

            PlayerPrefs.SetFloat("TotalDistance", totalDistance);
            PlayerPrefs.Save();
        }
    }

    private void WinGame()
    {
        Debug.Log("¡Ganaste!");
        ShowVictoryMenu();
        GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE);

        PlayerPrefs.SetFloat("TotalDistance", totalDistance);
        PlayerPrefs.Save();
    }

    private void ShowVictoryMenu()
    {
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("Victory menu no asignado en el Inspector.");
        }

        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
            lifeLeft.SetActive(false);
            tutorialPanel.SetActive(false);
            progressBarDocument.gameObject.SetActive(false);
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
