using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private float minDelay, maxDelay, randomX, randomY;

    private void OnEnable()
    {
        StartCoroutine(SpawnInterruptedly(spawnPrefab));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && FindObjectOfType<GameManager>().isGameRunning)
        {
            gameObject.GetComponent<Spawner>().enabled = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && FindObjectOfType<GameManager>().isGameRunning)
        {
            gameObject.GetComponent<Spawner>().enabled = false;
        }
    }
    public IEnumerator SpawnInterruptedly(GameObject spawnObj)
    {
        while (spawnObj != null)
        {
            GameObject obj = Instantiate(spawnObj, transform.position, Quaternion.identity);
            Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-randomX,randomX), transform.position.y + Random.Range(-randomY,randomY));
            obj.transform.position = spawnPos;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
        yield return null;
    }
}
