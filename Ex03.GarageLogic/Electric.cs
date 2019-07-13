using System;

namespace Ex03.GarageLogic
{
    internal class Electric : EnergySource
    {
        private ResourceManager m_BatteryState;

        internal Electric(float i_CurrentEnergy, float i_MaxEnergyCapacity)
        {
            m_BatteryState = new ResourceManager(i_CurrentEnergy, i_MaxEnergyCapacity);
        }

        internal override float RemainingEnergyPercentage
        {
            get
            {
                return (m_BatteryState.CurrentAmmount / EnergyCapacity) * 100;
            }
        }

        internal override float EnergyCapacity
        {
            get
            {
                return m_BatteryState.MaxAmmount;
            }
        }

        internal override void FillUp(float i_ammount)
        {
            m_BatteryState.FillUp(i_ammount);
        }

        public override string ToString()
        {
            return string.Format(
                @"Remaining Battery: {0}%",
                     RemainingEnergyPercentage);
        }
    }
}
