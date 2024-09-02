using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    List<Obstaculo> obstaculos = new List<Obstaculo>();
    public GameObject PFObstaculo;
    public Vector3 spawnPosMin;
    public Vector3 spawnPosMax;
    public float spawnRate;
    float timeSinceLastSpawn;
    
    void Update()
    {
        if(timeSinceLastSpawn > 0)
        {
            timeSinceLastSpawn-=Time.deltaTime;
        }
        else
        {
            timeSinceLastSpawn = spawnRate;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        Obstaculo obs = GetObstacleToSpawn();
        obs.transform.position = GetRandomSpawnPos();
        obs.gameObject.SetActive(true);
        obs.Inicializar();
    }

    Obstaculo GetObstacleToSpawn()
    {
        foreach (Obstaculo obstaculo in obstaculos)
        {
            if(obstaculo.gameObject.activeSelf == false)
            {
                return obstaculo;
            }
        }

        Obstaculo obs = Instantiate(PFObstaculo, GetRandomSpawnPos(), Quaternion.identity, transform).GetComponent<Obstaculo>();
        obstaculos.Add(obs);
        return obs;
    }

    Vector3 GetRandomSpawnPos()
    {
        return new Vector3(Random.Range(spawnPosMin.x, spawnPosMax.x), Random.Range(spawnPosMin.y, spawnPosMax.y), Random.Range(spawnPosMin.z, spawnPosMax.z));
    }
}
