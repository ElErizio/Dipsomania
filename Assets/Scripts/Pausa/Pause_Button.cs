using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con UI

public class PauseButton : MonoBehaviour
{
    public Button pauseButton;  // Referencia al bot�n de pausa

    void Start()
    {
        // Aseg�rate de asignar la funci�n al bot�n
        pauseButton.onClick.AddListener(PauseGame);
    }

    void PauseGame()
    {
        // Cambia el estado del juego a PAUSE cuando el bot�n es presionado
        GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE);
    }
}