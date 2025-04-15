using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] Sounds;
    
    void Awake(){
        //For each unique sound create a gameObject of type audio source and instatiate attributes of the sounds class
        foreach(Sound s in Sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name){
        foreach(Sound s in Sounds){
            if(s.name == name){
                s.source.Play();
            }
        }
    }
}
