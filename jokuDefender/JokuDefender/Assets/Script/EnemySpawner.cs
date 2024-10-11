using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : Wave
{
    float spawnTime;
    int points;
    float waitBeforeStartWave;
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] List<EnemyStats> enemies = new List<EnemyStats>();

    void Start()
    {
        preWaveState = true;
        Wave(preWaveState);
    }

    void Wave(bool isPreWave)
    {
        if (isPreWave)
        {
            points = GetNewWavePoints() / 2;
            spawnTime = Random.Range(3, 5);
            waitBeforeStartWave = 5;
        }
        else { points = GetNewWavePoints(); spawnTime = 0.5f; waitBeforeStartWave = 10; }

        StartCoroutine(Spawn(points, spawnTime));
    }

    IEnumerator Spawn(int points, float SpawnPerSec)
    {
        yield return new WaitForSeconds(waitBeforeStartWave);

        while (points > 0) 
        {
            int randomEnemy = Random.Range(0, enemies.Count);
            int randomSpawnPos = Random.Range(0, SpawnPoints.Count);

            int enemyCost = enemies[randomEnemy].costInPoints;

            if (enemyCost > points)
            {
                continue;
            }

            yield return new WaitForSeconds(SpawnPerSec);

            Vector2 spawPos = new Vector2(SpawnPoints[randomSpawnPos].transform.position.x, SpawnPoints[randomSpawnPos].transform.position.y);
            GameObject prefab = enemies[randomEnemy].prefab;

            Instantiate(prefab, spawPos, Quaternion.identity);
            points -= enemyCost;
        }
        StopCoroutine(Spawn(points, spawnTime));
        NextWave();
        Wave(preWaveState);
    }
}
