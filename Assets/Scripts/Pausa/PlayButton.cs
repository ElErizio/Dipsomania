using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con UI

public class PlayButton : MonoBehaviour
{
    public Button playButton;  // Referencia al bot�n de "Play"

    void Start()
    {
        // Aseg�rate de asignar la funci�n al bot�n
        playButton.onClick.AddListener(ResumeGame);
    }

    void ResumeGame()
    {
        // Cambia el estado del juego a PLAY cuando el bot�n es presionado
        GameManager.GetInstance().ChangeGameState(GAME_STATE.PLAY);
    }
}