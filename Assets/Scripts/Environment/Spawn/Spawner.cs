using UnityEngine;

using Environment.Core;
using Environment.Scriptable;

namespace Environment
{
    namespace Spawn
    {
        public class Spawner : MonoBehaviour
        {
            #region Spawnables
            [Header("Spawnables")]
            [SerializeField]
            private BaseSpawnable[] spawnableItems;
            #endregion

            #region Event
            [Header("Event")]
            [SerializeField]
            private OnSpawnReadyScriptable spawnEvent;
            #endregion

            void Awake()
            {
                spawnEvent.subscribe(onSpawnReady);
            }

            private void onSpawnReady(GameObject spawnedItem)
            {

            }
        }
    }
}