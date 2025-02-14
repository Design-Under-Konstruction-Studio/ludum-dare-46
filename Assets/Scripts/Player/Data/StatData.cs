using UnityEngine;

using Core.Delegate;
using Core.Enumeration;
using Core.Constant;
using Core.Events;

namespace Player
{
    namespace Data
    {
        [CreateAssetMenu(fileName = "Stat Data", menuName = "Data/Stat Data", order = 0)]
        public class StatData : ScriptableObject
        {
            #region Stats
            [Header("Stats - Do not change")]
            [SerializeField]
            private float oxygenLevel;

            [SerializeField]
            private float fuelLevel;

            [SerializeField]
            private bool alive = true;
            #endregion

            #region Stats Modifiers
            [Header("Stats Modifiers")]
            [SerializeField]
            private float oxygenModifier = -1.0f;

            [SerializeField]
            private float fuelModifier = -1.0f;

            [SerializeField]
            private float lowFuelSpeedModifier = 0.1f;
            #endregion

            #region Maximum Levels
            [Header("Maximum Levels")]
            [SerializeField]
            private float maximumOxygenLevel = 100.0f;

            [SerializeField]
            private float maximumFuelLevel = 100.0f;

            [SerializeField]
            private float fuelLevelThreshold = 20.0f;
            #endregion

            #region Starting Points
            [Header("Starting Points")]
            [SerializeField]
            private float oxygenStartingLevel = 50.0f;

            [SerializeField]
            private float fuelStartingLevel = 50.0f;

            private bool initialized = false;
            #endregion

            #region Events
            [SerializeField]
            private OnStatChanged onStatChanged;
            #endregion

            #region Properties
            public float OxygenLevel
            {
                get
                {
                    return oxygenLevel;
                }
                private set
                {

                }
            }

            public float FuelLevel
            {
                get
                {
                    return fuelLevel;
                }
                private set
                {

                }
            }

            public float LowFuelSpeedModifier
            {
                get
                {
                    return lowFuelSpeedModifier;
                }
                private set
                {

                }
            }

            public float OxygenStartingLevel
            {
                get
                {
                    return oxygenStartingLevel;
                }
                private set
                {

                }
            }

            public float FuelStartingLevel
            {
                get
                {
                    return fuelStartingLevel;
                }
                private set
                {

                }
            }

            public float FuelLevelThreshold
            {
                get
                {
                    return fuelLevelThreshold;
                }
                private set
                {

                }
            }

            public bool Alive
            {
                get
                {
                    return alive;
                }
                private set
                {

                }
            }

            public bool Initialized
            {
                get
                {
                    return initialized;
                }
                private set
                {

                }
            }
            #endregion

            public void init()
            {
                alive = true;
                fuelLevel = fuelStartingLevel;
                oxygenLevel = oxygenStartingLevel;
                initialized = true;
            }

            #region Subscribe/unsubscribe
            public void subscribe(OnStatChanged callback)
            {
                onStatChanged += callback;
            }

            public void unsubscribe(OnStatChanged callback)
            {
                onStatChanged -= callback;
            }
            #endregion

            #region Trigger
            public void triggerStatChange(StatType statType, float amountChanged)
            {
                switch (statType)
                {
                    case StatType.OXYGEN_LEVEL:
                        oxygenLevel += amountChanged;
                        break;
                    case StatType.FUEL_LEVEL:
                        fuelLevel += amountChanged;
                        break;
                    case StatType.BOTH:
                        fuelLevel += amountChanged;
                        oxygenLevel += amountChanged;
                        break;
                }

                if (onStatChanged != null)
                {
                    onStatChanged(statType);
                }
            }

            public void triggerNaturalStatChange()
            {
                triggerStatChange(StatType.FUEL_LEVEL, fuelModifier / GameDefinitions.FPS);
                triggerStatChange(StatType.OXYGEN_LEVEL, oxygenModifier / GameDefinitions.FPS);
            }
            #endregion

            private StatData()
            { }

        }
    }
}