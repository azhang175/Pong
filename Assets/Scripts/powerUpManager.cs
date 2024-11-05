using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpManager : MonoBehaviour
{
    public GameManager gameManager;
    public List<GameObject> powerUpPrefabs;
    public float minAppearTime = 1f;
    public float maxAppearTime = 3f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }
    
    private IEnumerator SpawnPowerUp()
    {

        while (true)
        {
            float waitTime = Random.Range(minAppearTime, maxAppearTime);
            yield return new WaitForSeconds(waitTime);

            GameObject powerUpToSpawn = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];

            Vector2 newPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y));

            GameObject powerUpInstance = Instantiate(powerUpToSpawn, newPosition, Quaternion.identity);
            Destroy(powerUpInstance, 7f);
        }
    }
}
