using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Préfabriqué pour les ennemis
    public float initialSpawnInterval = 6f; // Intervalle entre les vagues au début
    public float delayBetweenEnemies = 1f;  // Délai entre chaque ennemi dans une vague
    public Transform[] spawnPoints;      // Points de spawn des ennemis
    public int totalLevels = 5;          // Nombre total de niveaux
    public int wavesPerLevel = 10;       // Nombre de vagues par niveau

    private int currentLevel = 1;        // Niveau actuel
    private float spawnInterval;         // Intervalle dynamique entre les vagues

    private void Start()
    {
        spawnInterval = initialSpawnInterval; // Intervalle initial
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        // Boucle de progression par niveau
        while (currentLevel <= totalLevels)
        {
            for (int wave = 1; wave <= wavesPerLevel; wave++)
            {
                // Génère la vague actuelle avec difficulté croissante
                yield return StartCoroutine(SpawnWave(wave));

                // Réduit le temps entre les vagues pour augmenter la difficulté
                spawnInterval *= 0.95f; // Réduit de 5 % pour chaque vague
                yield return new WaitForSeconds(spawnInterval);
            }
            currentLevel++;
            spawnInterval = initialSpawnInterval; // Réinitialise l'intervalle entre vagues pour le prochain niveau
        }
    }

    IEnumerator SpawnWave(int waveNumber)
    {
        // Le nombre d'ennemis augmente avec le numéro de la vague
        int enemyCount = waveNumber * 2; // Par exemple, chaque vague ajoute 2 ennemis supplémentaires

        // Le délai entre les ennemis diminue légèrement à chaque vague
        float currentDelay = delayBetweenEnemies * Mathf.Pow(0.95f, waveNumber - 1);

        // Boucle pour créer tous les ennemis de la vague
        for (int i = 0; i < enemyCount; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(currentDelay); // Temps d'attente entre les ennemis
        }
    }
}
