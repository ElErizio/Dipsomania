using UnityEngine;
using UnityEngine.UIElements;

public class CustomProgressBarController : MonoBehaviour
{
    public UIDocument uiDocument;
    public GameObject victoryMenu;
    public GameObject pauseButton;
    public GameObject lifeLeft;

    public Transform playerTransform; // Referencia a la posición del jugador
    public ScenarioGenerator scenarioGenerator; // Referencia al generador de tiles

    private VisualElement progressBarFill;
    private float currentProgress = 0f;
    private bool hasWon = false;

    private Vector3 finalTilePosition;

    private void Start()
    {
        var root = uiDocument.rootVisualElement;
        progressBarFill = root.Q<VisualElement>("progress-bar-fill");

        if (progressBarFill == null)
        {
            Debug.LogError("progress-bar-fill no encontrado en el UXML!");
        }
        else
        {
            UpdateProgress(0f);
        }

        if (victoryMenu != null)
        {
            victoryMenu.SetActive(false);
        }

        finalTilePosition = scenarioGenerator.GetFinalTilePosition();
    }

    private void Update()
    {
        if (!hasWon)
        {
            UpdateProgressBar();
        }
    }

    private void UpdateProgressBar()
    {
        float totalDistance = Vector3.Distance(Vector3.zero, finalTilePosition);
        float remainingDistance = Vector3.Distance(playerTransform.position, finalTilePosition);

        currentProgress = 100f * (1 - (remainingDistance / totalDistance));
        UpdateProgress(currentProgress);

        if (currentProgress >= 100f)
        {
            Debug.Log("¡Ganaste!");
            hasWon = true;

            ShowVictoryMenu();
            HideProgressBar();
            GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE);
        }
    }

    private void UpdateProgress(float percentage)
    {
        if (progressBarFill != null)
        {
            float translateY = 100 - percentage;
            progressBarFill.style.translate = new StyleTranslate(new Translate(0, Length.Percent(translateY), 0));
        }
    }

    private void HideProgressBar()
    {
        uiDocument.gameObject.SetActive(false);
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
        }
        else
        {
            Debug.LogError("Pause button no asignado en el Inspector.");
        }
    }
}
