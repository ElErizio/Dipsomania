using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject pauseScreen;
    void Start()
    {
        pauseScreen.SetActive(false);
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
    }

    void OnGameStateChanged(GAME_STATE _newGameState)
    {
        pauseScreen.SetActive(_newGameState == GAME_STATE.PAUSE);

        Debug.Log("game state changed: " + _newGameState);
        /* if (_newGameState == GAME_STATE.PAUSE)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }*/
    }
}