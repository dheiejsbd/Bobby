using UnityEngine;
using UnityEditor;

namespace Boby
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] protected Transform SpawnTransform;
        [SerializeField] protected GameObject ProjectilePrefab;

        Pool pool;

        public void Fire(Transform targetTranform)
        {
            if (!targetTranform) return;
            Vector3 a = transform.position; 
            Vector3 b = new Vector3(targetTranform.position.x, a.y, targetTranform.position.z);

            Vector3 dir = ( b- a).normalized;
            Fire(dir, targetTranform.position);
        }

        public void Fire(Vector3 direction)
        {
            if (pool == null)
            {
                pool = new Pool(ProjectilePrefab);
            }

            Projectile projectile = GetObj<Projectile>();
            projectile.transform.position = SpawnTransform.position;
            projectile.transform.rotation = SpawnTransform.rotation;
            projectile.transform.forward = direction;
            projectile.SetDirection(direction);
            projectile.SetLauncher(gameObject);
            projectile.Activate();
        }

        public void Fire(Vector3 direction, Vector3 target)
        {
            if (pool == null)
            {
                pool = new Pool(ProjectilePrefab);
            }

            Projectile projectile = GetObj<Projectile>();
            projectile.transform.position = SpawnTransform.position;
            projectile.transform.rotation = SpawnTransform.rotation;
            projectile.transform.forward = direction;
            projectile.SetDirection(direction);
            projectile.SetLauncher(gameObject);
            projectile.SetTarget(target);
            projectile.Activate();
        }


        private GameObject GetObj()
        {
            GameObject ProjectileObject = pool.Pop();
            Projectile projectile = ProjectileObject.GetComponent<Projectile>();
            projectile.OnComplete = Complete;
            return ProjectileObject;
        }

        private T GetObj<T>() where T : Projectile
        {
            GameObject ProjectileObject = pool.Pop();
            Projectile projectile = ProjectileObject.GetComponent<Projectile>();
            projectile.OnComplete = Complete;
            return (T)projectile;
        }

        protected void Complete(GameObject porjecile)
        {
            pool.Push(porjecile);
        }

        private void OnDestroy()
        {
            if(Pool.PoolParent != null)
            {
                Destroy(Pool.PoolParent.gameObject);
            }
        }
    }
}