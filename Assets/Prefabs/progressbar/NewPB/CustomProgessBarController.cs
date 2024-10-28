using UnityEngine;
using UnityEngine.UIElements;

public class CustomProgressBarController : MonoBehaviour
{
    public UIDocument uiDocument; // Arrastra el UIDocument aquí en el Inspector
    private VisualElement progressBarFill;
    private float currentProgress = 0f; // Valor actual del progreso

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
            UpdateProgress(0f); // Inicializa el progreso en 0%
        }
    }

    private void Update()
    {
        // Llenado progresivo automático (ajusta la velocidad cambiando 10f)
        if (currentProgress < 100f)
        {
            currentProgress += 10f * Time.deltaTime; // Incremento continuo
            UpdateProgress(currentProgress);
        }

        // Prueba de reset al presionar la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentProgress = 0f; // Reinicia el progreso al 0%
            UpdateProgress(currentProgress);
        }
    }

    // Método para actualizar el progreso (entre 0 y 100)
    public void UpdateProgress(float percentage)
    {
        if (progressBarFill != null)
        {
            // Ajusta la altura en porcentaje para un relleno vertical
            progressBarFill.style.height = new Length(percentage, LengthUnit.Percent);
        }
    }
}
