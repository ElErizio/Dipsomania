using UnityEngine;

public class _PlayerSkin : MonoBehaviour
{
    [SerializeField] private SkinManager skinManager;
    void Start()
    {
        GetComponent<SkinnedMeshRenderer>().material = skinManager.OnSelectedSkin().skinMaterial;
    }
}
