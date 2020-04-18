using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

using Core;

namespace Input
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        private InputAction movement;

        [SerializeField]
        private float speed = 5.0f;

        [SerializeField]
        private float hardcodedAngle = 15.0f;

        void OnEnable()
        {
            movement.Enable();
        }

        void Awake()
        {
            movement.started += startMoving;
        }

        void OnDisable()
        {
            movement.Disable();
        }

        private void startMoving(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            if (direction == Vector2.up)
            {
                StartCoroutine(moveOnwardsCR());
                return;
            }

            if (direction == Vector2.down)
            {
                StartCoroutine(moveBackwardsCR());
                return;
            }

            if (direction == Vector2.left)
            {
                StartCoroutine(turnLeftCR());
                return;
            }

            if (direction == Vector2.right)
            {
                StartCoroutine(turnRightCR());
                return;
            }

        }

        private IEnumerator moveOnwardsCR()
        {
            transform.Translate(0, -getDistancePerFrame(), 0);
            yield return new WaitForSeconds(1 / GameDefinitions.FPS);
            StartCoroutine(moveOnwardsCR());
        }

        private IEnumerator moveBackwardsCR()
        {
            transform.Translate(0, getDistancePerFrame(), 0);
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

        private float getAnglePerFrame()
        {
            //TODO: Make it physically realistic!
            return hardcodedAngle;
        }

        private float getDistancePerFrame()
        {
            return speed / GameDefinitions.FPS;
        }
    }

}
