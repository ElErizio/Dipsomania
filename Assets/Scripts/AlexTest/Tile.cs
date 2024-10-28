using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    List<RandomSpawner> mySpawners = new List<RandomSpawner>();

    private void Start()
    {
        GetChildSpawner();
    }

    private void GetChildSpawner()
    {
        mySpawners = GetComponentsInChildren<RandomSpawner>().ToList<RandomSpawner>();
    }

    public void Inicializar()
    {
        if (mySpawners.Count == 0)
        {
            GetChildSpawner();
        }

        foreach (RandomSpawner spawner in mySpawners)
        {
            spawner.Spawn();
        }
    }
}