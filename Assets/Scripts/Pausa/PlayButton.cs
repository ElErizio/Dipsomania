using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con UI

public class PlayButton : MonoBehaviour
{
    public Button playButton;  // Referencia al botón de "Play"

    void Start()
    {
        // Asegúrate de asignar la función al botón
        playButton.onClick.AddListener(ResumeGame);
    }

    void ResumeGame()
    {
        // Cambia el estado del juego a PLAY cuando el botón es presionado
        GameManager.GetInstance().ChangeGameState(GAME_STATE.PLAY);
    }
}