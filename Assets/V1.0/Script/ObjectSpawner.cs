#region Summary
/// <summary>
/// This script is responsible for spawning objects (like obstacles or collectibles) ahead of the player in
/// an endless runner game. It randomly selects a prefab from a list and spawns it in one of three lanes at a specified interval.
/// Details:
/// - The script uses a timer to control the spawn interval.
/// - It calculates the spawn position based on the player's current position and a specified distance ahead.
/// - The spawned objects are instantiated at a random lane (left, center, right).
/// Usage:
/// 1. Attach this script to an empty GameObject in your Unity scene.
/// 2. Assign the object prefabs you want to spawn in the Inspector.
/// 3. Set the spawn interval, spawn ahead distance, and lane distance in the Inspector
/// Note: Ensure that the player GameObject has a PlayerMovement script attached for the spawner to reference the player's position.
/// </summary>
#endregion
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