using UnityEngine;

namespace Boby
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] GameObject BossPrefab;

        public GameObject Spawn()
        {
            GameObject BossObject = Instantiate(BossPrefab);
            BossObject.transform.position = transform.position;
            BossObject.transform.rotation = transform.rotation;

            return BossObject;
        }
    }
}