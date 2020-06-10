using UnityEngine;

using Player.Controller;

namespace Environment
{
    namespace Spawnable
    {
        public class Spawner : MonoBehaviour
        {
            [SerializeField]
            private Camera modelCamera;

            [SerializeField]
            private PlayerController playerController;

            [SerializeField]
            private BaseSpawnable[] spawners;

            private void Awake()
            {
                foreach (BaseSpawnable spawner in spawners)
                {
                    StartCoroutine(spawner.spawnCR(modelCamera, playerController));
                }
            }
        }
    }
}