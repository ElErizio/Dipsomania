using UnityEngine;
using UnityEngine.UIElements;

public class CustomProgressBarController : MonoBehaviour
{
    public UIDocument uiDocument;
    public GameObject victoryMenu; // Referencia al menú de victoria en el Canvas
    public GameObject pauseButton; // Referencia al botón de pausa en el Canvas

    private VisualElement progressBarFill;
    private float currentProgress = 0f;
    private bool hasWon = false;
    private int totalTilesForProgress = 50;

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

        // Asegurarse de que el menú de victoria y el botón de pausa estén desactivados al inicio si corresponde
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(false);
        }
    }

    public void IncrementProgress()
    {
        if (!hasWon)
        {
            currentProgress += 100f / totalTilesForProgress;
            UpdateProgress(currentProgress);

            if (currentProgress >= 100f)
            {
                Debug.Log("¡Ganaste!");
                hasWon = true;

                ShowVictoryMenu();
                HideProgressBar();
            }
        }
    }

    public void UpdateProgress(float percentage)
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

        // Ocultar el botón de pausa
        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }
        else
        {
            Debug.LogError("Pause button no asignado en el Inspector.");
        }
    }
}
