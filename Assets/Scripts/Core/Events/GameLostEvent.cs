using UnityEngine;

using Core.Delegate;

namespace Core
{
    namespace Events
    {
        [CreateAssetMenu(fileName = "Game Lost Event", menuName = "Events/Game Lost Event", order = 1)]
        public class GameLostEvent : ScriptableObject
        {
            private OnGameLost onGameLost;

            public void subscribe(OnGameLost func)
            {
                onGameLost += func;
            }

            public void unsubscribe(OnGameLost func)
            {
                onGameLost -= func;
            }

            public void trigger()
            {
                if (onGameLost != null)
                {
                    onGameLost();
                }
            }
        }
    }
}