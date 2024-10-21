using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public PoolMaster.OBJECT_TO_SPAWN objectToSpawn;

    [Range(0, 100)]
    public int spawnPercentage = 100;
    private void Start()
    {
        //Spawn();
    }

    public void Spawn()
    {
        int random = Random.Range(0, 100);
        if (random <= spawnPercentage) {
            GameObject newObj = PoolMaster.GetInstance().GetObjectFromPool(objectToSpawn);
            newObj.transform.parent = transform;
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localRotation = Quaternion.identity;
            newObj.gameObject.SetActive(true);
        }
    }
}
