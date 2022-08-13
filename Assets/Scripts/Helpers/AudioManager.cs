using System.Collections.Generic;
using MLRSCore.Sciptables;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private List<MLRS> _MLRS;

    [SerializeField] private MLRS _currentMLRS;

    public List<MLRS> Mlrs => _MLRS;
    public MLRS CurrentMlrs => _currentMLRS;

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
