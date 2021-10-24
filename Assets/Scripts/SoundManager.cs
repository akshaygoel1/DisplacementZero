using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public List<SoundLibrary> soundLib = new List<SoundLibrary>();
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void PlaySound(string soundName)
    {
        audioSource.PlayOneShot(soundLib[0].audioClip);
    }

}

[System.Serializable]
public class SoundLibrary
{
    public AudioClip audioClip;
    public string audioName;
}