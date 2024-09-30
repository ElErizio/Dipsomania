using UnityEngine;
using UnityEngine.UI; 

public class PlayButton : MonoBehaviour
{
    public Button playButton;  

    void Start()
    {
        playButton.onClick.AddListener(ResumeGame);
    }

    void ResumeGame()
    {
        GameManager.GetInstance().ChangeGameState(GAME_STATE.PLAY);
    }
}