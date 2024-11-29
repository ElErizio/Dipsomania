using UnityEngine;
using UnityEngine.Events;

public class Destruible : MonoBehaviour
{
    public int vida;
    private int vidaOriginal;

    public GameObject gameOverPanel;

    public UnityEvent MuerteEvent;

    private void Start()
    {
        vidaOriginal = vida;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (vida <= 0)
        {
            Morir();
        }
    }

    public void RecibirDanio(int danio)
    {
        vida -= danio;
    }

    public void Morir()
    {
        GameManager.GetInstance().ChangeGameState(GAME_STATE.GAME_OVER);

        MuerteEvent?.Invoke();
    }

    public void RecuperarVida(int cantidad)
    {
        vida += cantidad;
        if (vida > vidaOriginal)
        {
            vida = vidaOriginal;
        }
    }
}