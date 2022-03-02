using UnityEngine;

namespace Bobby
{
    public class FOV : MonoBehaviour
    {
        public static Vector3 GetTarget(Transform origin, LayerMask TargetlayerMask, float viewRange, float viewAngle)
        {
            Collider[] Targets = Physics.OverlapSphere(origin.position, viewRange, TargetlayerMask);

            float angle = 999;
            Vector3 Target = Vector3.zero;

            for (int i = 0; i < Targets.Length; i++)
            {
                Vector3 dir = (Targets[i].transform.position - origin.position).normalized;

                if (Vector3.Angle(origin.forward, dir) < viewAngle * 0.5f)
                {
                    if (Vector3.Angle(origin.forward, dir) < angle)
                    {
                        angle = Vector3.Angle(origin.forward, dir);
                        Target = Targets[i].transform.position;
                    }
                }
            }
            return Target;
        }
    }
}
