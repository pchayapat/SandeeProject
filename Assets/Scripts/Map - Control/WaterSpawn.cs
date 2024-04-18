using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawn : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn;
    public float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnPrefab());
    }

    IEnumerator SpawnPrefab()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int randomIndex = Random.Range(0, prefabsToSpawn.Count);
            Instantiate(prefabsToSpawn[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
