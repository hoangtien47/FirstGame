using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Play("Theme");
        Play("AmbientSound");
    }

    public void Play(string name)
    {
        Sound s =Array.Find(sounds,sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " +  name +" not found");
            return;
        }
        s.source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;


    public bool loop;

    [HideInInspector]
    public AudioSource source;
}