using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioDesactivator : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        other.transform.parent = transform;

    }
}
