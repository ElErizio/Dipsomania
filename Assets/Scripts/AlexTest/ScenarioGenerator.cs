using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioGenerator : MonoBehaviour
{

    GAME_STATE gameState;

    public GameObject groundTile;
    public LayerMask mask;
    public int tileWith;
    public Transform groundChecker;
    public GameObject destroyer;
    bool isGrounding;
    bool spawn;
    Vector3 nextSpawmPoint;

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, tileWith);
        nextSpawmPoint = transform.position - new Vector3(0, 0, tileWith);
        print(nextSpawmPoint);
        groundChecker.position = transform.position + new Vector3(0, 0, (-tileWith / 2)+ 0.09f);
        destroyer.transform.position = transform.position - new Vector3(0, 0, tileWith*3);
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
    }

    private void Update()
    {
        if(gameState == GAME_STATE.PLAY)
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
        GameObject newTile = Instantiate(groundTile, nextSpawmPoint, Quaternion.identity);
        spawn = false;
    }

    public float RoundToNearestMultiple(float number)
    {
        print(number + " / " + tileWith + " / " + Mathf.Round(number / tileWith) * tileWith);
        return Mathf.Round(number / tileWith) * tileWith;
    }
    void OnGameStateChanged(GAME_STATE _newGameState)
    {
        gameState = _newGameState;
    }
}
