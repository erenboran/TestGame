using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    public Transform[] spawnPoints;
    
    [SerializeField] 
    public Transform[] targetPoints;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    List<GameObject> enemyPrefabs = new List<GameObject>();

    [SerializeField]
    List<EnemyType> enemyTypes = new List<EnemyType>();
    
    [SerializeField]
    List<Wave> waves = new List<Wave>();

    [SerializeField]
    List<GameObject> spawnedEnemies = new List<GameObject>();

    public int dailyEnemyCount=1;
    float spawnTime;

    public int deadEnemyCount, totaldeadEnemyCount;

    public static WaveSpawner instance;

    public GameObject startDayButton;

    private void Awake()
    {
        instance = this;

    }

    public void StartWave()
    {
        CalculateTheWave();

        for (int i = 0; i < waves.Count; i++)
        {
            StartCoroutine(SpawnWave(waves[i]));
        }

        startDayButton.SetActive(false);
    }


    public void CalculateTheWave()
    {
        waves.Clear();

        deadEnemyCount = 0;

        spawnedEnemies.Clear();

        dailyEnemyCount = (int)(Mathf.Pow(1.45f, gameManager.day) + Mathf.Sin(gameManager.day) +10 );
        
        spawnTime = (int)(Mathf.Pow(1.0f, gameManager.day) / 10 + Mathf.Sin(gameManager.day) / 5 + 3);


        float totalPriority = 0;
        int totalWave=0;

        for (int i = 0; i < enemyTypes.Count; i++)
        {
            if (gameManager.day >= enemyTypes[i].startDay)
            {
                totalPriority += enemyTypes[i].priority;
               
                totalWave++;

            }

        }

        for (int i = 0; i < totalWave; i++)
        {
            waves.Add( new Wave(spawnTime / dailyEnemyCount / totalPriority * enemyTypes[i].priority, (int)(dailyEnemyCount / totalPriority * enemyTypes[i].priority), enemyPrefabs[i]));
            
        }
       
    }

    IEnumerator SpawnWave(Wave wave)
    {
       
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

        GameObject poolObject = PoolManager.instance.Pull("Enemy1",spawnPoints[randomSpawnPoint]);

        if (poolObject != null)
        {
            spawnedEnemies.Add(poolObject);
        }

        else
        {
            spawnedEnemies.Add(Instantiate(wave.enemyPrefab, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].transform.rotation));

        }


        spawnedEnemies[spawnedEnemies.Count-1].GetComponent<EnemyController>().target = targetPoints[randomSpawnPoint].gameObject; 

        wave.enemyCount--;

        yield return new WaitForSeconds(wave.spawnInterval);
        
        if (wave.enemyCount > 0)
        {
            StartCoroutine(SpawnWave(wave));
        }

    
    }

}
[System.Serializable] 
public class Wave
{
    public float spawnInterval;
    public int enemyCount;
    public GameObject enemyPrefab;

    public  Wave(float spawnInterval, int enemyCount, GameObject enemyPrefab)
    {
        this.spawnInterval = spawnInterval;
        this.enemyCount = enemyCount;
        this.enemyPrefab = enemyPrefab;

    }

}
