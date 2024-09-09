using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Destruible : MonoBehaviour
{
    public int vida;
    int vidaOriginal;

    public UnityEvent MuerteEvent;
    private void Start()
    {
        vidaOriginal = vida;
    }

    public void RecuperarVida(int masVida)
    {
        vida+= masVida;
        if(vida > vidaOriginal)
        {
            vida = vidaOriginal;
        }
    }
    public void RecibirDanio(int danio)
    {
        vida-=danio;
        if(vida <= 0)
        {
            Morir();
        }
    }

    public void Morir()
    {
        print("Se muere");
        MuerteEvent.Invoke();
    }
}
