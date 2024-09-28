using UnityEngine;
using TMPro; // Asegúrate de incluir la referencia a TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public int points = 0;
    public int highscore = 0;
    public int pointsPerSecond = 5; // Puntos ganados por cada segundo que pasa
    public TextMeshProUGUI scoreText; // Cambiado a TextMeshProUGUI
    public TextMeshProUGUI highscoreText; // Cambiado a TextMeshProUGUI
    private bool isPlayerInsideTrigger = false; // Saber si el jugador está dentro del trigger
    private bool isGameRunning = true; // Controlar si el juego está corriendo
    private float timeElapsed = 0f; // Contador de tiempo para sumar puntos

    private void Start()
    {
        // Cargar el highscore guardado
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        UpdateUI();
    }

    private void Update()
    {
        if (isGameRunning)
        {
            // Acumular el tiempo transcurrido en cada frame
            timeElapsed += Time.deltaTime;

            // Si ha pasado un segundo, sumar puntos
            if (timeElapsed >= 1f)
            {
                AddPoints(pointsPerSecond);
                timeElapsed = 0f; // Reiniciar el contador de tiempo
                UpdateUI();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInsideTrigger)
        {
            // Cuando el jugador entra en el trigger, ganar puntos adicionales
            AddPoints(100); // 100 puntos por entrar en el trigger
            isPlayerInsideTrigger = true; // Evitar que se sumen puntos múltiples veces
            UpdateUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cuando el jugador sale del trigger, resetear la variable
            isPlayerInsideTrigger = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Restar puntos si el jugador golpea el objeto
            SubtractPoints(25);
            UpdateUI();
        }
    }

    private void AddPoints(int amount)
    {
        points += amount;
        CheckHighscore();
    }

    private void SubtractPoints(int amount)
    {
        points -= amount;
        if (points < 0) points = 0; // Evitar puntajes negativos
        CheckHighscore();
    }

    private void CheckHighscore()
    {
        if (points > highscore)
        {
            highscore = points;
            PlayerPrefs.SetInt("Highscore", highscore); // Guardar el nuevo highscore
        }
    }

    private void UpdateUI()
    {
        // Actualizar la interfaz de usuario con los puntos y el highscore
        scoreText.text = "Score: " + points;
        highscoreText.text = "Highscore: " + highscore;
    }

    // Métodos para pausar o continuar el juego
    public void PauseGame()
    {
        isGameRunning = false; // Pausar el sistema de puntos
    }

    public void ResumeGame()
    {
        isGameRunning = true; // Reanudar el sistema de puntos
    }

    private void OnDestroy()
    {
        // Guardar el highscore cuando se cierre el juego
        PlayerPrefs.SetInt("Highscore", highscore);
    }
}
