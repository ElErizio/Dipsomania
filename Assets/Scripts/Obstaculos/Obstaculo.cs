using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public float TiempoDeVida = 12f;
    public void Inicializar()
    {
        //print ("Se inicializa " + gameObject.name);
        StartCoroutine(Suicide());
    }

    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(TiempoDeVida);
        gameObject.SetActive(false);
    }
}
