using UnityEditor;
using UnityEngine;

namespace Boby
{
    public class BossCam : MonoBehaviour
    {
        Transform Tr;
        [SerializeField] Transform Target;
        [SerializeField] Transform PlayerTr;

        //======================================================

        //[Header("Speed")]
        [SerializeField] float TurnSpeed = 1;

        //======================================================
        #region Follow
        //[Header("Pos")]
        [SerializeField] float Radius = 9;
        [SerializeField] float CameraY = 8;
        [SerializeField] Vector3 offset = Vector3.zero;
        #endregion

        #region Shake
        Transform camTransform;
        float shakeTimer = 0;

        [SerializeField] float shakeAmount = 0.7f;
        Vector3 originalPos = Vector3.zero;
        #endregion


        void Awake()
        {
            Tr = GetComponent<Transform>();
            camTransform = Tr.GetChild(0).GetComponent<Transform>();
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Move();
            Shake();
        }

        void Move()
        {
            if (Target == null || Target.gameObject.activeSelf == false/*missingObject*/) return;

            Vector3 TargetPos;
            float CameraAngle = GetAngle(Tr.position - Target.position);
            float TargetAngle = Mathf.Lerp(CameraAngle, GetTargetAngle(), TurnSpeed * Time.deltaTime);
            TargetPos = GetTargetPos(TargetAngle) + Target.position;
            Tr.position = TargetPos;

            Vector3 LookAtPos = Target.position + offset;
            Tr.LookAt(LookAtPos);
        }

        float GetTargetAngle()
        {
            float CameraAngle = GetAngle(Tr.position - Target.position);
            float TarGetAngle = GetAngle(PlayerTr.position - Target.position);
            if (Mathf.Abs(TarGetAngle - CameraAngle) > 180)
            {
                if ((TarGetAngle - CameraAngle) < 0)
                {
                    return 360 - TarGetAngle - CameraAngle;
                }
            }


            return TarGetAngle - CameraAngle;
        }


        float GetAngle(Vector3 Target)
        {
            float angle = Mathf.Atan2(Target.z, Target.x) * 180 / Mathf.PI;

            return angle;
        }


        Vector3 GetTargetPos(float Angle)
        {
            float x = Mathf.Cos(Angle) * Radius;
            float z = Mathf.Sin(Angle) * Radius;

            return new Vector3(x, CameraY, z);
        }






        void Shake()
        {
            if (shakeTimer > 0)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeTimer -= Time.deltaTime;
            }
            else
            {
                shakeTimer = 0f;
                camTransform.localPosition = originalPos;
            }
        }

        public void Shake(float Time)
        {
            shakeTimer = Time;
        }

        public void Shake(float Time, float shakeAmount)
        {
            shakeTimer = Time;
            this.shakeAmount = shakeAmount;
        }

        private void OnDrawGizmos()
        {
            if (Target == null || Target.gameObject.activeSelf == false/*missingObject*/) return;

            Gizmos.color = Color.green;
            
        }
        /*
        public void SetTarget(Transform Target)
        {
            this.Target = Target;

            Vector3 TargetPos;
            TargetPos = Target.position + Vector3.back * Back + Vector3.up * UP + Vector3.forward * Zoffset;
            Tr.localPosition = TargetPos;
            Tr.LookAt(Target.position + Vector3.up * offset);
        }*/
    }
}
