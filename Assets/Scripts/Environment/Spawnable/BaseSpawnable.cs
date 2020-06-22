using UnityEngine;
using System.Collections;

using Core.Constant;
using Core.Enumeration;
using Player.Controller;

namespace Environment
{
    namespace Spawnable
    {
        public class BaseSpawnable : MonoBehaviour
        {
            #region Properties
            public StatType Stat
            {
                get
                {
                    return statType;
                }
                private set { }
            }

            public float Impact
            {
                get
                {
                    return statImpact;
                }
                private set { }
            }
            #endregion

            #region Stats Info
            [SerializeField]
            private StatType statType;

            [SerializeField]
            private float statImpact;
            #endregion

            #region Spawning Information
            [SerializeField]
            private float maxMovementSpeed;

            [SerializeField]
            private int maxSpawnAmount;

            [SerializeField]
            private int maxSpawnDelay;

            [SerializeField]
            private float maxSpawnDistance;

            private Camera modelCamera;
            private PlayerController playerController;
            #endregion

            #region MonoBehaviour
            private void OnCollisionEnter(Collision col)
            {
                PlayerController playerController = col.transform.GetComponentInParent<PlayerController>();
                if (playerController != null)
                {
                    playerController.onHit(this);
                    onDestroy();
                }
            }
            #endregion

            #region Callbacks
            public void onSpawnReady(int spawnAmount)
            {
                for (int spawnCounter = 0; spawnCounter <= spawnAmount; spawnCounter++)
                {
                    Vector3 spawnablePosition = modelCamera.ViewportToWorldPoint(new Vector3(Random.value, Random.value, Random.Range(100, maxSpawnDistance)));
                    Vector3 movementDirection = (playerController.transform.position - spawnablePosition).normalized;
                    BaseSpawnable spawnable = Transform.Instantiate(this, spawnablePosition, Quaternion.identity);
                    spawnable.onSpawn(Random.value * maxMovementSpeed, movementDirection);
                }
            }

            private void onSpawn(float movementSpeed, Vector3 movementDirection)
            {
                StartCoroutine(moveCR(movementSpeed, movementDirection));
            }
            private void onDestroy()
            {
                Destroy(gameObject);
            }
            #endregion

            private IEnumerator moveCR(float movementSpeed, Vector3 movementDirection)
            {
                while (true)
                {
                    Vector3 newPosition = transform.position + (movementDirection * movementSpeed / GameDefinitions.FPS);

                    if (newPosition.z <= -10)
                    {
                        onDestroy();
                    }

                    transform.position = newPosition;
                    yield return new WaitForEndOfFrame();
                }
            }

            public IEnumerator spawnCR(Camera camera, PlayerController player)
            {
                modelCamera = camera;
                playerController = player;
                while (maxSpawnDelay > 0)
                {
                    onSpawnReady((int)Random.value * maxSpawnAmount);
                    yield return new WaitForSeconds(Random.value * maxSpawnDelay);
                }

            }
        }
    }
}