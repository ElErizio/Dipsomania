using UnityEngine;

public class _PlayerSkin : MonoBehaviour
{
    public bool isBasico, isNegro, isRubio, isJoker;
    public Material[] skins;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Awake()
    {
        // Obtener el componente SkinnedMeshRenderer del GameObject
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("No se encontró un Skinned Mesh Renderer en el GameObject.");
        }
    }

    void onSkinChanged(SKIN_SELECTED _skin)
    {
        if (skinnedMeshRenderer == null) return;

        switch (_skin)
        {
            case SKIN_SELECTED.BASICO:
                skinnedMeshRenderer.material = skins[0];
                break;
            case SKIN_SELECTED.NEGRO:
                skinnedMeshRenderer.material = skins[1];
                break;
            case SKIN_SELECTED.RUBIO:
                skinnedMeshRenderer.material = skins[2];
                break;
            case SKIN_SELECTED.JOKER:
                skinnedMeshRenderer.material = skins[3];
                break;
            default:
                Debug.LogWarning("Skin seleccionada no válida.");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().onSkinChanged += onSkinChanged;
        onSkinChanged(GameManager.GetInstance().currentSkin);
    }
}
