using UnityEngine;
using Cinemachine;

public class FOVOscillation : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float fovAmplitude = 5f;
    public float fovSpeed = 1f;

    private float initialFOV;

    void Start()
    {
        initialFOV = virtualCamera.m_Lens.FieldOfView;
    }

    void Update()
    {
        virtualCamera.m_Lens.FieldOfView = initialFOV + Mathf.Sin(Time.time * fovSpeed) * fovAmplitude;
    }
}
