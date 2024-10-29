using UnityEngine;
using UnityEngine.UIElements;

public class CustomProgressBarController : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement progressBarFill;
    private float currentProgress = 0f;
    private bool hasWon = false; 
    private int totalTilesForProgress = 30; 

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
                GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE);
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
}
