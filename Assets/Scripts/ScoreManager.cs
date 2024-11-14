using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public static void AddScore(int points)
    {
        score += points;
    }
}