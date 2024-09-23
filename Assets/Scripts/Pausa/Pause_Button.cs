using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con UI

public class PauseButton : MonoBehaviour
{
    public Button pauseButton;  // Referencia al botón de pausa

    void Start()
    {
        // Asegúrate de asignar la función al botón
        pauseButton.onClick.AddListener(PauseGame);
    }

    void PauseGame()
    {
        // Cambia el estado del juego a PAUSE cuando el botón es presionado
        GameManager.GetInstance().ChangeGameState(GAME_STATE.PAUSE);
    }
}