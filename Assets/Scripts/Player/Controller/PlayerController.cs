using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

using Player.Data;
using Player.Utils;

using Environment.Spawnable;

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
            private StatData statData;
            #endregion

            [Header("Internal movement variables - do not change!")]
            [SerializeField]
            private Vector3 currentRotation = new Vector3(0, 0, 0);

            [SerializeField]
            private bool isMoving = false;
            #endregion

            private void LateUpdate()
            {
                PlayerTranslationUtils.adjustForScreenBoundaries(modelCamera, transform.GetChild(0).GetComponent<Collider>());
            }

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
                if (statData != null)
                {
                    model = transform.GetChild(0);
                    statData.init();
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
                while (statData.Alive)
                {
                    statData.triggerNaturalStatChange();
                    if (statData.OxygenLevel <= 0)
                    {
                        statData.triggerDefeat();
                        break;
                    }

                    if (statData.FuelLevel <= 0)
                    {
                        statData.triggerDefeat();
                        break;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }

            public void onHit(BaseSpawnable spawnable)
            {
                statData.triggerStatChange(spawnable.Stat, spawnable.Impact);
                Debug.Log("I am hitting!");
            }

            private void OnCollisionEnter(Collision col)
            {
                BaseSpawnable spawnable = col.transform.GetComponent<BaseSpawnable>();
                if (spawnable != null)
                {
                    onHit(spawnable);
                    Debug.Log("I am being hit!");
                }
            }
        }
    }
}
