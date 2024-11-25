using UnityEngine;

public class SkinShopManager : MonoBehaviour
{
    [Header("Prefab del personaje")]
    public GameObject characterPrefab; // Asigna el prefab del personaje aquí.

    [Header("Skins disponibles")]
    public Material[] skins; // Lista de materiales disponibles.

    private int currentSkinIndex = 0; // Índice de la skin seleccionada.

    // Método para cambiar el material del prefab
    public void ChangePrefabSkin(int skinIndex)
    {
        if (skinIndex >= 0 && skinIndex < skins.Length)
        {
            // Cargar el prefab original
            MeshRenderer prefabMeshRenderer = characterPrefab.GetComponentInChildren<MeshRenderer>();

            if (prefabMeshRenderer != null)
            {
                prefabMeshRenderer.material = skins[skinIndex];
                currentSkinIndex = skinIndex;

                // Guardar el cambio en PlayerPrefs (opcional)
                SaveSkinSelection();

                Debug.Log($"Material del prefab cambiado a: {skins[skinIndex].name}");
            }
            else
            {
                Debug.LogError("El prefab no tiene un Mesh Renderer.");
            }
        }
        else
        {
            Debug.LogError("Índice de skin inválido.");
        }
    }

    // Opcional: Guardar la skin seleccionada
    public void SaveSkinSelection()
    {
        PlayerPrefs.SetInt("SelectedSkin", currentSkinIndex);
        PlayerPrefs.Save();
        Debug.Log("Skin seleccionada guardada.");
    }

    // Opcional: Cargar la skin seleccionada al iniciar el juego
    public void LoadSkinSelection()
    {
        if (PlayerPrefs.HasKey("SelectedSkin"))
        {
            int savedSkinIndex = PlayerPrefs.GetInt("SelectedSkin");
            ChangePrefabSkin(savedSkinIndex);
        }
    }
}
