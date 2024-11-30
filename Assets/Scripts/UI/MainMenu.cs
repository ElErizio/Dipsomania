using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Rafa Test");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}