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
    private bool isGamePaused = false; // Bandera para verificar si el juego está en pausa

    void Start()
    {
        // Suscribirse al evento de cambio de estado del GameManager
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
    }

    void Update()
    {
        if (!isGamePaused) // Solo actualizar si el juego no está en pausa
        {
            if (timeSinceLastSpawn > 0)
            {
                timeSinceLastSpawn -= Time.deltaTime;
            }
            else
            {
                timeSinceLastSpawn = spawnRate;
                SpawnObstacle();
            }
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
            if (obstaculo.gameObject.activeSelf == false)
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

    // Método que se ejecuta cuando cambia el estado del juego
    void OnGameStateChanged(GAME_STATE newState)
    {
        if (newState == GAME_STATE.PAUSE)
        {
            isGamePaused = true; // Pausar la generación de obstáculos
        }
        else if (newState == GAME_STATE.PLAY)
        {
            isGamePaused = false; // Reanudar la generación de obstáculos
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento cuando este objeto sea destruido
        if (GameManager.GetInstance() != null)
        {
            GameManager.GetInstance().OnGameStateChanged -= OnGameStateChanged;
        }
    }
}
