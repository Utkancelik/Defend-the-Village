using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private float minDelay, maxDelay;

    private void Start()
    {
        StartCoroutine(SpawnInterruptedly(spawnPrefab));
    }
    public IEnumerator SpawnInterruptedly(GameObject spawnObj)
    {
        while (spawnObj != null)
        {
            Instantiate(spawnObj, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
        yield return null;
    }
}
