using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string TutorialFinishedKey = "TutorialFinished";

    void Start()
    {
        // Check if the tutorial is already completed
        if (PlayerPrefs.HasKey(TutorialFinishedKey))
        {
            // If tutorial is finished, load the main menu
            SceneManager.LoadScene("MainMenuScene"); // Replace with your main menu scene name
        }
        else
        {
            // If not finished, load the tutorial
            SceneManager.LoadScene("TutorialScene"); // Replace with your tutorial scene name
        }
    }
}