using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Sound[] sounds;

    private bool isSfxMuted;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }

    public void SoundEffectMute()
    {
        isSfxMuted = false;
    }

    public void BackgroundMusicOff()
    {


        Mute("MainTheme");
    }

    public void SoundEffectOn()
    {
        isSfxMuted = true;
    }

    public void BackgroundMusicOn()
    {
        Play("MainTheme");
    }

    public void Play(string name)
    {
        Sound s =Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            return;
        } else
        {
            if(s.isSfx && isSfxMuted)
            {
                return;
            }else
            {
                s.source.Play();
            }
        }
    }

 
    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        else
        {
            s.source.Stop();
        }
    }

    
}
