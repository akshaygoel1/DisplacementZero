using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public List<SoundLibrary> soundLib = new List<SoundLibrary>();
    public AudioSource audioSource;


    public void PlaySound(string soundName)
    {
        audioSource.PlayOneShot(soundLib.Find(x => x.audioName == soundName).audioClip);
    }

}

[System.Serializable]
public class SoundLibrary
{
    public AudioClip audioClip;
    public string audioName;
}