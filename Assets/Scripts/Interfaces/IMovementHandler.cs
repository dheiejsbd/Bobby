using UnityEngine;
using UnityEditor;

namespace Bobby
{
    public interface IMovementHandler
    {
        void SetSpeed(float speed);

        void Resume();
        void Stop();
        void SetDestination(Vector3 destination);
    }
}