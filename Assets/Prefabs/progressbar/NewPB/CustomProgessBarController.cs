using UnityEngine;
using UnityEngine.UIElements;

public class CustomProgressBarController : MonoBehaviour
{
    public UIDocument uiDocument;
    private VisualElement progressBarFill;
    private float currentProgress = 0f;
    private bool hasWon = false; // Variable para verificar si ya se alcanz� el 100% de progreso
    private int totalTilesForProgress = 30; // N�mero de tiles que representan el 100% de la barra

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

    // M�todo que incrementa el progreso al ser llamado
    public void IncrementProgress()
    {
        if (!hasWon) // Solo actualizar si no hemos ganado a�n
        {
            currentProgress += 100f / totalTilesForProgress; // Incrementa el progreso seg�n el total de tiles
            UpdateProgress(currentProgress);

            // Verificar si el progreso ha alcanzado o superado el 100%
            if (currentProgress >= 100f)
            {
                Debug.Log("�Ganaste!");
                hasWon = true; // Marcar que ya se alcanz� el 100% de progreso

                // Cambiar el estado del juego a PAUSE
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
