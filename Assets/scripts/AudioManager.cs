/*
@Author - Patrick
@Description - Handles all audio
*/

using System;
using UnityEngine;

public class AudioManager : MonoBehaviour{
    public Sound[] sounds;
    
    void Awake(){
        instance = this;
        foreach(Sound s in sounds){
            s.src = gameObject.AddComponent<AudioSource>();
            s.src.clip = s.clip;
            s.src.volume = s.volume;
            s.src.pitch = 1;
            s.src.loop = s.loop;
            s.src.Stop();
        }
    }
    
    public void Play(string soundName)
    {
        if (soundName == "")
        {
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        s.src.Play();
    }
    public void Stop(string soundName){
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        s.src.Stop();
    }

    public static AudioManager instance;
}