using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float forwardSpeed = 5f;   
    public float tiltSensitivity = 2f;

    private Rigidbody rb;

    bool isOnPlay;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);
    }

    void Update()
    {
        if (isOnPlay)
        {
            Vector3 forwardMovement = transform.forward * forwardSpeed * Time.deltaTime;

            float tilt = Input.acceleration.x;
            Vector3 lateralMovement = transform.right * tilt * tiltSensitivity * Time.deltaTime;

            Vector3 movement = forwardMovement + lateralMovement;

            rb.MovePosition(rb.position + movement);
        }
    }

    void OnGameStateChanged(GAME_STATE _gs)
    {
        isOnPlay = _gs == GAME_STATE.PLAY;
    }
}
