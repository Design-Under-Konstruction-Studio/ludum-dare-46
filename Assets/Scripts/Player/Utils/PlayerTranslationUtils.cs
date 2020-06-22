using UnityEngine;

using Core.Utils;

namespace Player
{
    namespace Utils
    {
        public class PlayerTranslationUtils
        {
            public static void adjustForScreenBoundaries(Camera camera, Collider modelCollider)
            {
                //TODO: Optimize both matematically and programatically
                Vector3 modelMin = camera.WorldToViewportPoint(modelCollider.bounds.min);
                Vector3 modelMax = camera.WorldToViewportPoint(modelCollider.bounds.max);

                Vector3 modelPosition = camera.WorldToViewportPoint(modelCollider.transform.position);

                Vector3 modelDistancesMin = modelPosition - modelMin;
                Vector3 modelDistancesMax = modelMax - modelPosition;

                if (modelMax.x >= 1)
                {
                    modelPosition.x = 1 - modelDistancesMax.x;
                }
                if (modelMin.x <= 0)
                {
                    modelPosition.x = 0 + modelDistancesMin.x;
                }

                if (modelMax.y >= 1)
                {
                    modelPosition.y = 1 - modelDistancesMax.y;
                }
                if (modelMin.y <= 0)
                {
                    modelPosition.y = 0 + modelDistancesMin.y;
                }

                modelCollider.transform.position = camera.ViewportToWorldPoint(modelPosition);
            }

            private PlayerTranslationUtils()
            { }
        }
    }
}