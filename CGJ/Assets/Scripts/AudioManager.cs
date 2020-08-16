using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;

    public AudioClip whiteClip;
    public AudioClip blackClip;
    private int wake ;

    AudioSource whiteSource;
    AudioSource blackSource; 

    private void Awake()
    {
        current = this;
        wake = 1;

        DontDestroyOnLoad(gameObject);

        whiteSource = gameObject.AddComponent<AudioSource>();
        blackSource = gameObject.AddComponent<AudioSource>();

        StartLevelAudio();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl ))
        {
            wake = -wake;
            Debug.Log(wake);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            wake = -wake;
            Debug.Log(wake);
        }
        if (wake == 1)
        {
            /*while(current.whiteSource.volume <= 1)
            {
                current.whiteSource.volume = current.whiteSource.volume + 0.1f;
            }
            while(current.blackSource.volume >= 0)
            {
                current.blackSource.volume = current.blackSource.volume - 0.1f;
            }*/
            for(float i=1; current.whiteSource.volume < 1; i++)
            {
                current.whiteSource.volume = 0.1f*i;
            }
            for (float i = 0.1f; current.blackSource.volume > 0; i++)
            {
                current.blackSource.volume = current.blackSource.volume - i;
            }


        }
        else if (wake == -1)
        {
            for (float i = 0.1f; current.blackSource.volume < 1; i++)
            {
                current.blackSource.volume = current.blackSource.volume + i;
            }
            for (float i = 0.1f; current.whiteSource.volume > 0; i++)
            {
                current.whiteSource.volume = current.whiteSource.volume - i;
            }


        }

    }

    void StartLevelAudio()
    {

        current.whiteSource.clip = current.whiteClip;
        current.whiteSource.loop = true;
        current.blackSource.clip = current.blackClip;
        current.blackSource.loop = true;


        current.whiteSource.Play();
        current.blackSource.Play();


    }
}
