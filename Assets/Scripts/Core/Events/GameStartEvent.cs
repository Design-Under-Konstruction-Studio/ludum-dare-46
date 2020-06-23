using UnityEngine;

using Core.Delegate;

namespace Core
{
    namespace Events
    {
        [CreateAssetMenu(fileName = "Game Start Event", menuName = "Events/Game Start Event", order = 1)]
        public class GameStartEvent : ScriptableObject
        {
            [SerializeField]
            private float secondsUntilActualStart = 3;

            private OnGameStarted onGameStarted;

            public void subscribe(OnGameStarted func)
            {
                onGameStarted += func;
            }

            public void unsubscribe(OnGameStarted func)
            {
                onGameStarted -= func;
            }

            public void trigger()
            {
                if (onGameStarted != null)
                {
                    onGameStarted(secondsUntilActualStart);
                }
            }
        }
    }
}