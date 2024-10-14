using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60f; // 1 minuto (60 segundos)
    public TextMeshProUGUI timerText;
    private bool isGamePaused = false;

    void Start()
    {
        // Suscribir al evento de cambio de estado
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
    }

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

            // Actualizar UI
            if (timerText != null)
            {
                timerText.text = Mathf.Round(timeRemaining).ToString();
            }

            // Verificar si el tiempo ha llegado a 0
            if (timeRemaining <= 0)
            {
                PauseGame(); // Pausar el juego cuando llegue a 0
            }
        }
    }

    // Método que se llama cuando cambia el estado del juego
    void OnGameStateChanged(GAME_STATE newState)
    {
        if (newState == GAME_STATE.PAUSE)
        {
            isGamePaused = true; // Pausa el timer
        }
        else if (newState == GAME_STATE.PLAY)
        {
            isGamePaused = false; // Reanuda el timer
        }
    }

    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0; // Pausa el juego
        Debug.Log("El juego se ha pausado.");
        GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE); // Cambiar el estado a PAUSE
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento cuando este objeto sea destruido
        if (GameManager.GetInstance() != null)
        {
            GameManager.GetInstance().OnGameStateChanged -= OnGameStateChanged;
        }
    }
}