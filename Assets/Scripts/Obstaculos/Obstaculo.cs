using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public void Inicializar()
    {
        print ("Se inicializa " + gameObject.name);
        StartCoroutine(Suicide());
    }

    IEnumerator Suicide()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
