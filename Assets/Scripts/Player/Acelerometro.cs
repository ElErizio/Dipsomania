using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acelerometro : MonoBehaviour
{

    bool isOnPlay;
    private Rigidbody rb;
    private float speed = 2.0f;

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
            Vector3 tilt = new Vector3(Input.acceleration.x, 0, 0);
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
            rb.AddForce(tilt * speed);
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0), ForceMode.Impulse);
        }

    }

    void OnGameStateChanged(GAME_STATE _gs)
    {
        isOnPlay = _gs == GAME_STATE.PLAY;
    }
}
