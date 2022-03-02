using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public interface IState
    {
        int Id { get; }

        void Enter();
        void Execute();
        void Exit();
    }
}
