using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : Wave
{
    float spawnTime;
    public int points;
    float waitBeforeStartWave = 10;
    [SerializeField] Text waveText;
    Animator waveTextAnim;
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] List<EnemyStats> enemies = new List<EnemyStats>();

    int easyEnemiesCost = 4;
    int mediumEnemiesCost = 10;

    void Start()
    {
        preWaveState = true;
        Wave(preWaveState);
        waveText.TryGetComponent(out waveTextAnim);
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
            waveText.text = "Wave " + currentWave;
            points = GetNewWavePoints(); spawnTime = 0.5f;
            waveTextAnim.SetTrigger("New Wave Start");
        }

        StartCoroutine(Spawn(points, spawnTime));
    }

    IEnumerator Spawn(int points, float SpawnPerSec)
    {
        
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

        yield return new WaitForSeconds(waitBeforeStartWave);
        NextWave();
        Wave(preWaveState);
    }
    List<EnemyStats> EnemiesOfThisWave()
    {
        List<EnemyStats> currentEnemies = new List<EnemyStats>();

        

        if(currentWave <= 2)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].costInPoints <= easyEnemiesCost)
                {
                    currentEnemies.Add(enemies[i]);
                }
            }
        }

        else if (currentWave >= 3 && currentWave <= 5)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].costInPoints <= mediumEnemiesCost)
                {
                    currentEnemies.Add(enemies[i]);
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