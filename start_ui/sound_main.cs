using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_main : MonoBehaviour
{
    public AudioSource audio_main;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void change_audio(AudioClip clip_temp)
    {
        audio_main.clip = clip_temp;
    }
    public void audio_play()
    {
        audio_main.Play();
    }
    public void audio_stop()
    {
        audio_main.Stop();
    }
}
