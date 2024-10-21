using Unity.VisualScripting;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPoint;

    // Crear un evento estático para cuando se genere un nuevo GroundTile
    public static event System.Action OnTileSpawned;

    public void SpawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        // Disparar el evento cuando se genera un nuevo GroundTile
        if (OnTileSpawned != null)
        {
            OnTileSpawned.Invoke();
        }
    }

    private void Start()
    {
        nextSpawnPoint = transform.position;
        for (int i = 0; i < 5; i++)
        {
            SpawnTile();
        }
    }
}
