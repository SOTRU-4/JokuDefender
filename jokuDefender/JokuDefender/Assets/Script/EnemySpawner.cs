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

        while (points > 0) 
        {
            var thisWaveEnemies = ThisWaveEnemies();
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
    List<EnemyStats> ThisWaveEnemies()
    {
        List<EnemyStats> currentEnemies = new List<EnemyStats>();
        if(currentWave <= 2)
        {
            foreach (EnemyStats enemy in enemies)
            {
                if (enemy.costInPoints <= 2)
                {
                    currentEnemies.Add(enemy);
                }
            }
        }

        if (currentWave > 2 && currentWave <= 5)
        {
            foreach (EnemyStats enemy in enemies)
            {
                if (enemy.costInPoints <= 5)
                {
                    currentEnemies.Add(enemy);
                }
            }
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                currentEnemies.Add(enemies[i]);
            }
        }

        return currentEnemies;
    }
}
