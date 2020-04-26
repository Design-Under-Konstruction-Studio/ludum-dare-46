using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

using Core.Constant;
using Core.Data;
namespace Controller
{
    public class Player : MonoBehaviour
    {
        #region Movement
        [Header("Movement")]
        [SerializeField]
        private float translationSpeed = 5.0f;

        private bool isMoving = false;
        private float distancePerFrame;
        #endregion

        #region Inclination
        [Header("Inclination")]
        [SerializeField]
        private float rotationSpeed = 5.0f;

        [SerializeField]
        private float maxRotation = 15.0f;

        private float rotationPerFrame;
        private Vector3 currentRotation = new Vector3(0, 0, 0);
        private Transform model;
        [SerializeField]
        private Vector2 modelSize;
        #endregion

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

        private void LateUpdate()
        {
            Vector2 minXY = modelCamera.WorldToViewportPoint(new Vector2(transform.position.x - modelSize.x, transform.position.y - modelSize.y));
            Vector2 maxXY = modelCamera.WorldToViewportPoint(new Vector2(transform.position.x + modelSize.x, transform.position.y + modelSize.y));

            Debug.Log("min " + minXY.x + " " + minXY.y);
            Debug.Log("max " + maxXY.x + " " + maxXY.y);

            Vector3 modelPosition = modelCamera.WorldToViewportPoint(transform.position);
            Debug.Log("modelPosition " + modelPosition.x + " " + modelPosition.y + " " + modelPosition.z);
            if (minXY.x < 0)
            {
                modelPosition.x = Mathf.Clamp01(modelPosition.x - minXY.x);
            }
            else if (maxXY.x > 1)
            {
                modelPosition.x = Mathf.Clamp01(modelPosition.x + maxXY.x);
            }

            if (minXY.y < 0)
            {
                modelPosition.y = Mathf.Clamp01(modelPosition.y - minXY.y);
            }
            else if (maxXY.y > 1)
            {
                modelPosition.y = Mathf.Clamp01(modelPosition.y + maxXY.y);
            }

            transform.position = modelCamera.ViewportToWorldPoint(modelPosition);
        }

        private void Awake()
        {
            distancePerFrame = translationSpeed / GameDefinitions.FPS;
            rotationPerFrame = rotationSpeed / GameDefinitions.FPS;

            model = transform.GetChild(0);
            modelSize = model.GetComponent<BoxCollider>().size;

            if (playerData != null)
            {
                playerData.init();
                StartCoroutine(decreaseStatsNaturally());
            }
        }

        private IEnumerator move(Vector2 moveDirection)
        {
            while (isMoving)
            {
                transform.Translate(moveDirection.x * distancePerFrame, moveDirection.y * distancePerFrame, 0);
                twist(moveDirection);
                yield return new WaitForEndOfFrame();
            }
        }

        private void twist(Vector2 moveDirection)
        {
            if (
                (currentRotation.z < -maxRotation && moveDirection == Vector2.left) ||
                (currentRotation.z > maxRotation && moveDirection == Vector2.right) ||
                (currentRotation.x < -maxRotation && moveDirection == Vector2.down) ||
                (currentRotation.x > maxRotation && moveDirection == Vector2.up))
            {
                return;
            }

            Vector3 futureRotation = new Vector3(0, 0, 0);

            futureRotation.x = moveDirection.y * rotationPerFrame;
            futureRotation.z = moveDirection.x * rotationPerFrame;

            currentRotation += futureRotation;

            model.Rotate(futureRotation);
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
    }
}
