using UnityEngine;
using TMPro;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager;

    public TMP_Text scoreText, mistypedText;
    public float countingDuration = 1f; // Set the duration in seconds for the counting animation

    void OnEnable()
    {
        StartCoroutine(CountUpScore(scoreManager.GetScore()));
        StartCoroutine(CountUpMistyped(scoreManager.GetMistyped()));

        if (scoreManager.GetScore() > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", scoreManager.GetScore());
        }
    }

    IEnumerator CountUpScore(int finalScore)
    {
        float elapsedTime = 0f;
        int currentScore = 0;

        while (elapsedTime < countingDuration)
        {
            currentScore = (int)Mathf.Lerp(0, finalScore, elapsedTime / countingDuration);
            scoreText.text = currentScore.ToString();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final score is displayed accurately
        scoreText.text = finalScore.ToString();
    }

    IEnumerator CountUpMistyped(int finalMistyped)
    {
        float elapsedTime = 0f;
        int currentScore = 0;

        while (elapsedTime < countingDuration)
        {
            currentScore = (int)Mathf.Lerp(0, finalMistyped, elapsedTime / countingDuration);
            mistypedText.text = currentScore.ToString();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the final score is displayed accurately
        mistypedText.text = finalMistyped.ToString();
    }
}
