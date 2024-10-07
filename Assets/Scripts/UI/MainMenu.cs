using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar las escenas

public class MainMenu : MonoBehaviour
{
    // Este método se ejecuta cuando presionas el botón Play
    public void PlayGame()
    {
        SceneManager.LoadScene("Test_Lvl1_V2"); // Cambia "Lvl1" por el nombre exacto de tu escena
    }
}