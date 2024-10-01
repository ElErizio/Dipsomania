using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioGenerator : MonoBehaviour
{

    GAME_STATE gameState;

    public GameObject groundTile;
    public LayerMask mask;
    public int tileWith;
    public int defaultTilesCount;
    public Vector3 startPos;
    public Transform groundChecker;
    public GameObject destroyer;
    bool isGrounding;
    bool startSpawning,spawn;
    Vector3 nextSpawmPoint;

    private void Start()
    {
        transform.position = startPos;
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, tileWith);
        //nextSpawmPoint = transform.position - new Vector3(0, 0, tileWith);
        //print(nextSpawmPoint);
        groundChecker.position = transform.position + new Vector3(0, 0, (-tileWith / 2));
        print(groundChecker.position);
        destroyer.transform.position = transform.position - new Vector3(0, 0, tileWith * defaultTilesCount + defaultTilesCount);
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        SpawnDefault();
        startSpawning = true;
    }

    private void SpawnDefault()
    {
        for (int i = 0; i < defaultTilesCount-1; i++)
        {
            SpawnTile();
            transform.localPosition += new Vector3(0, 0, tileWith);
        }
        SpawnTile();
    }

    private void Update()
    {
        if(gameState == GAME_STATE.PLAY && startSpawning)
        {
            isGrounding = Physics.CheckSphere(groundChecker.position, 0.01f, mask);
            if (!isGrounding && spawn == false)
            {
                spawn = true;
                SpawnTile();
            }
        }
    }
    void SpawnTile()
    {
        nextSpawmPoint = new Vector3(0, 0, RoundToNearestMultiple(transform.position.z));
        //GameObject newTile = Instantiate(groundTile, nextSpawmPoint, Quaternion.identity);
        GameObject newTile = PoolMaster.GetInstance().GetTileToSpawn();
        newTile.transform.parent = null;
        newTile.transform.position = nextSpawmPoint;
        newTile.SetActive(true);
        newTile.GetComponent<Tile>().Inicializar();
        spawn = false;
    }

    public float RoundToNearestMultiple(float number)
    {
        //print(number + " / " + tileWith + " / " + Mathf.Round(number / tileWith) * tileWith);
        return Mathf.Round(number / tileWith) * tileWith;
    }
    void OnGameStateChanged(GAME_STATE _newGameState)
    {
        gameState = _newGameState;
    }
}
