using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioDesactivator : MonoBehaviour
{
    public CustomProgressBarController progressBarController; // Referencia al controlador de la barra de progreso
    private void OnTriggerEnter(Collider other)
    {
        // Desactivar el tile
        other.gameObject.SetActive(false);
        other.transform.parent = transform;

        

    }
}
