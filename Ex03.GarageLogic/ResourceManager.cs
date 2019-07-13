using System;

namespace Ex03.GarageLogic
{
    internal class ResourceManager
    {
        private const int k_MinAmmount = 0;
        private float m_CurrentAmmount;
        private float m_MaxAmmount;

        internal ResourceManager(float i_CurrentAmmount, float i_MaxAmmount)
        {
            this.m_CurrentAmmount = i_CurrentAmmount;
            this.m_MaxAmmount = i_MaxAmmount;
        }

        internal float CurrentAmmount
        {
            get
            {
                return m_CurrentAmmount;
            }
        }

        internal float MaxAmmount
        {
            get
            {
                return m_MaxAmmount;
            }
        }

        internal void FillUp(float i_Ammount)
        {
            if (m_CurrentAmmount + i_Ammount > m_MaxAmmount || m_CurrentAmmount < k_MinAmmount)
            {
                throw new ValueOutOfRangeException(m_MaxAmmount - m_CurrentAmmount);
            }
            else
            {
                m_CurrentAmmount += i_Ammount;
            }
        }
    }
}
