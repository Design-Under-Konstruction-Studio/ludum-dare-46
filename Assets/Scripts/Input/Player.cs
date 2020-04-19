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

        private bool[] isMoving = new bool[4];
        private float distancePerFrame;
        private float rotationPerFrame;
        private float currentRotation;

        private Vector2 screenBounds;
        private float width;
        private float height;

        /* void LateUpdate()
        {
            var posY = transform.position.y * 2;
            var posX = transform.position.x * 2;
            var position = camera.WorldToViewportPoint(new Vector2(posX, posY));
            position.x = Mathf.Clamp01(position.x);
            position.y = Mathf.Clamp01(position.y);
            transform.position = camera.ViewportToWorldPoint(position);
        } */

        private void Awake()
        {
            distancePerFrame = translationSpeed / GameDefinitions.FPS;
            rotationPerFrame = rotationSpeed / GameDefinitions.FPS;
            // currentRotation = 0;
        }

        public void controlMovement(InputAction.CallbackContext ctx)
        {
            var moveDirection = ctx.ReadValue<Vector2>();

            if (ctx.phase == InputActionPhase.Started)
            {
                isMoving[(int)getDirection(moveDirection)] = true;
                StartCoroutine(move(moveDirection, getDirection(moveDirection)));
            }
            else if (ctx.phase == InputActionPhase.Canceled)
            {
                stopMovement(ctx.ReadValue<Vector2>());
            }

        }

        private IEnumerator move(Vector2 moveDirection, Direction dirEnum)
        {
            while (isMoving[(int)dirEnum])
            {
                transform.Translate(-moveDirection.x * distancePerFrame, moveDirection.y * distancePerFrame, 0);
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

        // TODO: UGLY, UGLY, UGLY, UUUUUGLYYYYYY!!!!!
        private Direction getDirection(Vector2 direction)
        {
            if (direction == Vector2.up)
            {
                return Direction.UP;
            }

            if (direction == Vector2.down)
            {
                return Direction.DOWN;
            }

            if (direction == Vector2.left)
            {
                return Direction.LEFT;
            }

            return Direction.RIGHT;
        }

        private void stopMovement(Vector2 moveDirection)
        {
            if (moveDirection != Vector2.up)
            {
                isMoving[(int)Direction.UP] = false;
            }
            if (moveDirection != Vector2.down)
            {
                isMoving[(int)Direction.DOWN] = false;
            }
            if (moveDirection != Vector2.left)
            {
                isMoving[(int)Direction.LEFT] = false;
            }
            if (moveDirection != Vector2.right)
            {
                isMoving[(int)Direction.RIGHT] = false;
            }
        }

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
