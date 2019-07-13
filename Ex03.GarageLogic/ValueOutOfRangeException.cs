using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private const int k_MinValue = 0;

        public ValueOutOfRangeException(float i_MaxValue)
            : base(string.Format(
                @"Invalid value: 
Maximim value: {0}
Minumum value: {1}",
        i_MaxValue,
        k_MinValue))
        {
        }
    }
}
