using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSFX;

    public AudioClip[] audioClip;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayOneShot(int index)
    {
        audioSourceSFX.PlayOneShot(audioClip[index]);
    }

    public void StopMusic()
    {
        audioSourceMusic.Stop();
    }

}
