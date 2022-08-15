using System.Collections.Generic;
using Scriptables.MLRS;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private List<MlrsData> _MLRS;

    [SerializeField] private MlrsData currentMlrsData;

    public List<MlrsData> Mlrs => _MLRS;
    public MlrsData CurrentMlrsData => currentMlrsData;

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
}
