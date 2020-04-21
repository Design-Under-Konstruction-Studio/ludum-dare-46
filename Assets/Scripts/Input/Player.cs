using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

using Core;

namespace Input
{
    public class Player : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float translationSpeed = 5.0f;

        [Header("Inclination")]
        [SerializeField]
        private float rotationSpeed = 15.0f;

        [SerializeField]
        private float maxRotation = 45.0f;

        [Header("Limits")]
        [SerializeField]
        private Camera camera;

        private bool isMoving = false;
        private float distancePerFrame;
        private float rotationPerFrame;
        private float currentRotation;

        private Vector2 screenBounds;
        private Vector3 modelPosition;

        void LateUpdate()
        {
            modelPosition = camera.WorldToViewportPoint(transform.position);
            modelPosition.x = Mathf.Clamp01(modelPosition.x);
            modelPosition.y = Mathf.Clamp01(modelPosition.y);
            transform.position = camera.ViewportToWorldPoint(modelPosition);
        }

        private void Awake()
        {
            distancePerFrame = translationSpeed / GameDefinitions.FPS;
            rotationPerFrame = rotationSpeed / GameDefinitions.FPS;

            /* screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Mathf.Abs(camera.transform.position.z + transform.position.z)));
            modelSize = new Vector2(GetComponent<BoxCollider>().size.x, GetComponent<BoxCollider>().size.y); */

            // Vector2 bounds = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            // Debug.Log("Screen : " + Screen.width + " " + Screen.height);
            // Debug.Log("Bounds : " + screenBounds.x + " " + screenBounds.y);

            // currentRotation = 0; */
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
                //twist(moveDirection);
                yield return new WaitForEndOfFrame();
            }
        }

        /* private void twist(Vector2 moveDirection)
        {
            if (currentRotation >= -maxRotation && currentRotation <= maxRotation)
            {
                currentRotation += rotationPerFrame * moveDirection.x;
                transform.Rotate(0, 0, currentRotation);
            }
        } */

        /*
                private IEnumerator moveOnwardsCR()
                {
                    transform.Translate(0, getDistancePerFrame(), 0);
                    yield return new WaitForSeconds(1 / GameDefinitions.FPS);
                    StartCoroutine(moveOnwardsCR());
                }

                private IEnumerator moveBackwardsCR()
                {
                    transform.Translate(0, -getDistancePerFrame(), 0);
                    yield return new WaitForSeconds(1 / GameDefinitions.FPS);
                    StartCoroutine(moveBackwardsCR());

                }

                private IEnumerator turnLeftCR()
                {
                    transform.Rotate(0, 0, getAnglePerFrame());
                    yield return new WaitForSeconds(1 / GameDefinitions.FPS);
                    StartCoroutine(turnLeftCR());
                }

                private IEnumerator turnRightCR()
                {
                    transform.Rotate(0, 0, -getAnglePerFrame());
                    yield return new WaitForSeconds(1 / GameDefinitions.FPS);
                    StartCoroutine(turnRightCR());
                }
        */
    }
}
