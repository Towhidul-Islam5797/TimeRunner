using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] objectPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnAheadDistance = 30f;

    [Header("Lane Settings")]
    [SerializeField] private float laneDistance = 2f;

    private Transform player;
    private float timer;

    // Lanes: -1 = left, 0 = center, 1 = right
    private readonly int[] lanes = { -1, 0, 1 };

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        if (objectPrefabs.Length == 0 || player == null) return;

        int randomLane = lanes[Random.Range(0, lanes.Length)];
        GameObject randomPrefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

        Vector3 spawnPosition = new Vector3(
            randomLane * laneDistance,
            player.position.y + 0.5f, // Slightly above player to avoid immediate collision
            player.position.z + spawnAheadDistance
        );

        Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
    }
}