using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance { get; private set; }

    public List<Sound> sounds = new List<Sound>();
    public AudioSource mainSource = null;

    void Start()
    {
        instance = this;
    }


    public void Play(string soundName)
    {
        foreach (Sound sound in sounds)
        {
            if (sounds.Contains(sound))
            {
                if (sound._name == soundName)
                {
                    mainSource.PlayOneShot(sound.clip, sound.volume);
                }
            }
        }
    }
}
