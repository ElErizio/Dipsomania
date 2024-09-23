using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> spwnObjs = new List<GameObject>();
    private void Start()
    {
        GameObject obj = spwnObjs[Random.Range(0, spwnObjs.Count - 1)];
        Instantiate(obj,transform.position,Quaternion.identity,transform);
    }
}
