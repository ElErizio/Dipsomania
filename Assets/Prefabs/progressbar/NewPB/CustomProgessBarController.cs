using UnityEngine;
using UnityEngine.UIElements;

public class CustomProgressBarController : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement progressBarFill;
    private float currentProgress = 0f;
    private bool hasWon = false; // Variable para verificar si ya se alcanzó el 100% de progreso

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

    private void Update()
    {
        if (currentProgress < 100f)
        {
            currentProgress += 10f * Time.deltaTime;
            UpdateProgress(currentProgress);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentProgress = 0f;
            UpdateProgress(currentProgress);
            hasWon = false; // Reiniciar el estado de victoria al resetear
        }
    }

    public void UpdateProgress(float percentage)
    {
        if (progressBarFill != null)
        {
            float translateY = 100 - percentage;
            progressBarFill.style.translate = new StyleTranslate(new Translate(0, Length.Percent(translateY), 0));

            // Verificar si el progreso ha alcanzado o superado el 100%
            if (percentage >= 100f && !hasWon)
            {
                Debug.Log("¡Ganaste!");
                hasWon = true; // Marcar que ya se alcanzó el 100% de progreso

                // Cambiar el estado del juego a PAUSE
                GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE);
            }
        }
    }
}
