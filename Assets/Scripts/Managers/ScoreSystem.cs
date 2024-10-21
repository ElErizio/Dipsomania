using UnityEngine;
using UnityEngine.UI; // Para la barra de progreso 

public class ScoreSystem : MonoBehaviour
{
    
    private int currentScore = 0;// Variable para contar las colisiones
    public int objectiveScore = 10; // Variable pública para establecer el objetivo
    public float progressPercentage = 0f; // Variable para almacenar el progreso en porcentaje
    public Slider progressBar; // Slider UI para mostrar el progreso

    // Método que se llama cuando algo colisiona con el "Destroyer"
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        currentScore++;

        progressPercentage = (float)currentScore / (float)objectiveScore * 100f;

        if (progressBar != null)
        {
            progressBar.value = progressPercentage / 100f;
        }

        // Comprobar si el objetivo se ha cumplido
        if (currentScore >= objectiveScore)
        {
            Debug.Log("Nivel completado");

        }
    }
}
