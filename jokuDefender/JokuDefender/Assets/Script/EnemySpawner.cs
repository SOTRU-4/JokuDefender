using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : Wave
{
    float spawnTime;
    public int points;
    float waitBeforeStartWave = 10;
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] List<EnemyStats> enemies = new List<EnemyStats>();

    void Start()
    {
        preWaveState = true;
        Wave(preWaveState);
    }

    public void Wave(bool isPreWave)
    {
        if (isPreWave)
        {
            points = GetNewWavePoints() / 2;
            spawnTime = Random.Range(3, 5);
        }
        else 
        { 
            points = GetNewWavePoints(); spawnTime = 0.5f; 
        }

        StartCoroutine(Spawn(points, spawnTime));
    }

    IEnumerator Spawn(int points, float SpawnPerSec)
    {
        yield return new WaitForSeconds(waitBeforeStartWave);
        var thisWaveEnemies = EnemiesOfThisWave();

        while (points > 0) 
        {
            
            int randomEnemy = Random.Range(0, thisWaveEnemies.Count);
            int randomSpawnPos = Random.Range(0, SpawnPoints.Count);

            int enemyCost = thisWaveEnemies[randomEnemy].costInPoints;

            if (enemyCost > points)
            {
                continue;
            }

            yield return new WaitForSeconds(SpawnPerSec);

            Vector2 spawPos = new Vector2(SpawnPoints[randomSpawnPos].transform.position.x, SpawnPoints[randomSpawnPos].transform.position.y);
            GameObject prefab = thisWaveEnemies[randomEnemy].prefab;

            Instantiate(prefab, spawPos, Quaternion.identity);
            points -= enemyCost;
            this.points = points;
        }
        StopCoroutine(Spawn(points, spawnTime));
        NextWave();
        Wave(preWaveState);
    }
    List<EnemyStats> EnemiesOfThisWave()
    {
        List<EnemyStats> currentEnemies = new List<EnemyStats>();

        

        if(currentWave <= 3)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].costInPoints <= 2)
                {
                    currentEnemies.Add(enemies[i]);
                }
                Debug.Log("From FIRST wave");
            }
        }

        else if (currentWave >= 3 && currentWave <= 6)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].costInPoints <= 5)
                {
                    currentEnemies.Add(enemies[i]);
                }
                Debug.Log("From SECOND wave");
            }
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                currentEnemies.Add(enemies[i]);
                Debug.Log("From THIRD wave");
            }
        }

        return currentEnemies;
    }
}