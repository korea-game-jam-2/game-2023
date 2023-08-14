using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSources;

    public void SetMusicVolume(float volume)
    {
        musicSources.volume = volume;
    }
    public void setAudioVolume(float volume)
    {
        musicSources.volume = volume;
    }
}
