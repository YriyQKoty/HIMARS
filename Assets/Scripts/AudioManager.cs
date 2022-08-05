using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private List<AudioClip> _fireSounds;

    public List<AudioClip> FireSounds => _fireSounds;
    
    public static AudioManager Instance { get; private set; }

    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    public AudioClip PickRandomFireSound()
    {
        return FireSounds[Random.Range(0, FireSounds.Count - 1)];
    }

}
