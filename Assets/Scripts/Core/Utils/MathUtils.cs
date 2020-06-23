using UnityEngine;
using System;

namespace Core
{
    namespace Utils
    {
        public class MathUtils
        {
            private MathUtils()
            {
                throw new Exception();
            }

            public static float reduce(Vector3 redux, Vector3 direction)
            {
                redux.Scale(direction);
                return Mathf.Sqrt(redux.magnitude);
            }

            public static float reduce(Vector2 redux, Vector2 direction)
            {
                redux.Scale(direction);
                return Mathf.Sqrt(redux.magnitude);
            }
        }
    }
}