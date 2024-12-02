using UnityEngine;

public class SkinShopManager : MonoBehaviour
{
    public static SkinShopManager Instance { get; private set; }

    [Header("Prefab del personaje")]
    public GameObject characterPrefab;

    [Header("Skins disponibles")]
    public Material[] skins;

    private const string SkinKey = "SelectedSkin";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evitar m�ltiples instancias
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persistir entre escenas
    }

    // M�todo para cargar la skin guardada
    public void LoadPrefabSkin()
    {
        if (PlayerPrefs.HasKey(SkinKey))
        {
            int savedSkinIndex = PlayerPrefs.GetInt(SkinKey);

            if (savedSkinIndex >= 0 && savedSkinIndex < skins.Length)
            {
                ApplySkin(savedSkinIndex);
            }
        }
        else
        {
            Debug.Log("No hay skin guardada, usando la skin predeterminada.");
        }
    }

    // M�todo para cambiar la skin con un �ndice
    public void ChangeSkin(int skinIndex)
    {
        if (skinIndex >= 0 && skinIndex < skins.Length)
        {
            ApplySkin(skinIndex);

            // Guardar el �ndice de la skin seleccionada
            PlayerPrefs.SetInt(SkinKey, skinIndex);
            PlayerPrefs.Save();

            Debug.Log($"Skin cambiada y guardada: {skins[skinIndex].name}");
        }
        else
        {
            Debug.LogError("�ndice de skin inv�lido.");
        }
    }

    // Aplicar la skin al prefab
    private void ApplySkin(int skinIndex)
    {
        MeshRenderer prefabMeshRenderer = characterPrefab.GetComponentInChildren<MeshRenderer>();
        if (prefabMeshRenderer != null)
        {
            prefabMeshRenderer.material = skins[skinIndex];
            Debug.Log($"Skin aplicada: {skins[skinIndex].name}");
        }
        else
        {
            Debug.LogError("El prefab no tiene un Mesh Renderer.");
        }
    }
}