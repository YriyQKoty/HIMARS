using cakeslice;
using UnityEngine;

namespace Interfaces
{
    public interface IExitable
    {
        Outline Outline { get; }
        
        void Enter(Transform player);

        void Exit(Transform player);
    }
}