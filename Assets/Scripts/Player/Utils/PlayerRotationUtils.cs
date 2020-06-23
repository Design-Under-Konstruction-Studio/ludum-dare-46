using UnityEngine;
using System;

using Core.Constant;
using Player.Data;

namespace Player
{
    namespace Utils
    {
        public class PlayerRotationUtils
        {
            public static Vector3 provideNextRotation(MovementData movementData, Vector2 moveDirection, Vector3 currentRotation)
            {
                float maxRotation = movementData.MaxRotation;
                float rotationPerFrame = movementData.RotationPerFrame;

                Vector3 futureRotation = new Vector3(0, 0, 0);

                if (
                    (currentRotation.z < -maxRotation && moveDirection == Vector2.left) ||
                    (currentRotation.z > maxRotation && moveDirection == Vector2.right) ||
                    (currentRotation.x < -maxRotation && moveDirection == Vector2.down) ||
                    (currentRotation.x > maxRotation && moveDirection == Vector2.up))
                {
                    return new Vector3(0, 0, 0);
                }

                futureRotation.x = moveDirection.y * rotationPerFrame;
                futureRotation.z = moveDirection.x * rotationPerFrame;

                return futureRotation;
            }

            private PlayerRotationUtils()
            {
                throw new System.Exception();
            }
        }
    }
}