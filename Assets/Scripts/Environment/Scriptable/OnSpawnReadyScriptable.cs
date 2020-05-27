using UnityEngine;

using Environment.Delegate;

namespace Environment
{
    namespace Scriptable
    {
        [CreateAssetMenu(fileName = "Spawn Event", menuName = "Environment/Spawn Event", order = 1)]
        public class OnSpawnReadyScriptable : ScriptableObject
        {
            private OnSpawnReady onSpawnReady;

            #region Delegate methods
            public void subscribe(OnSpawnReady func)
            {
                onSpawnReady += func;
            }

            public void unsubscribe(OnSpawnReady func)
            {
                onSpawnReady -= func;
            }

            public void trigger(GameObject spawnedItem)
            {
                if (onSpawnReady != null)
                {
                    onSpawnReady(spawnedItem);
                }
            }
            #endregion
        }
    }
}