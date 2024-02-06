using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum State
{
    Start,
    Pause,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public WordManager wordManager;

    public State currentState;

    [SerializeField]
    private float minSpawn, maxSpawn;

    [SerializeField]
    private float baseMinSpawn, baseMaxSpawn;

    [SerializeField]
    private float baseSpeed;

    [SerializeField]
    private Player jester;

    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private ScoreManager scoreManager;


    [SerializeField]
    private King king;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        minSpawn = baseMinSpawn;
        maxSpawn = baseMaxSpawn;
        speed = baseSpeed;
        gameOverCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawnWord());
    }

    public void Reset()
    {
        scoreManager.SetScore(scoreManager.GetScore()+1);
        wordManager.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartSpawnWord()
    {
        while (currentState != State.GameOver)
        {
            wordManager.StartSpawning(speed);
            yield return new WaitForSeconds(Random.Range(minSpawn, maxSpawn));
        }
    }

    public void UpdateOnScore(int score)
    {
        AdjustSpawnRatesBasedOnScore(score);
        AdjustSpeedBasedOnScore(score);
    }

    void AdjustSpeedBasedOnScore(int score)
    {
        // Use a formula to calculate speed based on the score
        float speedIncreasePercentage = baseSpeed * score / 100; // 10% increase for each score point
        speed = baseSpeed + speedIncreasePercentage;
    }

    void AdjustSpawnRatesBasedOnScore(int score)
    {
        // Use a formula to adjust minSpawn and maxSpawn based on the score
        float minSpawnReductionPercentage = 0.05f; // 2% reduction for each score point
        float maxSpawnReductionPercentage = 0.05f; // 1% reduction for each score point

        minSpawn = baseMinSpawn - (score * minSpawnReductionPercentage);
        maxSpawn = baseMaxSpawn - (score * maxSpawnReductionPercentage);
    }


    public void WrongWord(int damage)
    {
        scoreManager.mistyped++;
        king.Throw();
        AudioManager.instance.PlayOneShot(2);
        jester.TakeDamage(damage);
        SetKingSprite(2);
    }

    public void FinishLine(int damage)
    {
        king.Throw();
        AudioManager.instance.PlayOneShot(2);
        jester.TakeDamage(damage);
        SetKingSprite(2);
    }

    public void GameOver()
    {
        currentState = State.GameOver;
        gameOverCanvas.SetActive(true);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayOneShot(3);
    }

    public void SetKingSprite(int index)
    {
        king.SetSprite(index);
    }
}
