using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] Sounds;


    void Awake()
    {
        //For each unique sound create a gameObject of type audio source and instatiate attributes of the sounds class
        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Start(){
        Play("In_Game_Music");
    }

    public void Play(string name){
        foreach(Sound s in Sounds){
            if(s.name == name){
                s.source.Play();
                try{
                    s.source.volume = s.volume * SaveLoadManager.Instance.LoadGame().masterVolume;
                }
                catch (Exception e){
                    s.source.volume = s.volume;
                    Debug.LogWarning(e);
                }
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
    }
}
