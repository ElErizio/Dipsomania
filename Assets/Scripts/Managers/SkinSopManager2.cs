using UnityEngine;

public class SkinShopManager2 : MonoBehaviour
{
    public static SkinShopManager2 Instance { get; private set; }

    [Header("Referencia del personaje en la escena")]
    public SkinnedMeshRenderer characterRenderer;

    [Header("Skins disponibles")]
    public Material[] skins;

    private const string SkinKey = "SelectedSkin";

    public int currentSkinIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evitar múltiples instancias
        }
        else 
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }       
    }

    // Método para cargar la skin guardada
    public void LoadSavedSkin()
    {
        if (PlayerPrefs.HasKey(SkinKey))
        {
            int savedSkinIndex = PlayerPrefs.GetInt(SkinKey);
            currentSkinIndex = savedSkinIndex;

            if (savedSkinIndex >= 0 && savedSkinIndex < skins.Length)
            {
                Debug.Log("La skin está cargada: " + savedSkinIndex);
                ApplySkin(savedSkinIndex);
            }
        }
        else
        {
            Debug.Log("No hay skin guardada, usando la skin predeterminada.");
        }
    }

    // Método para cambiar la skin con un índice
    public void ChangeSkin(int skinIndex)
    {
        if (skinIndex >= 0 && skinIndex < skins.Length)
        {
            currentSkinIndex = skinIndex;
            ApplySkin(skinIndex);

            // Guardar el índice de la skin seleccionada
            PlayerPrefs.SetInt(SkinKey, skinIndex);
            PlayerPrefs.Save();

            Debug.Log($"Skin cambiada y guardada: {skins[skinIndex].name}");
        }
        else
        {
            Debug.LogError("Índice de skin inválido.");
        }
    }

    // Aplicar la skin al objeto en la escena
    private void ApplySkin(int skinIndex)
    {
        if (characterRenderer != null)
        {
            characterRenderer.material = skins[skinIndex];
            Debug.Log($"Skin aplicada: {skins[skinIndex].name}");
        }
        else
        {
            Debug.LogError("El Skinned Mesh Renderer no está asignado.");
        }
    }
}
