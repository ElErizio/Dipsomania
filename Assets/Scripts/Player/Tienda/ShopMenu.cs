using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [Header("Store Fields and managers")]
    // [SerializeField] private Image selectedSkin;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private SkinManager skinManager;

    void Update()
    {
        int totalDistance = Mathf.FloorToInt(PlayerPrefs.GetFloat("TotalDistance"));

        // money.text = "Puntos: " + PlayerPrefs.GetFloat("TotalDistance");
        money.text = "Puntos: " + totalDistance;
        // selectedSkin.sprite = skinManager.OnSelectedSkin().skinImage;
    }

    private void Start()
    {
       // PlayerPrefs.DeleteAll();
    }

    //public void ExitShop() => SceneManager.LoadScene(sceneName);
}