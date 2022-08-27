using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.MLRSCore.FireCore
{
    public class TubesContainer : MonoBehaviour
    {
        [SerializeField] private List<FireTube> _fireTubes;

        public List<FireTube> FireTubes => _fireTubes;
    }
}