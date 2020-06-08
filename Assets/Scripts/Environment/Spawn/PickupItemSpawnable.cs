using Player.Data;
using Environment.Core;

using UnityEngine;

namespace Environment
{
    namespace Spawn
    {
        public class PickupItemSpawnable : BaseSpawnable
        {

            void Awake()
            {
                StartCoroutine(countUntilNextSpawnCR(this));
            }
            override protected void hit(StatData statData)
            {
                if (statImpact < 0)
                {
                    statImpact *= -1;
                }
                statData.triggerStatChange(statType, statImpact);
            }

            override protected void onHit()
            {

            }
            override protected void onDestroy()
            {

            }

            override protected bool canSpawn()
            {
                return false;
            }
        }
    }
}