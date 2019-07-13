using System;
using System.Text;
using System.Linq;

namespace Ex03.GarageLogic
{
    public enum eCarColor
    {
        Gray = 1,
        Blue,
        White,
        Black
    }

    public enum eNumberOfDoors
    {
        Two = 2,
        Three,
        Four,
        Five
    }

    public enum eLicenseTypes
    {
        A = 1,
        A1,
        B1,
        B2
    }

    public enum eEnergyTypes
    {
        Electric,
        Soler,
        Octane95,
        Octane96,
        Octane98
    }

    public enum eVehicleType
    {
        ElectricCar = 1,
        FuelCar,
        ElectricMotorCycle,
        FuelMotorCycle,
        Truck
    }

    public enum eFuelTypes
    {
        Soler = 1,
        Octane95,
        Octane96,
        Octane98
    }

    public enum eVehicleStatus
    {
        InRepair = 1,
        Repaired,
        Paid
    }

    public enum eFilteringOptions
    {
        NoFilter = 0,
        InRepair,
        Repaired,
        Paid
    }

    public enum eTruckCooled
    {
        NotCooled = 0,
        Cooled
    }

    public enum eAnswerTypes
    {
        String,
        Integer,
        Float,
        FloatHasMax,
        Enum
    }

    public class EnumManager
    {
        public static string EnumToList<TEnum>()
        {
            StringBuilder list = new StringBuilder();

            foreach (var values in Enum.GetValues(typeof(TEnum)))
            {
                list.Append((int)values + " -");
                foreach (char c in values.ToString())
                {
                    if (char.IsUpper(c))
                    {
                        list.Append(" " + c);
                    }
                    else
                    {
                        list.Append(c);
                    }
                }

                list.Append(Environment.NewLine);
            }

            return list.ToString();
        }

        public static int FirstEnumIndex<TEnum>()
        {
            int[] values = (int[])Enum.GetValues(typeof(TEnum));
            return values.First();
        }

        public static int LastEnumIndex<TEnum>()
        {
            return FirstEnumIndex<TEnum>() + Enum.GetNames(typeof(TEnum)).Length - 1;
        }
    }
}
