using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("Shop parameters")]
    [SerializeField] private SkinManager skinManager;
    [SerializeField] private Button purchase;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Button seleccionar;

    [Header("Skin parameters")]
    [SerializeField] private int skinIndex;

    private Skin skin;
    void Start()
    {
        skin = skinManager.skins[skinIndex];

        GetComponent<Image>().sprite = skin.skinImage;

        if (skinManager.IsSkinUnlocked(skinIndex))
        {
            purchase.gameObject.SetActive(false);
            seleccionar.gameObject.SetActive(true);
        }
        else
        {
            cost.text = skin.price.ToString();
            purchase.gameObject.SetActive(true);
            seleccionar.gameObject.SetActive(false);
        }
    }

    public void PurchaseSkin()
    {
        float money = PlayerPrefs.GetFloat("TotalDistance", 0); //A cambiar a tus PlayerPrefs referentes a la distancia

        if (money >= skin.price && !skinManager.IsSkinUnlocked(skinIndex))
        {
            PlayerPrefs.SetFloat("TotalDistance", money - skin.price);
            skinManager.UnlockSkin(skinIndex);
            purchase.gameObject.SetActive(false);
            seleccionar.gameObject.SetActive(true);
            skinManager.SelectSkin(skinIndex);
        }
        else Debug.Log("Not enough Distance");
    }

    public void SelectSkin()
    {
        if (skinManager.IsSkinUnlocked(skinIndex)) skinManager.SelectSkin(skinIndex);
    }
}
