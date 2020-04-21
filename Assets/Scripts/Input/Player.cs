using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

using Core.Constant;

namespace Input
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
        private float rotationSpeed = 15.0f;

        [SerializeField]
        private float maxRotation = 15.0f;

        private float rotationPerFrame;

        private Vector3 currentRotation = new Vector3(0, 0, 0);

        private Transform model;
        #endregion

        #region Movement limits
        [Header("Movement limits")]
        [SerializeField]
        private Camera camera;

        [SerializeField]
        private Vector2 minXY = new Vector2(0f, 0f);

        [SerializeField]
        private Vector2 maxXY = new Vector2(1.0f, 1.0f);

        #endregion

        void LateUpdate()
        {
            Vector3 modelPosition = camera.WorldToViewportPoint(transform.position);
            modelPosition.x = Mathf.Clamp01(modelPosition.x);
            modelPosition.y = Mathf.Clamp01(modelPosition.y);
            transform.position = camera.ViewportToWorldPoint(modelPosition);
        }

        private void Awake()
        {
            distancePerFrame = translationSpeed / GameDefinitions.FPS;
            rotationPerFrame = rotationSpeed / GameDefinitions.FPS;

            model = transform.GetChild(0);
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
            Vector3 futureRotation = new Vector3(0, 0, 0);

            futureRotation.x = moveDirection.y * rotationPerFrame;
            futureRotation.z = moveDirection.x * rotationPerFrame;

            model.Rotate(futureRotation);
        }
    }
}
