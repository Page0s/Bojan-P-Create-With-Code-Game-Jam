using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] int spawnEnemys = 5;
    [SerializeField] float timeBetweenSpawns = 5;
    [SerializeField] Transform[] spawnLocations;
    [SerializeField] GameObject enemy;

    GameManager gameManager;
    bool isSpawningEnemys = true;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private IEnumerator SpawnEnemys()
    {
        for(int i = 0; i < spawnEnemys; ++i)
        {
            Instantiate(enemy, spawnLocations[Random.Range(0, spawnLocations.Length)].position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(timeBetweenSpawns);
        }
    }

    // Update is called every frame, if the MonoBehaviour is enabled
    private void Update()
    {
        if (gameManager.IsGameActive && isSpawningEnemys)
        {
            Debug.Log("Spawning Enemys!");
            isSpawningEnemys = false;
            StartCoroutine(SpawnEnemys());
        }
    }
}
