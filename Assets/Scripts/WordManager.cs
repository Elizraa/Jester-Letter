using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;

public class WordManager : MonoBehaviour
{
    public string[] words;

    public WordSpawner wordSpawner;
    public Color defaultColor;
    public Color correctColor;

    [HideInInspector]
    private TMP_Text currentWordObject;

    public List<TMP_Text> spawnedWords = new List<TMP_Text>();
    public List<string> spawnedStrings = new List<string>();

    public List<int> indexWordChosed = new List<int>();

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private TMP_InputField inputField;

    private int currentIndex;

    [SerializeField]
    private int damage = 7;

    private bool greenState = false;

    //private bool firstWordFound = false;


    private void Start()
    {
        inputField.ActivateInputField();
    }


    public void StartSpawning(float speed)
    {
        if (currentIndex >= words.Length)
        {
            // Shuffle the array to restart the cycle
            ShuffleArray(words);
            currentIndex = 0;
        }

        string newWord = words[currentIndex];
        TMP_Text newWordObject = wordSpawner.SpawnWord(newWord, speed);
        spawnedStrings.Add(newWord);
        spawnedWords.Add(newWordObject);
        currentIndex++;
    }

    // Helper method to shuffle an array
    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    public void OnValueChange(string input)
    {
        AudioManager.instance.PlayOneShot(1);

        if (indexWordChosed.Count > 0)
        {
            CheckTypo(input);
            return;
        }

        if (input == "")
        {
            //currentWordObject.color = defaultColor;
            return;
        }
        for(int i=0; i<spawnedWords.Count;i++)
        {
            if (spawnedStrings[i][0] == input[0])
            {
                indexWordChosed.Add(i);
                string coloredText = $"<color=green>{input}</color>{spawnedStrings[i].Substring(input.Length)}";
                spawnedWords[i].text = coloredText;
                greenState = true;
            }


        }
    }

    private void CheckTypo(string input)
    {
        if (GameManager.instance.currentState == State.GameOver)
        {
            return;
        }

        if(input == "")
        {
            foreach (int index in indexWordChosed)
            {
                spawnedWords[index].text = spawnedStrings[index];
            }
            indexWordChosed.Clear();
            return; 
        }
        List<int> indicesToRemove = new List<int>();

        foreach (int index in indexWordChosed)
        {
            if (input == spawnedStrings[index])
            {
                spawnedWords[index].text = $"<color=green>{spawnedStrings[index]}</color>";
                AudioManager.instance.PlayOneShot(0);
                GameManager.instance.SetKingSprite(1);
                scoreManager.ThrowWord(spawnedWords[index]);

                        indexWordChosed.Clear();
                inputField.text = "";
                return;
            }

            if (input.Length <= spawnedStrings[index].Length && input == spawnedStrings[index].Substring(0, input.Length))
            {
                // Change color to green for correct input
                string coloredText = $"<color=green>{input}</color>{spawnedStrings[index].Substring(input.Length)}";
                spawnedWords[index].text = coloredText;
                greenState = true;
            }
            else
            {
                if (indexWordChosed.Count == 1)
                {
                    string word = spawnedStrings[index];

                    int matchLength = word.Zip(input, (c1, c2) => c1 == c2).TakeWhile(b => b).Count();
                    int nonMatchStart = Mathf.Min(matchLength, word.Length);

                    string coloredText = $"<color=green>{word.Substring(0, nonMatchStart)}</color>" +
                                        $"<color=red>{word.Substring(nonMatchStart)}</color>";

                    //string coloredText = $"<color=green>{spawnedStrings[index].Substring(0, input.Length - 1)}</color>" +
                    //    $"<color=red>{spawnedStrings[index].Substring(input.Length-1)}</color>";

                    spawnedWords[index].text = coloredText;
                    if (greenState)
                    {
                        GameManager.instance.WrongWord(damage);
                        greenState = false;
                    }
                    continue;
                }
                indicesToRemove.Add(index);
            }
        }

        if (indexWordChosed.Count == indicesToRemove.Count)
        {
            GameManager.instance.WrongWord(damage*2);
            foreach (int indexToRemove in indicesToRemove)
            {
                spawnedWords[indexToRemove].text = $"{spawnedStrings[indexToRemove]}";
                StartCoroutine(ChangeColorTemp(spawnedWords[indexToRemove], spawnedStrings[indexToRemove]));
                indexWordChosed.Remove(indexToRemove);
            }
            return;
        }

        foreach (int indexToRemove in indicesToRemove)
        {
            spawnedWords[indexToRemove].text = $"{spawnedStrings[indexToRemove]}";
            indexWordChosed.Remove(indexToRemove);
        }
    }

    IEnumerator ChangeColorTemp(TMP_Text tMP_Text, string text)
    {
        tMP_Text.text = $"<color=red>{text}</color>";
        yield return new WaitForSeconds(0.5f);
        tMP_Text.text = text;
    }

    public void Reset()
    {
        foreach (TMP_Text tMP_Text in spawnedWords)
        {
            try
            {
                WordObject wordObject = tMP_Text.GetComponent<WordObject>();
                wordObject.GetWordScore();
            }
            catch
            {
                continue;
            }
        }

        indexWordChosed.Clear();
        spawnedStrings.Clear();
        spawnedWords.Clear();
    }

    public void DeleteOnList(TMP_Text item)
    {
        try
        {
            int index = spawnedWords.IndexOf(item);

            if (index >= 0)
            {
                // Remove the item from the lists
                spawnedStrings.RemoveAt(index);
                spawnedWords.Remove(item);
                indexWordChosed.Remove(index);
            }
            else
            {
                Debug.LogWarning("Item not found in the spawnedWords list.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred: {ex.Message}");
            // You can also log the stack trace if needed: Debug.LogException(ex);
        }
    }

}
