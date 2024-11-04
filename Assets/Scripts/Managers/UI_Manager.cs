using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_Manager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform livesContainer;

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;

        InitializeHearts(3);
    }

    void OnGameStateChanged(GAME_STATE _newGameState)
    {
        Debug.Log("game state changed: " + _newGameState);
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
}
