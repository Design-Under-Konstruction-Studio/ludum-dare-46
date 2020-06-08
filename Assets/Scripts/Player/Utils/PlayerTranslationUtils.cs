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
                Debug.Log("modelMin " + modelMin);
                Debug.Log("modelMax " + modelMax);
                Debug.Log(" ");

                Vector3 modelPosition = camera.WorldToViewportPoint(modelCollider.transform.position);
                Debug.Log("modelPosition " + modelPosition);
                Debug.Log(" ");

                Vector3 modelDistancesMin = modelPosition - modelMin;
                Vector3 modelDistancesMax = modelMax - modelPosition;
                Debug.Log("modelDistancesMin " + modelDistancesMin);
                Debug.Log("modelDistancesMax " + modelDistancesMax);
                Debug.Log(" ");

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
                /*0.8918822
                4.859679
                                if (modelDistancesMax.z >= 1)
                                {
                                    modelPosition.z = 1 - modelDistancesMax.z;
                                }
                                if (modelDistancesMin.z <= 0)
                                {
                                    modelPosition.z = 0 + modelDistancesMin.z;
                                }
                */
                modelCollider.transform.position = camera.ViewportToWorldPoint(modelPosition);
            }

            private PlayerTranslationUtils()
            { }
        }
    }
}