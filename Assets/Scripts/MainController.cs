using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController Instance { get; private set; }

    List<EnemySpawner> spawners = new List<EnemySpawner>();

    int waveNumber = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        // Retrieve spawner list
        foreach (Transform child in GameObject.Find("Spawners").transform)
        {
            EnemySpawner spawner = child.GetComponent<EnemySpawner>();
            spawners.Add(spawner);
        }

        NextWave();
    }

    public void NextWave()
    {
        switch (waveNumber)
        {
            case 0:
                for (int i = 0; i < 25; i++)
                {
                    spawners[Random.Range(0, spawners.Count)].SpawnEnemy("Chort");
                }
                
                break;
        }

        waveNumber++;
    }

    public void GameOver()
    {
        //GameObject.Find("GameOverScreen").SetActive(true);
    }
}
