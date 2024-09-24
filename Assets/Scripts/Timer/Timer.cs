using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60f; // 1 minuto (60 segundos)
    public TextMeshProUGUI timerText; 
    private bool isGamePaused = false;

    void Update()
    {
        if (timeRemaining > 0 && !isGamePaused)
        {
            // Restar tiempo
            timeRemaining -= Time.deltaTime;

            // Cambiar el color del texto a rojo cuando queden menos de 10 segundos
            if (timeRemaining <= 10)
            {
                timerText.color = Color.red;
            }

            // Actualizar UI (si estás usando una)
            if (timerText != null)
            {
                timerText.text = Mathf.Round(timeRemaining).ToString();
            }

            // Verificar si el tiempo ha llegado a 0
            if (timeRemaining <= 0)
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0; // Pausa el juego
        Debug.Log("El juego se ha pausado.");
    }
}
