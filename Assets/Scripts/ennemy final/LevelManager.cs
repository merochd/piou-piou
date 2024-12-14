using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public int totalLevels = 3; // Nombre total de niveaux
    public int wavesPerLevel = 10; // Nombre de vagues par niveau
    public int basicEnemiesPerWave = 12;
    public int sniperEnemiesPerWave = 12;// Nombre d'ennemis par vague
    public float timeBetweenLevels = 5f; // Temps entre les niveaux
    public int[][] gameDesign;

    [Header("References")]
    public GameObject basicEnemyPrefab;
    public GameObject sniperEnemyPrefab;// Préfab de l'ennemi
    public Transform[] spawnPoints; // Points de spawn

    private int currentLevel = 0;
    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(StartLevelSequence());
        gameDesign = new int[10][];

        for (int x = 0; x < gameDesign.Length; x++)
        {
            gameDesign[x] = new int[2];
        }
        
        gameDesign[0][0] = 20 ;
        gameDesign[0][1] = 20 ;
        gameDesign[1][0] = 8 ;
        gameDesign[1][1] = 2;
        gameDesign[2][0] = 10 ;
        gameDesign[2][1] = 4 ;
        gameDesign[3][0] = 8 ;
        gameDesign[3][1] = 8 ;
        gameDesign[4][0] = 10 ;
        gameDesign[4][1] = 10 ;
        gameDesign[5][0] = 8 ;
        gameDesign[5][1] = 12 ;
        gameDesign[6][0] = 11 ;
        gameDesign[6][1] = 11 ;
        gameDesign[7][0] = 12 ;
        gameDesign[7][1] = 12 ;
        gameDesign[8][0] = 0 ;
        gameDesign[8][1] = 20 ;
        gameDesign[9][0] = 20 ;
        gameDesign[9][1] = 20 ;
    }

    IEnumerator StartLevelSequence()
    {
        for (currentLevel = 1; currentLevel <= totalLevels; currentLevel++)
        {
            Debug.Log("Starting Level: " + currentLevel);
            yield return StartCoroutine(StartWaveSequence());
            Debug.Log("Level " + currentLevel + " completed.");
            yield return new WaitForSeconds(timeBetweenLevels);
        }
        Debug.Log("All levels completed!");
    }

    IEnumerator StartWaveSequence()
    {
        for (currentWave = 0; currentWave < wavesPerLevel; currentWave++)
        {
            Debug.Log("Starting Wave: " + currentWave);

            basicEnemiesPerWave = gameDesign[currentWave][0];
            sniperEnemiesPerWave = gameDesign[currentWave][1];

            // Spawn all enemies for the wave
            SpawnWave();

            // Wait until all enemies are destroyed
            yield return new WaitUntil(() => activeEnemies.Count == 0);
            Debug.Log("Wave " + currentWave + " completed.");
        }
    }

    void SpawnWave()
    {
        activeEnemies.Clear();
        int counter = 0;

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (counter >= basicEnemiesPerWave) break;

            GameObject enemy = Instantiate(basicEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            activeEnemies.Add(enemy);
            counter++;

            // Register enemy death
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            HomingEnemyController homingEnemyController = enemy.GetComponent<HomingEnemyController>();
            if (enemyController != null)
            {
                enemyController.OnEnemyDeath += HandleEnemyDeath;
            }            
            if (homingEnemyController != null)
            {
                homingEnemyController.OnEnemyDeath += HandleEnemyDeath;
            }
        }

        counter = 0;
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (counter >= sniperEnemiesPerWave) break;

            GameObject enemy = Instantiate(sniperEnemyPrefab, spawnPoint.position, spawnPoint.rotation);
            activeEnemies.Add(enemy);
            counter++;

            // Register enemy death
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            HomingEnemyController homingEnemyController = enemy.GetComponent<HomingEnemyController>();
            if (enemyController != null)
            {
                enemyController.OnEnemyDeath += HandleEnemyDeath;
            }            
            if (homingEnemyController != null)
            {
                homingEnemyController.OnEnemyDeath += HandleEnemyDeath;
            }
        }
    }

    void HandleEnemyDeath(GameObject enemy)
    {
        Debug.Log("ennemie touché");
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Debug.Log("ennemie mort");

        }
    }
}
