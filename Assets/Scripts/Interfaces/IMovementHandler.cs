using UnityEngine;
using UnityEditor;

namespace Boby
{
    public interface IMovementHandler
    {
        void SetSpeed(float speed);

        void Resume();
        void Stop();
        void SetDestination(Vector3 destination);
    }
}