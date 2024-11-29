using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioGenerator : MonoBehaviour
{
    public event Action<Vector3> OnFinalTileGenerated; // Evento para notificar cuando se calcule la posición final

    GAME_STATE gameState;

    public GameObject groundTile;
    public GameObject finalTile;
    public LayerMask mask;
    public int tileWith;
    public int defaultTilesCount;
    public int totalTilesToSpawn;
    public Vector3 startPos;
    public Transform groundChecker;
    public GameObject destroyer;

    private int tilesSpawnedCount = 0;
    private bool isGrounding;
    private bool startSpawning, spawn;
    private Vector3 nextSpawmPoint;
    private Vector3 finalTilePosition;

    private void Start()
    {
        transform.position = startPos;
        groundChecker.position = transform.position + new Vector3(0, 0, (-tileWith / 2));
        destroyer.transform.position = transform.position - new Vector3(0, 0, tileWith * defaultTilesCount + defaultTilesCount);
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        SpawnDefault();
        startSpawning = true;

        CalculateFinalTilePosition();
    }

    private void CalculateFinalTilePosition()
    {
        finalTilePosition = startPos + new Vector3(0, 0, tileWith * totalTilesToSpawn);
        OnFinalTileGenerated?.Invoke(finalTilePosition); // Notificar la posición final
    }

    private void SpawnDefault()
    {
        for (int i = 0; i < defaultTilesCount - 1; i++)
        {
            SpawnTile();
            transform.localPosition += new Vector3(0, 0, tileWith);
        }
        SpawnTile();
    }

    private void Update()
    {
        if (gameState == GAME_STATE.PLAY && startSpawning)
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
        if (tilesSpawnedCount >= totalTilesToSpawn)
        {
            SpawnFinalTile();
            startSpawning = false;
            return;
        }

        nextSpawmPoint = new Vector3(0, 0, RoundToNearestMultiple(transform.position.z));
        GameObject newTile = PoolMaster.GetInstance().GetTileToSpawn();
        newTile.transform.parent = null;
        newTile.transform.position = nextSpawmPoint;
        newTile.SetActive(true);
        newTile.GetComponent<Tile>().Inicializar();

        tilesSpawnedCount++;
        spawn = false;
    }

    void SpawnFinalTile()
    {
        nextSpawmPoint = new Vector3(0, 0, RoundToNearestMultiple(transform.position.z));

        if (finalTile == null)
        {
            Debug.LogError("El prefab 'finalTile' no está asignado en el Inspector.");
            return;
        }

        GameObject finalTileInstance = Instantiate(finalTile, nextSpawmPoint, Quaternion.identity);
        finalTileInstance.transform.parent = null;
        finalTileInstance.SetActive(true);
        finalTileInstance.GetComponent<Tile>().Inicializar();

        // Verificar si el objeto está activo y tiene la escala correcta
        if (!finalTileInstance.activeSelf)
        {
            Debug.LogWarning("El tile final está inactivo después de la instanciación.");
        }

        if (finalTileInstance.transform.localScale == Vector3.zero)
        {
            Debug.LogWarning("La escala del tile final es cero. Ajustando a 1,1,1.");
            finalTileInstance.transform.localScale = Vector3.one;
        }

        Debug.Log("Tile final generado con éxito en la posición: " + nextSpawmPoint);
    }

    public Vector3 GetFinalTilePosition()
    {
        return finalTilePosition;
    }

    public float RoundToNearestMultiple(float number)
    {
        return Mathf.Round(number / tileWith) * tileWith;
    }

    void OnGameStateChanged(GAME_STATE _newGameState)
    {
        gameState = _newGameState;
    }
}
