using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void OnExitButtonClicked()
    {
        GameManager.GetInstance().ChangeGameState(GAME_STATE.MENU);

        SceneManager.LoadScene("MainMenu");
    }

    public void OnPlayButtonClicked()
    {
        // Al regresar al juego, recarga la última escena jugada
        SceneManager.LoadScene("Rafa Test");
    }
}