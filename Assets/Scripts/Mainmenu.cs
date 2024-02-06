using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mainmenu : MonoBehaviour
{
    public TMP_Text highscore;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Highscore"))
            PlayerPrefs.SetInt("Highscore", 0);
        highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
