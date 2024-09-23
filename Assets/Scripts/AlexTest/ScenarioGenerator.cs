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
    bool isGrounding;
    Vector3 nextSpawmPoint;

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, tileWith);
        nextSpawmPoint = transform.position - new Vector3(0, 0, tileWith);
        print(nextSpawmPoint);
        groundChecker.position = transform.position + new Vector3(0, 0, (-tileWith / 2)+ 0.09f);
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
    }

    private void Update()
    {
        if(gameState == GAME_STATE.PLAY)
        {
            isGrounding = Physics.CheckSphere(groundChecker.position, 0.01f, mask);
            if (!isGrounding)
            {
                SpawnTile();
            }
        }
    }
    void SpawnTile()
    {
        GameObject newTile = Instantiate(groundTile, nextSpawmPoint, Quaternion.identity);
        nextSpawmPoint = new Vector3(0, 0, RoundToNearestMultiple(transform.position.z));
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
