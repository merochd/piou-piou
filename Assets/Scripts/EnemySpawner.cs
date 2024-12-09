using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;         // Préfabriqué pour les ennemis
    public Transform[] spawnPoints;        // Points de spawn (grille)
    public float initialSpawnInterval = 6f; // Intervalle entre les vagues au début
    public float delayBetweenEnemies = 1f;  // Délai entre chaque ennemi dans une vague
    public int totalLevels = 5;            // Nombre total de niveaux
    public int wavesPerLevel = 10;         // Nombre de vagues par niveau

    private int currentLevel = 1;          // Niveau actuel
    private float spawnInterval;           // Intervalle dynamique entre les vagues

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
                // Génère la vague actuelle en grille
                yield return StartCoroutine(SpawnWaveInGrid(wave));

                // Réduit le temps entre les vagues pour augmenter la difficulté
                spawnInterval *= 0.95f; // Réduit de 5 % pour chaque vague
                yield return new WaitForSeconds(spawnInterval);
            }
            currentLevel++;
            spawnInterval = initialSpawnInterval; // Réinitialise l'intervalle entre vagues pour le prochain niveau
        }
    }

    IEnumerator SpawnWaveInGrid(int waveNumber)
    {
        // Configuration de la grille : nombre de lignes et colonnes
        int rows = 2; // Modifier pour plus de lignes
        int cols = spawnPoints.Length / rows; // Nombre de colonnes calculé en fonction des spawn points

        // Délai entre chaque ennemi (peut être ajusté avec la difficulté)
        float currentDelay = delayBetweenEnemies * Mathf.Pow(0.95f, waveNumber - 1);

        // Parcourt la grille et instancie les ennemis
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int spawnIndex = row * cols + col;
                if (spawnIndex < spawnPoints.Length) // Vérifie que l'index reste valide
                {
                    Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
                    yield return new WaitForSeconds(currentDelay); // Temps d'attente entre chaque ennemi
                }
            }
        }
    }
}
