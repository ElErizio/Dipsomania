using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    bool isOnPlay;
    public float speed = 5f;
    public Vector3 direction;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Verifica que direction no sea (0, 0, 0)
        if (direction == Vector3.zero)
        {
            direction = Vector3.forward; // Dirección predeterminada
        }

        direction = direction.normalized;

        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);
    }

    void FixedUpdate()
    {
        if (isOnPlay)
        {
            Movement(); // Mueve el objeto si el juego está en "PLAY"
        }
    }

    private void Movement()
    {
        //rb.AddForce(direction*speed, ForceMode.VelocityChange);
        rb.velocity = direction * speed;
    }

    void OnGameStateChanged(GAME_STATE _gs)
    {
        isOnPlay = _gs == GAME_STATE.PLAY;
    }
}
