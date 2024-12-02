using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI; // Para UI tradicional (uGUI)
using UnityEngine.UIElements; // Para UI Toolkit

public class UI_Manager : MonoBehaviour
{
    public GameObject heartPrefab;
    public GameObject brokenBottlePrefab; // Prefab de la botella rota
    public Transform livesContainer;
    public GameObject gameOverPanel; // Reference to the Game Over panel
    public GameObject pauseButton; // Reference to the Pause button on the Canvas
    public GameObject tutorialPanel;
    public UIDocument progressBarDocument; // Reference to the progress bar UIDocument
    public TextMeshProUGUI distanceText; // Referencia al objeto de texto TextMeshPro en el menú de configuraciones
    public UnityEngine.UI.Button resetButton; // Especificar el namespace completo

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        gameOverPanel.SetActive(false); // Hide Game Over panel at the start
        if (pauseButton != null) pauseButton.SetActive(true); // Show pause button initially
        if (progressBarDocument != null) progressBarDocument.gameObject.SetActive(true); // Show progress bar initially
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;

        InitializeHearts(3); // Initialize with the maximum number of lives

        // Actualizar el texto de distancia al inicio
        UpdateDistanceText(PlayerPrefs.GetFloat("TotalDistance", 0f));

        // Asignar el método al botón de reset en el Inspector
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetDistance);
        }
    }

    void OnEnable()
    {
        // Actualizar el texto de distancia al abrir el menú de configuraciones
        UpdateDistanceText(PlayerPrefs.GetFloat("TotalDistance", 0f));
    }

    void OnGameStateChanged(GAME_STATE _newGameState)
    {
        Debug.Log("Game state changed: " + _newGameState);
    }

    public void InitializeHearts(int lives)
    {
        foreach (Transform child in livesContainer)
        {
            Destroy(child.gameObject);
        }

        hearts.Clear();

        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, livesContainer);
            hearts.Add(heart);
        }
    }

    public void RemoveHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject heart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(heart); // Destruye el corazón

            // Instancia la botella rota en la misma posición que el corazón destruido
            GameObject brokenBottle = Instantiate(brokenBottlePrefab, heart.transform.position, heart.transform.rotation, livesContainer);
        }
    }

    // Show the Game Over panel and hide the pause button and progress bar
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }

        if (tutorialPanel != null)
        {
            tutorialPanel.gameObject.SetActive(false);
        }

        if (progressBarDocument != null)
        {
            progressBarDocument.gameObject.SetActive(false);
        }
    }

    // Actualizar el texto de distancia total en el menú de configuraciones
    public void UpdateDistanceText(float distance)
    {
        if (distanceText != null)
        {
            distanceText.text = "Distancia Total: " + distance.ToString("F2") + " metros";
        }
    }

    // Método para reiniciar la distancia total
    public void ResetDistance()
    {
        PlayerPrefs.SetFloat("TotalDistance", 0f);
        PlayerPrefs.Save();
        UpdateDistanceText(0f);
        Debug.Log("Distancia total reiniciada.");
    }
}
