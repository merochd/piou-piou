using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  
    public float spawnInterval = 6f;  // Temps d'attente entre les vagues pour permettre à chaque vague de disparaître
    public float delayBetweenEnemies = 1f;  // Délai entre chaque ennemi dans une vague
    public Transform[] spawnPoints;  

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            int pattern = Random.Range(0, 3);  // Choisit un motif de manière aléatoire
            int enemyCount;

            switch (pattern)
            {
                case 0:  // Spirale descendante
                    enemyCount = 8;  
                    yield return StartCoroutine(SpawnSpiral(enemyCount));
                    break;

                case 1:  // Croix
                    enemyCount = 5;  
                    yield return StartCoroutine(SpawnCross(enemyCount));
                    break;

                case 2:  // Ligne Horizontale
                    enemyCount = 5;  
                    yield return StartCoroutine(SpawnHorizontal(enemyCount));
                    break;
            }

            yield return new WaitForSeconds(spawnInterval);  // Attente entre les vagues
        }
    }

    IEnumerator SpawnSpiral(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int spawnIndex = i % spawnPoints.Length;
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().SetMovementPattern("spiral");
            yield return new WaitForSeconds(delayBetweenEnemies);  // Attendre avant de spawn le prochain ennemi
        }
    }

    IEnumerator SpawnCross(int enemyCount)
    {
        // Pour la croix, on va suivre un ordre spécifique
        int[] spawnOrder = { 2, 1, 3, 1, 2, 4 };  // Indices des points de spawn pour former une croix

        foreach (int spawnIndex in spawnOrder)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().SetMovementPattern("cross");
            yield return new WaitForSeconds(delayBetweenEnemies);
        }
    }

    IEnumerator SpawnHorizontal(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int spawnIndex = i % spawnPoints.Length;
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().SetMovementPattern("horizontal");
            yield return new WaitForSeconds(delayBetweenEnemies);  // Attendre avant de spawn le prochain ennemi
        }
    }
}
