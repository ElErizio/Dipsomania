using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class UI_Manager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform livesContainer;
    public GameObject gameOverPanel; // Reference to the Game Over panel
    public GameObject pauseButton; // Reference to the Pause button on the Canvas
    public UIDocument progressBarDocument; // Reference to the progress bar UIDocument

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        gameOverPanel.SetActive(false); // Hide Game Over panel at the start
        if (pauseButton != null) pauseButton.SetActive(true); // Show pause button initially
        if (progressBarDocument != null) progressBarDocument.gameObject.SetActive(true); // Show progress bar initially
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;

        InitializeHearts(3); // Initialize with the maximum number of lives
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
            heart.SetActive(false);
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
            pauseButton.SetActive(false); // Hide the pause button
        }

        if (progressBarDocument != null)
        {
            progressBarDocument.gameObject.SetActive(false); // Hide the progress bar
        }
    }
}
