using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boby
{
    public class FollowCam : MonoBehaviour
    {
        Transform Tr;
        
        [SerializeField]Transform Target;

//======================================================

        //[Header("Speed")]
        [SerializeField] float MoveSpeed = 1;

        //======================================================
        #region Follow
        //[Header("Pos")]
        [SerializeField] float Back = 9;
        [SerializeField] float UP = 8;
        [SerializeField] float offset = 2;
        [SerializeField] float Zoffset = 0;
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
            camTransform = transform.GetChild(0).GetComponent<Transform>();
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
            TargetPos = Target.position + Vector3.back * Back + Vector3.up * UP + Vector3.forward * Zoffset;
            Tr.localPosition = Vector3.Lerp(Tr.localPosition, TargetPos, MoveSpeed * Time.deltaTime);
            Tr.LookAt(Target.position + Vector3.up * offset);
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
            Gizmos.DrawWireSphere(Target.position + Vector3.up * offset + Vector3.forward * Zoffset, 0.5f);
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.DrawLine(Target.position + Vector3.up * offset + Vector3.forward * Zoffset, transform.position);
        }

        public void SetTarget(Transform Target)
        {
            this.Target = Target;

            Vector3 TargetPos;
            TargetPos = Target.position + Vector3.back * Back + Vector3.up * UP + Vector3.forward * Zoffset;
            Tr.localPosition = TargetPos;
            Tr.LookAt(Target.position + Vector3.up * offset);
        }
    }
}