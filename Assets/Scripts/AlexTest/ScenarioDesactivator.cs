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

        // Notificar al CustomProgressBarController que un tile ha sido "destruido"
        if (progressBarController != null)
        {
            progressBarController.IncrementProgress();
        }
        else
        {
            Debug.LogError("progressBarController no está asignado en ScenarioDesactivator.");
        }

    }
}
