using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Transform leftBorder, rightBorder;

    [SerializeField]
    private float multiplier;

    [SerializeField]
    private int currentScore;

    [SerializeField]
    private TMP_Text scoreText;

    public int mistyped;

    private void Start()
    {
        currentScore = 0;
        mistyped = 0;
    }

    public void ThrowWord(TMP_Text tmp_wordObject)
    {
        try
        {
            WordObject wordObject = tmp_wordObject.GetComponentInParent<WordObject>();

            float wordXPosition = wordObject.transform.position.x;
            int wordScore = wordObject.GetWordScore();
            if (wordXPosition > leftBorder.position.x && wordXPosition < rightBorder.position.x)
            {
                currentScore += (int)(wordScore * multiplier);
                // The wordObject is between the left and right borders
                Debug.Log("Word is between the borders!");
            }
            else
            {
                currentScore += wordScore;
                // The wordObject is outside the borders
                Debug.Log("Word is outside the borders!");
            }
            SetScore(currentScore);
            GameManager.instance.UpdateOnScore(currentScore);
        }
        catch (System.Exception error)
        {
            Debug.Log(error);
            GameManager.instance.Reset();
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString("D4");
    }

    public int GetScore()
    {
        return currentScore;
    }

    public int GetMistyped()
    {
        return mistyped;
    }
}
