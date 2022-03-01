

using UnityEngine;

namespace Boby
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] PlayerPrefab;

        public GameObject Spawn(int id)
        {
            GameObject PlayerObject = Instantiate(PlayerPrefab[id]);

            PlayerObject.GetComponent<CharacterController>().enabled = false;
            PlayerObject.transform.position = transform.position;
            PlayerObject.transform.rotation = transform.rotation;
            PlayerObject.GetComponent<CharacterController>().enabled = true;

            return PlayerObject;
        }
    }
}