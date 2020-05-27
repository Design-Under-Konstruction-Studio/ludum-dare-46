using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

using Player.Data;
using Player.Utils;

using Environment.Core;

namespace Player
{
    namespace Controller
    {
        public class PlayerController : MonoBehaviour
        {
            #region Physics
            private Transform model;
            #endregion

            #region Movement
            [Header("Movement")]
            [SerializeField]
            private MovementData movementData;

            #region Movement limits
            [Header("Movement limits")]
            [SerializeField]
            private Camera modelCamera;
            #endregion

            #region Stats
            [Header("Stats")]
            [SerializeField]
            private PlayerData playerData;
            #endregion

            [Header("Internal movement variables - do not change!")]
            [SerializeField]
            private Vector3 currentRotation = new Vector3(0, 0, 0);

            [SerializeField]
            private bool isMoving = false;
            #endregion

            public void controlMovement(InputAction.CallbackContext ctx)
            {
                if (ctx.phase == InputActionPhase.Started)
                {
                    isMoving = true;
                    StartCoroutine(move(ctx.ReadValue<Vector2>()));
                }
                else if (ctx.phase == InputActionPhase.Canceled)
                {
                    isMoving = false;
                }
            }

            private void Awake()
            {
                if (playerData != null)
                {
                    model = transform.GetChild(0);
                    playerData.init();
                    StartCoroutine(decreaseStatsNaturally());
                }
            }

            private IEnumerator move(Vector2 moveDirection)
            {
                while (isMoving)
                {
                    processTranslation(moveDirection);
                    processRotation(moveDirection);
                    yield return new WaitForEndOfFrame();
                }
            }

            private void processTranslation(Vector2 moveDirection)
            {
                transform.Translate(moveDirection.x * movementData.DistancePerFrame, moveDirection.y * movementData.DistancePerFrame, 0);
            }

            private void processRotation(Vector2 moveDirection)
            {
                Vector3 nextRotation = PlayerRotationUtils.provideNextRotation(movementData, moveDirection, currentRotation);
                currentRotation += nextRotation;
                model.Rotate(nextRotation);
            }

            private IEnumerator decreaseStatsNaturally()
            {
                while (playerData.Alive)
                {
                    playerData.triggerNaturalStatChange();
                    if (playerData.OxygenLevel <= 0)
                    {
                        playerData.triggerDefeat();
                        break;
                    }

                    if (playerData.FuelLevel <= 0)
                    {
                        playerData.triggerDefeat();
                        break;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }

            public void onHit(BaseSpawnable spawnable)
            {
                if (spawnable != null)
                {
                    spawnable.onHit(playerData);
                }
            }
        }
    }

}
