using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int currentWave {  get; private set; }
    [SerializeField] int wavePoints;
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] List<EnemyStats> enemies = new List<EnemyStats>();

    void Start()
    {
        StartCoroutine(PreWave());
    }


    void Update()
    {
        
    }

    IEnumerator PreWave()
    {
        if(currentWave <= 5) { wavePoints += 5; }
        else { wavePoints += 10; }

        for (int wavePoints = this.wavePoints; wavePoints > 0;)
        {
            int randomEnemy = Random.Range(0, enemies.Count);
            int randomSpawnPos = Random.Range(0, SpawnPoints.Count);

            int enemyCost = enemies[randomEnemy].costInPoints;

            if (enemyCost > wavePoints)
            {
                Debug.Log("Spawn Canceled");
                continue;
            }

            float randomSpawnTime = Random.Range(2, 5);
            yield return new WaitForSeconds(randomSpawnTime);


            Vector2 spawPos = new Vector2(SpawnPoints[randomSpawnPos].transform.position.x, SpawnPoints[randomSpawnPos].transform.position.y);
            GameObject prefab = enemies[randomEnemy].prefab;

            Instantiate(prefab, spawPos, Quaternion.identity);
            wavePoints -= enemyCost;
        }
    }
}
