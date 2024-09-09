using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    bool isOnPlay;
    public float speed = 5f;
    public Vector3 direction;

    private void Start()
    {
        direction = direction.normalized;
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);
    }

    void Update()
    {
        // Si el juego est� en estado de "PLAY", se ejecuta el movimiento
        if (isOnPlay)
        {
            Movement(); // Mueve el objeto si el juego est� en "PLAY"
        }
        // Si est� en "PAUSE" o cualquier otro estado, no se ejecuta nada
    }

    private void Movement()
    {
        // M�todo para activar el movimiento de los obst�culos
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnGameStateChanged(GAME_STATE _gs)
    {
        // Cambia el estado de isOnPlay dependiendo del estado del juego
        isOnPlay = _gs == GAME_STATE.PLAY;
    }
}
