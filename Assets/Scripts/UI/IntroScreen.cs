using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Core.Events;

namespace UI
{
    public class IntroScreen : MonoBehaviour
    {
        [SerializeField]
        private GameStartEvent gameStartEvent;

        [SerializeField]
        private GameLostEvent gameLostEvent;

        public void triggerStart()
        {
            gameLostEvent.subscribe(show);
            gameStartEvent.trigger();
            gameObject.SetActive(false);
        }

        private void show()
        {
            gameObject.SetActive(true);
        }
    }
}