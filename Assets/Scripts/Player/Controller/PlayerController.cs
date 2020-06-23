using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

using Core.Events;
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

            #region Events
            [Header("Events")]
            [SerializeField]
            private GameStartEvent gameStartEvent;

            [SerializeField]
            private GameLostEvent gameLostEvent;
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

            private void onGameStarted(float durationUntilStart)
            {
                StartCoroutine(delayAndStart(durationUntilStart));
            }

            private IEnumerator delayAndStart(float delayDuration)
            {
                yield return new WaitForSeconds(delayDuration);
                if (statData != null)
                {
                    model = transform.GetChild(0);
                    statData.init();
                    StartCoroutine(decreaseStatsNaturally());
                }
            }

            private void Awake()
            {
                gameStartEvent.subscribe(onGameStarted);
            }

            private IEnumerator move(Vector2 moveDirection)
            {
                while (isMoving && statData.Initialized)
                {
                    processTranslation(moveDirection);
                    processRotation(moveDirection);
                    yield return new WaitForEndOfFrame();
                }
            }

            private void processTranslation(Vector2 moveDirection)
            {
                float movementSpeed = statData.FuelLevel <= statData.FuelLevelThreshold ?
                    movementData.DistancePerFrame * statData.LowFuelSpeedModifier :
                    movementData.DistancePerFrame;
                transform.Translate(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed, 0);
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
                        gameLostEvent.trigger();
                        break;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }

            public void onHit(BaseSpawnable spawnable)
            {
                statData.triggerStatChange(spawnable.Stat, spawnable.Impact);
            }

            private void OnCollisionEnter(Collision col)
            {
                BaseSpawnable spawnable = col.transform.GetComponent<BaseSpawnable>();
                if (spawnable != null)
                {
                    onHit(spawnable);
                }
            }
        }
    }
}
