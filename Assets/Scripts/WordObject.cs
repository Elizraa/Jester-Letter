using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordObject : MonoBehaviour
{
    public Transform targetPosition;
    public float moveSpeed = 5f;

    [SerializeField]
    private int score = 1;

    [SerializeField]
    private ParticleSystem particleDestroy;

    public WordManager wordManager;

    void Start()
    {
        // Start the coroutine when the script is initialized
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (Vector3.Distance(transform.position, targetPosition.position) > 0.01f)
        {
            // Move the object towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            // Yield execution until the next frame
            yield return null;
        }

        // Optional: Do something when the object has reached the target position
        Debug.Log("Object has reached the target position!");
        End();
    }

    public int GetWordScore()
    {
        StartCoroutine(DestroyWord());
        return score;
    }

    private IEnumerator DestroyWord()
    {
        yield return new WaitForSeconds(0.1f);
        if (gameObject != null)
        {
            particleDestroy.Play();
            Destroy(transform.GetChild(0).gameObject);
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }

    private void End()
    {
        wordManager.DeleteOnList(this.GetComponentInChildren<TMP_Text>());
        GameManager.instance.FinishLine(10);
        Destroy(gameObject);
    }
}
