using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProgressBars : MonoBehaviour
{
    public GameObject uiDocumentObject; // Arrastra el objeto con el componente UIDocument aquí
    private ProgressBar progressBar;
    private bool isGamePaused = false; // Bandera para verificar si el juego está en pausa

    // Número de ground tiles necesarios para completar el progreso
    public int totalGroundTiles = 30;
    private int currentGroundTiles = -10; // Contador de ground tiles generados
    public int tilesToIgnore = 10; // Número de ground tiles que se deben ignorar al inicio

    private bool progressStarted = false; // Controla si el progreso debe comenzar

    void Start()
    {
        Debug.Log("ProgressBars script Start() called.");

        // Suscribir al evento de cambio de estado del GameManager
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;

        // Encontrar el UIDocument en el objeto asignado
        var uidoc = uiDocumentObject.GetComponent<UIDocument>();
        if (uidoc == null)
        {
            Debug.LogError("UIDocument component not found on the specified GameObject.");
            return;
        }

        // Acceder al root VisualElement
        var root = uidoc.rootVisualElement;
        if (root == null)
        {
            Debug.LogError("Root VisualElement is null!");
            return;
        }

        Debug.Log("Root VisualElement obtained");

        // Buscar el ProgressBar en el UXML
        progressBar = root.Q<ProgressBar>("progress-bar"); // Buscar por nombre
        if (progressBar == null)
        {
            Debug.LogError("ProgressBar not found in the UXML file!");
            return;
        }

        Debug.Log("ProgressBar found");

        // Inicializar el progreso a 0 para asegurarse de que esté vacío al inicio
        progressBar.value = 0f;

        // Suscribir el evento para cuando se genere un nuevo ground tile
        GroundSpawner.OnTileSpawned += UpdateProgressBar;
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

    // Método para actualizar la barra de progreso cuando se genera un nuevo GroundTile
    void UpdateProgressBar()
    {
        if (!isGamePaused)
        {
            currentGroundTiles++; // Incrementar el contador de tiles

            // Ignorar las primeras 'tilesToIgnore' tiles generadas de golpe al inicio
            if (currentGroundTiles <= tilesToIgnore)
            {
                Debug.Log("Ignorando tile número: " + currentGroundTiles);
                return; // Ignorar las primeras tiles generadas
            }

            // Si ya hemos pasado los tiles a ignorar, iniciar el progreso
            if (currentGroundTiles > tilesToIgnore && !progressStarted)
            {
                progressStarted = true; // Iniciar el progreso después de las primeras 5 tiles
                Debug.Log("Iniciando progreso con el tile número: " + currentGroundTiles);
            }

            // Solo actualizar el progreso si ya hemos empezado
            if (progressStarted)
            {
                // Ajustar el número de tiles que realmente cuentan para el progreso
                int tilesCounted = currentGroundTiles - tilesToIgnore;

                // Calcular el progreso basado en los tiles restantes (después de las primeras 5)
                float progress = (float)tilesCounted / (totalGroundTiles - tilesToIgnore) * 100f;
                progressBar.value = Mathf.Clamp(progress, 0f, 100f); // Asegurarse de que no exceda el 100%
                Debug.Log("Progress: " + progressBar.value + "%");
            }
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento cuando este objeto sea destruido
        if (GameManager.GetInstance() != null)
        {
            GameManager.GetInstance().OnGameStateChanged -= OnGameStateChanged;
        }

        // Desuscribir el evento cuando este objeto se destruya
        GroundSpawner.OnTileSpawned -= UpdateProgressBar;
    }
}
