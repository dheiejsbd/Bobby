using UnityEngine;
using UnityEditor;

namespace Boby
{
    public partial class MonsterController : IMovementHandler
    {
        public void SetSpeed(float speed)
        {
            navMeshAgent.speed = speed;
        }

        public void Resume()
        {
            navMeshAgent.isStopped = false;
        }
        void IMovementHandler.Stop()
        {
            navMeshAgent.isStopped = true;
        }
        public void SetDestination(Vector3 destination)
        {
            Resume();
            navMeshAgent.SetDestination(destination);
        }
    }
}