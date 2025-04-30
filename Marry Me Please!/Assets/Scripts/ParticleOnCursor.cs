using UnityEngine;

public class ParticleOnCursor : MonoBehaviour
{
    public ParticleSystem sakuraSpawner;
    void Start(){
        sakuraSpawner = GetComponent<ParticleSystem>();
        sakuraSpawner.Play();
    }
}
