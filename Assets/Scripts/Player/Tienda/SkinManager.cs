using UnityEngine;

[CreateAssetMenu(fileName = "SkinManager", menuName = "Skin Manager")]
public class SkinManager : ScriptableObject
{
    [SerializeField] public Skin[] skins;
    private const string Prefix = "Skin_";
    private const string SelectedSkin = "SelectedSkin";

    public Skin OnSelectedSkin()
    {
        // Para saber la skin que fue seleccionada, se toma el PlayerPref
        int skinIndex = PlayerPrefs.GetInt(SelectedSkin, 0);

        // Si la skin que se selecciono se encuentra en el indice va a seleccionarla
        if (skinIndex >= 0 && skinIndex < skins.Length) return skins[skinIndex];
        else return null;
    }

    /// <summary>
    /// Funcion para seleccionar skin, aqui se asignan los PlayerPrefs de SelectedSkin y un indice skinIndex para saber que skin es
    /// </summary>
    public void SelectSkin(int skinIndex) => PlayerPrefs.SetInt(SelectedSkin, skinIndex);

    /// <summary>
    /// Funcion para desbloquear la skin correspondiente, se asigna los PlayerPrefs a skinIndex al comprar la skin, se pone 1 como referencia a un booleano
    /// </summary>
    public void UnlockSkin(int skinIndex) => PlayerPrefs.SetInt(Prefix + skinIndex, 1);

    /// <summary>
    /// Funcion para saber si la skin esta desbloqueada, regresa un booleano y comprueba que los PlayerPrefs de skinIndex sean 1, de lo contrario no esta desbloqueada
    /// </summary>
    public bool IsSkinUnlocked(int skinIndex) => PlayerPrefs.GetInt(Prefix + skinIndex, 0) == 1;
}
