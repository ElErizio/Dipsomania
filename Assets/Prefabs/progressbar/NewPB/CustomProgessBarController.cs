using UnityEngine;
using UnityEngine.UIElements;

public class CustomProgressBarController : MonoBehaviour
{
    public UIDocument uiDocument;

    public Transform playerTransform; // Referencia a la posición del jugador

    private VisualElement progressBarFill;
    private VisualElement runnerIcon;
    private VisualElement houseIcon;
    private float currentProgress = 0f;

    private Vector3 startPosition;
    private Vector3 finalTilePosition = new Vector3(0, 0, 400); // Posición fija de la tile final

    private void Start()
    {
        var root = uiDocument.rootVisualElement;
        progressBarFill = root.Q<VisualElement>("progress-bar-fill");
        runnerIcon = root.Q<VisualElement>("runner-icon");
        houseIcon = root.Q<VisualElement>("house-icon");

        if (progressBarFill == null)
        {
            Debug.LogError("progress-bar-fill no encontrado en el UXML!");
        }
        else
        {
            UpdateProgress(0f);
        }

        startPosition = playerTransform.position;
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        float totalDistance = Vector3.Distance(startPosition, finalTilePosition);
        float traveledDistance = Vector3.Distance(startPosition, playerTransform.position);

        currentProgress = 100f * (traveledDistance / totalDistance);
        UpdateProgress(currentProgress);
    }

    private void UpdateProgress(float percentage)
    {
        if (progressBarFill != null)
        {
            float translateY = 100 - percentage;
            progressBarFill.style.translate = new StyleTranslate(new Translate(0, Length.Percent(translateY), 0));

            // Actualiza la posición del monito
            if (runnerIcon != null)
            {
                runnerIcon.style.bottom = new Length(percentage, LengthUnit.Percent);
            }
        }
    }
}
