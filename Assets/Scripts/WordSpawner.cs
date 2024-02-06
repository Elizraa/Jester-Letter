using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject wordObject;

    [SerializeField]
    private GameObject wordFinish;

    [SerializeField]
    private WordManager wordManager;

    private int currentIndex = 0;

    private float[] delta = { 0, -0.5f, 0.5f , 1};

    private void Awake()
    {
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TMP_Text SpawnWord(string word, float speed)
    {

        float randomY = delta[currentIndex % delta.Length];
        currentIndex++;
        Vector3 spawnPosition;
        GameObject newObject;
        if (Random.Range(0, 10) > 4)
        {
            spawnPosition = new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z);
            newObject = Instantiate(wordObject, spawnPosition, Quaternion.identity);


            WordObject newWordObject = newObject.GetComponent<WordObject>();
            newWordObject.targetPosition = wordFinish.transform;
            newWordObject.moveSpeed = speed;
            newWordObject.wordManager = this.wordManager;
        }
        else
        {
            spawnPosition = new Vector3(wordFinish.transform.position.x, transform.position.y + randomY, transform.position.z);
            newObject = Instantiate(wordObject, spawnPosition, Quaternion.identity);


            WordObject newWordObject = newObject.GetComponent<WordObject>();
            newWordObject.targetPosition = transform;
            newWordObject.moveSpeed = speed;
            newWordObject.wordManager = this.wordManager;
        }

       

        TMP_Text newTextObject = newObject.GetComponentInChildren<TMP_Text>();
        newTextObject.text = word;
        return newTextObject;


    }
}
