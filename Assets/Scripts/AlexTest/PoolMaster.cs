using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolMaster : MonoBehaviour
{
    #region Singleton
    static PoolMaster instance;

    public static PoolMaster GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    #endregion


    public enum OBJECT_TO_SPAWN
    {
        Edificio,
        Arbol,
        Piso,
        Tile,
        Carro,
        CasaFinal
    }


    List<GameObject> tilePool = new List<GameObject>();
    public GameObject tilePF;

    List<GameObject> edificiosPool = new List<GameObject>();
    public List<GameObject> edificiosPF = new List<GameObject>();

    List<GameObject> arbolPool = new List<GameObject>();
    public List<GameObject> arbolPF = new List<GameObject>();

    List<GameObject> pisoPool = new List<GameObject>();
    public List<GameObject> pisoPF = new List<GameObject>();

    List<GameObject> carroPool = new List<GameObject>();
    public List<GameObject> carroPF = new List<GameObject>();

    List<GameObject> casafPool = new List<GameObject>();
    public List<GameObject> casafPF = new List<GameObject>();


    private void Start()
    {
        edificiosPool.Clear();
        arbolPool.Clear();
    }
    public GameObject GetObjectFromPool(OBJECT_TO_SPAWN obj)
    {
        switch (obj)
        {
            case OBJECT_TO_SPAWN.Edificio: return GetEdificioToSpawn();
            case OBJECT_TO_SPAWN.Arbol: return GetArbolToSpawn();
            case OBJECT_TO_SPAWN.Piso: return GetPisoToSpawn();
            case OBJECT_TO_SPAWN.Tile: return GetTileToSpawn();
            case OBJECT_TO_SPAWN.Carro: return GetCarroToSpawn();
            case OBJECT_TO_SPAWN.CasaFinal: return GetCasaFinalToSpawn();
        }
        return null;
    }
    public GameObject GetEdificioToSpawn()
    {
        foreach (GameObject edificio in edificiosPool)
        {
            if (edificio.gameObject.activeSelf == false)
            {
                return edificio;
            }
        }

        GameObject newEdificio = Instantiate(edificiosPF[Random.Range(0,edificiosPF.Count)], new Vector3(-100,-100,-100), Quaternion.identity, transform);
        edificiosPool.Add(newEdificio);
        return newEdificio;
    }

    public GameObject GetArbolToSpawn()
    {
        foreach (GameObject arbol in arbolPool)
        {
            if (arbol.gameObject.activeSelf == false)
            {
                return arbol;
            }
        }

        GameObject newArbol = Instantiate(arbolPF[Random.Range(0, arbolPF.Count)], new Vector3(-100, -100, -100), Quaternion.identity, transform);
        arbolPool.Add(newArbol);
        return newArbol;
    }

    public GameObject GetPisoToSpawn()
    {
        foreach (GameObject piso in pisoPool)
        {
            if (piso.gameObject.activeSelf == false)
            {
                return piso;
            }
        }

        GameObject newPiso = Instantiate(pisoPF[Random.Range(0, pisoPF.Count)], new Vector3(-100, -100, -100), Quaternion.identity, transform);
        pisoPool.Add(newPiso);
        return newPiso;
    }

    public GameObject GetTileToSpawn()
    {
        foreach (GameObject tile in tilePool)
        {
            if (tile.gameObject.activeSelf == false)
            {
                return tile;
            }
        }

        GameObject newTile = Instantiate(tilePF, new Vector3(-100, -100, -100), Quaternion.identity, transform);
        tilePool.Add(newTile);
        return newTile;
    }

    public GameObject GetCarroToSpawn()
    {
        foreach (GameObject carro in carroPool)
        {
            if (carro.gameObject.activeSelf == false)
            {
                return carro;
            }
        }

        GameObject newCarro = Instantiate(carroPF[Random.Range(0, carroPF.Count)], new Vector3(-100, -100, -100), Quaternion.identity, transform);
        carroPool.Add(newCarro);
        return newCarro;
    }

    public GameObject GetCasaFinalToSpawn()
    {
        foreach (GameObject casaF in casafPool)
        {
            if (casaF.gameObject.activeSelf == false)
            {
                return casaF;
            }
        }

        GameObject newCasaFinal = Instantiate(casafPF[Random.Range(0, casafPF.Count)], new Vector3(-100, -100, -100), Quaternion.identity, transform);
        casafPool.Add(newCasaFinal);
        return newCasaFinal;
    }
}
