using System;

using Core.Enumeration;

namespace Core
{
    namespace Delegate
    {
        [Serializable]
        public delegate void OnStatChanged(StatType statType, float amountChanged);
    }
}