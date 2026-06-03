using UnityEngine;
using TMPro; // Gunakan ini jika memakai TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Tarik objek ScoreText ke sini
    private int score = 0;

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}