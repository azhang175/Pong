using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpManager : MonoBehaviour
{
    // Reference to the GameManager script
    public GameManager gameManager;
    
    // List of power-up prefabs to spawn
    public List<GameObject> powerUpPrefabs;
    
    // Minimum and maximum time between power-up spawns
    public float minAppearTime = 1f;
    public float maxAppearTime = 3f;
    
    // Boundaries for the spawn area
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private void Start()
    {
        // Start the coroutine to spawn power-ups
        StartCoroutine(SpawnPowerUp());
    }
    
    private IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            // Wait for a random amount of time before spawning the next power-up
            float waitTime = Random.Range(minAppearTime, maxAppearTime);
            yield return new WaitForSeconds(waitTime);

            // Select a random power-up prefab to spawn
            GameObject powerUpToSpawn = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];

            // Generate a random position within the spawn area
            Vector2 newPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y));

            // Instantiate the power-up at the generated position
            GameObject powerUpInstance = Instantiate(powerUpToSpawn, newPosition, Quaternion.identity);
            
            // Destroy the power-up after 7 seconds
            Destroy(powerUpInstance, 7f);
        }
    }
}
