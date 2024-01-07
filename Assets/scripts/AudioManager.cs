/*
@Author - Patrick
@Description - Handles all audio
*/

using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    
    void Awake(){
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        foreach(Sound s in sounds){
            s.src = gameObject.AddComponent<AudioSource>();
            s.src.clip = s.clip;
            s.src.volume = s.volume;
            s.src.pitch = s.pitch;
            s.src.Stop();
            s.src.loop = s.loop;
        }
    }
    
    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            print($"Sound not found: {soundName}");
            return;
        }
        s.src.Play();
    }
    public void Stop(string soundName){
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            print($"Sound not found: {soundName}");
            return;
        }
        s.src.Stop();
    }

    public static AudioManager instance;
}