using UnityEngine;
namespace Boby
{
    class FeatherObj : Projectile
    {
        [SerializeField] float Speed = 2;

        //protected override void OnCollisionEnter(Collision collision)
        //{
        //    BossController boss;
        //    if (!collision.gameObject.TryGetComponent<BossController>(out boss)) return;

        //    boss.CurHP -= 10;
        //    gameObject.SetActive(false);
        //}

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public void Update()
        {
            transform.Translate(direction * Speed * Time.deltaTime, Space.Self);
        }


        private void OnDisable()
        {
        }
    }
}
