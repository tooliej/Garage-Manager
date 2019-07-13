using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private ResourceManager m_TirePressure;

        internal Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            this.r_ManufacturerName = i_ManufacturerName;
            m_TirePressure = new ResourceManager(i_CurrentAirPressure, i_MaxAirPressure);
        }

        internal string ManufacturersName
        {
            get
            {
                return r_ManufacturerName;
            }
        }

        internal float CurrentAirPressure
        {
            get
            {
                return m_TirePressure.CurrentAmmount;
            }
        }

        internal float MaxAirPressure
        {
            get
            {
                return m_TirePressure.MaxAmmount;
            }
        }

        internal void InflateAction(float i_Ammount)
        {
            m_TirePressure.FillUp(i_Ammount);
        }

        public override string ToString()
        {
            return string.Format(
@"Manufactures name: {0}
Current Air Pressure: {1}",
                                 r_ManufacturerName,
                                 CurrentAirPressure);
        }
    }
}
