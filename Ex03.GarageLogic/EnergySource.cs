using System;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        internal abstract float RemainingEnergyPercentage { get; }

        internal abstract float EnergyCapacity { get; }

        internal abstract void FillUp(float i_ammount);
    }
}
