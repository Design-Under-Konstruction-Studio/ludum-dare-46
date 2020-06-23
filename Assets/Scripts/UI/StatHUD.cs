using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Player.Data;
using Core.Enumeration;

namespace UI
{
    public class StatHUD : MonoBehaviour
    {
        #region Data
        [Header("Player Data")]
        [SerializeField]
        private StatData playerData;
        #endregion

        #region UI Components
        [Header("UI Components")]
        [SerializeField]
        private Slider oxygenSlider;

        [SerializeField]
        private Slider fuelSlider;
        #endregion

        private void Awake()
        {
            StartCoroutine(initCR());
        }

        private void onStatChanged(StatType statType)
        {
            switch (statType)
            {
                case StatType.FUEL_LEVEL:
                    fuelSlider.value = playerData.FuelLevel / 100;
                    break;
                case StatType.OXYGEN_LEVEL:
                    oxygenSlider.value = playerData.OxygenLevel / 100;
                    break;
            }
        }

        private IEnumerator initCR()
        {
            while (!playerData.Initialized)
            {
                yield return new WaitForEndOfFrame();
            }

            oxygenSlider.value = playerData.OxygenStartingLevel / 100;
            fuelSlider.value = playerData.FuelStartingLevel / 100;
            playerData.subscribe(onStatChanged);
        }
    }
}