using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public int totalLevels = 3; // Nombre total de niveaux
    public int wavesPerLevel = 10; // Nombre de vagues par niveau
    public int enemiesPerWave = 12; // Nombre d'ennemis par vague
    public float timeBetweenLevels = 5f; // Temps entre les niveaux

    [Header("References")]
    public GameObject enemyPrefab; // Préfab de l'ennemi
    public Transform[] spawnPoints; // Points de spawn

    private int currentLevel = 0;
    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(StartLevelSequence());
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
        for (currentWave = 1; currentWave <= wavesPerLevel; currentWave++)
        {
            Debug.Log("Starting Wave: " + currentWave);

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

        foreach (Transform spawnPoint in spawnPoints)
        {
            if (activeEnemies.Count >= enemiesPerWave) break;

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            activeEnemies.Add(enemy);

            // Register enemy death
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.OnEnemyDeath += HandleEnemyDeath;
            }
        }
    }

    void HandleEnemyDeath(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
    }
}
