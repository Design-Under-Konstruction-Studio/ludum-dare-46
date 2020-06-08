using UnityEngine;
using System.Collections;

using Core.Enumeration;
using Player.Data;
using Player.Controller;
using Environment.Scriptable;

namespace Environment
{
    namespace Core
    {
        public abstract class BaseSpawnable : MonoBehaviour
        {
            #region Stats
            [Header("Stats")]
            [SerializeField]
            protected StatType statType;

            [SerializeField]
            protected int statImpact;
            #endregion

            #region Spawn
            [Header("Spawn")]
            [SerializeField]
            protected float spawnFrequencyInSeconds;

            protected OnSpawnReadyScriptable onSpawnReady;
            #endregion

            #region Collision methods
            public void onHit(StatData statData)
            {
                onHit();
                hit(statData);
                onDestroy();
            }

            protected abstract void onHit();
            protected abstract void hit(StatData statData);
            protected abstract void onDestroy();
            #endregion

            #region Spawn methods
            protected abstract bool canSpawn();
            protected IEnumerator countUntilNextSpawnCR(BaseSpawnable spawnableObject)
            {
                yield return new WaitForSeconds(spawnFrequencyInSeconds);
                if (canSpawn())
                {
                    onSpawnReady.trigger(Transform.Instantiate(spawnableObject.gameObject, spawnableObject.transform.position, spawnableObject.transform.rotation));
                }
                StartCoroutine(countUntilNextSpawnCR(spawnableObject));
            }
            #endregion

            void OnCollisionEnter(Collision col)
            {
                PlayerController player = col.gameObject.GetComponentInParent<PlayerController>();
                if (player != null)
                {
                    player.onHit(this);
                }
            }
        }
    }
}