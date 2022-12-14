using System.Collections.Generic;
using Source.Scripts.Scriptables.Missiles;
using UnityEngine;

namespace Source.Scripts.Scriptables.MLRS
{
    [CreateAssetMenu(fileName = "MLRS Data", menuName = "MLRS/MLRS", order = 0)]
    public class MlrsData : ScriptableObject
    {
        [SerializeField]private List<AudioClip> _fireSounds;
        [SerializeField] private List<AudioClip> _aimingSounds;
        [SerializeField] private List<Missile> _adoptedMissiles;

        public List<Missile> AdoptedMissiles => _adoptedMissiles;

        public List<AudioClip> FireSounds => _fireSounds;

        public List<AudioClip> AimingSounds => _aimingSounds;
        
        public AudioClip PickRandomFireSound()
        {
            return FireSounds[Random.Range(0, FireSounds.Count - 1)];
        }
        
        public AudioClip PickAimingSound()
        {
            return AimingSounds[0];
        }
    }
}