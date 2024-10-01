using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public PoolMaster.OBJECT_TO_SPAWN objectToSpawn;
    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        GameObject newObj = PoolMaster.GetInstance().GetObjectFromPool(objectToSpawn);
        newObj.transform.parent = transform;
        newObj.transform.localPosition = Vector3.zero;
        newObj.gameObject.SetActive(true);
    }
}
