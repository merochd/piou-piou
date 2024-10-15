using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Le prefab de l'ennemi
    public float spawnInterval = 3f;  // Temps entre chaque vague
    public int enemiesPerWave = 5;  // Nombre d'ennemis par vague
    public Transform[] spawnPoints;  // Positions de spawn possibles

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            // Pour chaque vague, on instancie un certain nombre d'ennemis
            for (int i = 0; i < enemiesPerWave; i++)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);  // Choisir un point de spawn aléatoire
                Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
                yield return new WaitForSeconds(1f);  // Délai entre chaque ennemi
            }
            yield return new WaitForSeconds(spawnInterval);  // Délai entre les vagues
        }
    }
}
