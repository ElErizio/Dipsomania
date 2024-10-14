using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProgressBars : MonoBehaviour
{
    public GameObject uiDocumentObject; // Drag the GameObject with the UIDocument component into this field
    private ProgressBar progressBar;
    private bool isGamePaused = false; // Flag to check if the game is paused

    void Start()
    {
        Debug.Log("ProgressBars script Start() called.");

        // Suscribir al evento de cambio de estado del GameManager
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;

        // Find the UIDocument component on the assigned GameObject
        var uidoc = uiDocumentObject.GetComponent<UIDocument>();
        if (uidoc == null)
        {
            Debug.LogError("UIDocument component not found on the specified GameObject.");
            return;
        }

        // Access the root VisualElement
        var root = uidoc.rootVisualElement;
        if (root == null)
        {
            Debug.LogError("Root VisualElement is null!");
            return;
        }

        Debug.Log("Root VisualElement obtained");

        // Query the ProgressBar that is already in the UXML
        progressBar = root.Q<ProgressBar>("progress-bar"); // Query by name
        if (progressBar == null)
        {
            Debug.LogError("ProgressBar not found in the UXML file!");
            return;
        }

        Debug.Log("ProgressBar found");

        // Start updating the progress bar value
        progressBar.value = 0f;
        progressBar.schedule.Execute(() =>
        {
            if (!isGamePaused) // Solo actualizar si el juego no está en pausa
            {
                progressBar.value += 2f;
            }
        }).Every(75).Until(() => progressBar.value >= 100f);
    }

    // Método que se ejecuta cuando cambia el estado del juego
    void OnGameStateChanged(GAME_STATE newState)
    {
        if (newState == GAME_STATE.PAUSE)
        {
            isGamePaused = true; // Pausar el progreso
        }
        else if (newState == GAME_STATE.PLAY)
        {
            isGamePaused = false; // Reanudar el progreso
        }
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