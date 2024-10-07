using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("Test_Lvl1_V2");
    }
}