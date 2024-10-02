using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destruible))]
public class Player : MonoBehaviour
{

    bool isOnPlay;
    Destruible destruible;
    Rigidbody rb;

    public string invulnerableLMName;
    public float invulnerableTime;
    bool isInvulnerable;
    LayerMask defaultLM;
    private void Start()
    {
        defaultLM = gameObject.layer;
        destruible = GetComponent<Destruible>();
        rb = GetComponent<Rigidbody>();
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isOnPlay)
        {
            if (!isInvulnerable && collision.gameObject.CompareTag("Obstacle"))
            {
                print("Me pegó");
                try
                {
                    LayerMask.NameToLayer(invulnerableLMName);
                    StartCoroutine(Invulnerabilidad());
                }
                catch
                {
                    print("Layer mask no encontrado, no funciona la invensibilidad");
                }
                destruible.RecibirDanio(1);
            }
        }
    }


    void OnGameStateChanged(GAME_STATE _gs)
    {
        isOnPlay = _gs == GAME_STATE.PLAY;

        if (!isOnPlay)
        {
            rb.velocity = Vector3.zero;
            
        }
    }

    IEnumerator Invulnerabilidad()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.layer = LayerMask.NameToLayer(invulnerableLMName);
        yield return new WaitForSeconds(invulnerableTime);
        gameObject.layer = defaultLM;
        isInvulnerable = false;


    }
}
