using UnityEngine;
using UnityEditor;

namespace Boby
{
    public interface IState
    {
        int Id { get; }

        void Enter();
        void Execute();
        void Exit();
    }
}
