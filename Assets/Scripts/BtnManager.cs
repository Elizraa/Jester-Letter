using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
