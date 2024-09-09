using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destruible))]
public class Player : MonoBehaviour
{
    Destruible destruible;
    private void Start()
    {
        destruible = GetComponent<Destruible>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            print("Me pegó");
            destruible.RecibirDanio(1);
        }
    }
}
