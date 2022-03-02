using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Bobby
{
    public class Projectile : PoolObj, IDamageCauser
    {
        [SerializeField] protected float Damage;
        [SerializeField] protected DamageType DamageType;

        public Complete OnComplete;
        protected GameObject launcher;  //  발사체를 발사한 오브젝트
        protected Vector3 direction;    // 발사체가 나아가야할 방향
        protected Vector3 target;

        protected virtual void OnCollisionEnter(Collision collision)
        {
            IDamageable damageable = collision.transform.GetComponent<IDamageable>();
            damageable?.TakeDamage(gameObject, Damage, DamageType);

            OnComplete(gameObject);
        }

        protected override IEnumerator ReturnObj(GameObject obj)
        {
            yield return base.ReturnObj(obj);
            OnComplete(gameObject);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void SetDirection(Vector3 direction)
        {
            this.direction = direction;
        }
        public void SetLauncher(GameObject launcher)
        {
            this.launcher = launcher;
        }
        public void SetTarget(Vector3 target)
        {
            this.target = target;
        }

        public GameObject GetCauser()
        {
            return launcher;
        }
    }
}