using cakeslice;
using UnityEngine;

namespace Source.Scripts.Interfaces
{
    public interface IExitable
    {
        Outline Outline { get; }
        
        void Enter(Transform player);

        void Exit(Transform player);
    }
}