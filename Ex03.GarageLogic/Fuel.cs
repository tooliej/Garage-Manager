using System;

namespace Ex03.GarageLogic
{
    internal class Fuel : EnergySource
    {
        private ResourceManager m_FuelState;
        private eEnergyTypes m_FuelType;

        internal Fuel(float i_CurrentFuel, float i_MaxFuelCapacity, eEnergyTypes i_FuelType)
        {
            m_FuelState = new ResourceManager(i_CurrentFuel, i_MaxFuelCapacity);
            m_FuelType = i_FuelType;
        }

        internal override float RemainingEnergyPercentage
        {
            get
            {
                return (m_FuelState.CurrentAmmount / EnergyCapacity) * 100;
            }
        }

        internal override float EnergyCapacity
        {
            get
            {
                return m_FuelState.MaxAmmount;
            }
        }

        internal override void FillUp(float i_ammount)
        {
            m_FuelState.FillUp(i_ammount);
        }

        public override string ToString()
        {
            return string.Format(
                    @"Fuel Type: {0}
Remaining Fuel: {1}%",
            m_FuelType,
            RemainingEnergyPercentage);
        }
    }
}
