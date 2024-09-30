using UnityEngine;
using UnityEngine.UI; 

public class PauseButton : MonoBehaviour
{
    public Button pauseButton;  

    void Start()
    {
        
        pauseButton.onClick.AddListener(PauseGame);
    }

    void PauseGame()
    {
        GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE);
    }
}