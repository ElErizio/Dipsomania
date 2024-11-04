using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_Manager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject heartPrefab; // Prefab del corazón
    public Transform livesContainer; // Contenedor de corazones en el Canvas

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        pauseScreen.SetActive(false);
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;

        InitializeHearts(3); // Cambia el número de vidas inicial si es necesario
    }

    void OnGameStateChanged(GAME_STATE _newGameState)
    {
        pauseScreen.SetActive(_newGameState == GAME_STATE.PAUSE);

        Debug.Log("game state changed: " + _newGameState);
    }

    // Inicializa los corazones al inicio del juego
    public void InitializeHearts(int lives)
    {
        // Limpiar corazones existentes en el contenedor
        foreach (Transform child in livesContainer)
        {
            Destroy(child.gameObject);
        }

        hearts.Clear();

        // Crear corazones en el contenedor
        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, livesContainer);
            hearts.Add(heart);
        }
    }

    // Llama a esta función para quitar una vida (corazón)
    public void RemoveHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject heart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            heart.SetActive(false); // Esconde el corazón en lugar de destruirlo
        }
    }
}
