using UnityEngine;
using Cinemachine;

public class OscillatingCameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float oscillationIntensity = 0.05f;
    public float oscillationSpeed = 5f;
    public Transform player; // El jugador

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
        virtualCamera.Follow = player;  // Asignamos el jugador como el objetivo de seguimiento
    }

    void Update()
    {
        // Oscilación de la cámara para simular el mareo
        float offsetX = Mathf.Sin(Time.time * oscillationSpeed) * oscillationIntensity;
        float offsetY = Mathf.Cos(Time.time * oscillationSpeed) * oscillationIntensity;

        transform.localPosition = initialPosition + new Vector3(offsetX, offsetY, 0);
    }
}
