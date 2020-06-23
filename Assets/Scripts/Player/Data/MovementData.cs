using UnityEngine;

using Core.Constant;

namespace Player
{
    namespace Data
    {
        [CreateAssetMenu(fileName = "Movement Data", menuName = "Data/Movement Data", order = 0)]
        public class MovementData : ScriptableObject
        {
            #region Inclination
            #region Properties
            public float RotationPerFrame
            {
                get
                {
                    return rotationSpeed / GameDefinitions.FPS;
                }
                private set
                {

                }
            }

            public float RotationSpeed
            {
                get
                {
                    return rotationSpeed;
                }
                private set
                {

                }
            }

            public float MaxRotation
            {
                get
                {
                    return maxRotation;
                }
                private set
                {

                }
            }
            #endregion

            [Header("Inclination")]
            [SerializeField]
            private float rotationSpeed = 5.0f;

            [SerializeField]
            private float maxRotation = 15.0f;
            #endregion

            #region Translation
            #region Properties
            public float TranslationSpeed
            {
                get
                {
                    return translationSpeed;
                }
                private set
                {

                }
            }

            public float DistancePerFrame
            {
                get
                {
                    return translationSpeed / GameDefinitions.FPS; ;
                }
                private set
                {

                }
            }
            #endregion

            [Header("Translation")]
            [SerializeField]
            private float translationSpeed = 5.0f;

            [Header("Internal calculation only - do not change!")]
            [SerializeField]
            private Vector3 screenLimitsMax;

            [SerializeField]
            private Vector3 screenLimitsMin;
            #endregion

            private MovementData()
            { }
        }
    }
}