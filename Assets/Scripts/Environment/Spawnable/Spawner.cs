using UnityEngine;
using System.Collections;

using Player.Controller;
using Core.Events;

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
            private GameStartEvent gameStartEvent;

            [SerializeField]
            private GameLostEvent gameLostEvent;

            [SerializeField]
            private BaseSpawnable[] spawners;

            private void Awake()
            {
                gameStartEvent.subscribe(onGameStarted);
                gameLostEvent.subscribe(onGameLost);
            }

            private void onGameStarted(float durationUntilStart)
            {
                StartCoroutine(delayAndStart(durationUntilStart));
            }

            private void onGameLost()
            {
                foreach (BaseSpawnable spawner in spawners)
                {
                    spawner.allowSpawning(false);
                }
                foreach (BaseSpawnable spawnedItem in transform.GetComponentsInChildren<BaseSpawnable>())
                {
                    spawnedItem.kill();
                }
            }

            private IEnumerator delayAndStart(float delayDuration)
            {
                yield return new WaitForSeconds(delayDuration);
                foreach (BaseSpawnable spawner in spawners)
                {
                    spawner.allowSpawning(true);
                    StartCoroutine(spawner.spawnCR(modelCamera, playerController, this));
                }
            }
        }
    }
}