using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{

    public bool waitingPeriod;
    [SerializeField] Enemy_Base[] normalEnemies;
    [SerializeField] float maxSpawnTime = 5f, minSpawnTime = 2f;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float maxX = 1f, minX = -1f, maxZ = 1f, minZ = -1f;

    [SerializeField] float btnWaveTime = 30f;
    [SerializeField] int baseWeight = 10;
    [SerializeField] int growthFactor = 5;


    [Header("--BOSS PROPERTIES--")]
    [SerializeField] int bossWaveNumber = 10;
    [SerializeField] Enemy_Base[] bossEnemies;
    [SerializeField] bool bossSpawned;
    [SerializeField] Transform[] bossSpawnPoints;

    [Header("-- UI ELEMENTS--")]
    [SerializeField] Text waveText;
    [SerializeField] Text nextWaveTimerText;
    [SerializeField] GameObject nextWavePanel;

    int currentWaveNumber = 1;
    [SerializeField] int waveWeight = 10;
    float spawnTime;
    bool canSpawn;
    float _btnWaveTime;

    int waveNumber;
    GameStatus gameStatus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStatus = GetComponent<GameStatus>();
        waveNumber = PlayerPrefs.GetInt("Wave number", 5);
        canSpawn = true;
        RandomSpawnTime();
        _btnWaveTime = btnWaveTime;
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Wave " + currentWaveNumber.ToString();
        if (spawnTime > 0)
            spawnTime -= Time.deltaTime;

        if (waveWeight <= 0)
            canSpawn = false;

        SpawnEnemy();
        CheckForNextWave();
    }

    void RandomSpawnTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnEnemy()
    {
        if(spawnTime <= 0 && canSpawn)
        {
            if(currentWaveNumber % bossWaveNumber == 0)
            {
                if (!bossSpawned)
                    SpawnBos();
            }
            SpawnNomalEnemy();
        }
    }

    void SpawnNomalEnemy()
    {
        GameObject enemy = GetRandomEnemyFromPool();
        if(enemy!= null)
        {
            Instantiate(enemy, RandomSpawnPoint(), Quaternion.identity);
            waveWeight -= enemy.GetComponent<Enemy_Base>().weight;
        }
        RandomSpawnTime();
    }
    void SpawnBos()
    {
        GameObject boss = GetRandomBossFromPool();
        if(boss != null)
        {
            Instantiate(boss, RandomSpawnPoint(), Quaternion.identity);
            waveWeight -= boss.GetComponent<Enemy_Base>().weight;
        }
        RandomSpawnTime();
        bossSpawned = true;
    }

    Vector3 RandomSpawnPoint()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        return new Vector3(randomX, 0, randomZ) + spawnPoints[spawnPointIndex].position;
    }

    public GameObject GetRandomBossFromPool()
    {
        List<Enemy_Base> validBosses = new List<Enemy_Base>();
        foreach(Enemy_Base boss in bossEnemies)
        {
            if(boss.minWaveNumber <= currentWaveNumber && boss.weight <= waveWeight)
            {
                validBosses.Add(boss);
            }
        }
        if(validBosses.Count > 0)
        {
            int index = Random.Range(0, validBosses.Count);
            return validBosses[index].gameObject;
        }
        return null;
    }

    public GameObject GetRandomEnemyFromPool()
    {
        List<Enemy_Base> validEnemies = new List<Enemy_Base>();
        foreach(Enemy_Base enemy in normalEnemies)
        {
            if(enemy.minWaveNumber <= currentWaveNumber && enemy.weight <= waveWeight)
            {
                validEnemies.Add(enemy);
            }
        }
        if(validEnemies.Count > 0)
        {
            int index = Random.Range(0, validEnemies.Count);
            return validEnemies[index].gameObject;
        }
        return null;
    }

    void CheckForNextWave()     //we'll also use this to determine if we've completed the level
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length <= 0 && !canSpawn)
        {
            if(currentWaveNumber == waveNumber)
            {
                LevelComplete();
                return;
            }
            //This is where we'll add the Game Complete Logic
            waitingPeriod = true;
            NextWave();
        }
        else
        {
            waitingPeriod = false;
            nextWaveTimerText.text = "";
            nextWavePanel.SetActive(false);
        }
    }

    void NextWave()
    {
        _btnWaveTime -= Time.deltaTime;
        float displayTime = Mathf.Floor(_btnWaveTime * 100f) / 100f;
        nextWavePanel.SetActive(true);
        if(_btnWaveTime <= 0)
        {
            currentWaveNumber += 1;
            canSpawn = true;
            _btnWaveTime = btnWaveTime;
            WeightUpdate();
        }
        bossSpawned = false;
        SpawnTimeReduction();
        nextWaveTimerText.text = displayTime.ToString("00.00");
    }

    void WeightUpdate()
    {
        waveWeight = baseWeight + (currentWaveNumber * growthFactor);
    }

    void SpawnTimeReduction()
    {
        if(currentWaveNumber % 3 == 0)
        {
            if (maxSpawnTime > 2f)
                maxSpawnTime -= 0.5f;
            if (minSpawnTime > 0.5f)
                minSpawnTime -= 0.5f;
        }
    }

    void LevelComplete()
    {
        if(!gameStatus.Over())
            gameStatus.GameComplete();
    }

    public int WaveNumber()
    {
        return waveNumber;
    }
}
