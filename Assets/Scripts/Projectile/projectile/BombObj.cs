using UnityEditor;
using UnityEngine;
using System.Collections;

namespace Bobby
{
    public class BombObj : Projectile
    {
        [SerializeField] protected float FinishTime = 2;
        [SerializeField] protected float MaxHight = 2;
        [SerializeField] protected Vector2 RotateRange;
        [SerializeField] float explosionForce;
        [SerializeField] float explosionRadius = 10;
        [SerializeField] float Upwards_Force;
        [SerializeField] Vector3 Piece_Transform = Vector3.zero;
        [SerializeField] LayerMask TargetLayerMask;

        Transform[] Piece_Position;

        static FollowCam Cam;
        [SerializeField] bool Shake = false;
        [SerializeField] float shakeTime = 1;
        [SerializeField] float ShakeAmount = 0.7f;

        static Vector3[] OriginPos;
        static Quaternion[] OriginRotation;


        Vector3 StartPoint;
        Vector3 Rotate;
        float activeTime = 0;
        bool a = false;
        bool activeRd = false;
        Vector3 velocity = Vector3.zero;
        protected override void OnEnable()
        {
            Piece_Position = GetComponentsInChildren<Transform>();



            if(OriginPos == null)
            {
                Cam = GameObject.Find("Main Camera").GetComponent<FollowCam>();
                OriginPos = new Vector3[Piece_Position.Length];
                OriginRotation = new Quaternion[Piece_Position.Length];
                for (int i = 0; i < Piece_Position.Length; i++)
                {
                    OriginPos[i] = Piece_Position[i].localPosition;
                    OriginRotation[i] = Piece_Position[i].localRotation;
                }
            }

            a = false;
            activeRd = false;
            StartPoint = transform.position;
            activeTime = 0;
            Rotate = new Vector3(Random.Range(RotateRange.x,RotateRange.y),Random.Range(RotateRange.x,RotateRange.y));
        }

        public void Update()
        {
            if (a) return;
            if (activeRd) return;

            activeTime += Time.deltaTime;
            activeTime = Mathf.Clamp(activeTime, 0, FinishTime);

            if (activeTime == FinishTime)
            {
                activeRd = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GetComponent<Rigidbody>().velocity = velocity;
                return;
            }

            float per = activeTime / FinishTime;
            Vector3 pos = Vector3.Slerp(StartPoint,target,per);

            velocity = (pos - transform.position) / Time.deltaTime ;

            transform.position = pos;
            transform.Rotate(Rotate * Time.deltaTime);
            
        }
        
        private void OnDisable()
        {
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            if (a) return;
            a = true;

            if (Shake)
            {
                Cam.Shake(shakeTime, ShakeAmount);
            }

            Rigidbody rd = GetComponent<Rigidbody>();

            Collider[] coll = Physics.OverlapSphere(transform.position, explosionRadius, TargetLayerMask);
            IDamageable damage;
            for (int i = 0; i < coll.Length; i++)
            {
                if (coll[i].transform.parent.TryGetComponent(out damage))
                {
                    damage.TakeDamage(gameObject, Damage, DamageType);
                }
            }
            

            Rigidbody[] Piece_Rigidbody = new Rigidbody[Piece_Position.Length];

            for (int i = 1; i < Piece_Rigidbody.Length; i++)
            {
                Piece_Rigidbody[i] = Piece_Position[i].gameObject.AddComponent<Rigidbody>();
                Piece_Rigidbody[i].AddExplosionForce(explosionForce, transform.position + Piece_Transform, explosionRadius,Upwards_Force);
                Piece_Rigidbody[i].velocity += velocity/2;
                Piece_Rigidbody[i].gameObject.layer = 12;

            }

            rd.constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(Reset());
        }
        protected IEnumerator Reset()
        {
            yield return new WaitForSeconds(5);
            Rigidbody[] Piece_Rigidbody = new Rigidbody[Piece_Position.Length];
            for (int i = 1; i < Piece_Position.Length; i++)
            {
                Destroy(Piece_Position[i].gameObject.GetComponent<Rigidbody>());
                Piece_Position[i].localPosition = OriginPos[i];
                Piece_Position[i].localRotation = OriginRotation[i];
                Piece_Position[i].gameObject.layer = 16;
            }
            gameObject.SetActive(false);
            OnComplete(gameObject);
        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}