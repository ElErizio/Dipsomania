using UnityEngine;

public class Acelerometro2 : MonoBehaviour
{
    bool isOnPlay;
    private Rigidbody rb;
    private float speed = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Suscribirse al evento de cambio de estado del juego.
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);
    }

    void Update()
    {
        if (isOnPlay)
        {
            // Obtener los valores del aceler�metro
            Vector3 tilt = new Vector3(Input.acceleration.x, 0, Input.acceleration.y); // Utiliza el eje X e Y para movimiento en X y Z
            tilt = Vector3.ClampMagnitude(tilt, 1); // Limita la magnitud para evitar valores muy grandes

            // Aplicar fuerza en el Rigidbody
            rb.velocity = new Vector3(tilt.x * speed, rb.velocity.y, tilt.z * speed); // Controla la velocidad en lugar de a�adir fuerza constantemente

            // Tambi�n puedes agregar controles manuales si deseas combinar ambos m�todos
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            rb.velocity += new Vector3(horizontal * speed, 0, vertical * speed); // Combina controles de aceler�metro y manuales
        }
    }

    void OnGameStateChanged(GAME_STATE _gs)
    {
        // Actualizar estado para verificar si se est� jugando
        isOnPlay = _gs == GAME_STATE.PLAY;
    }
}
