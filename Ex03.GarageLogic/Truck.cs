using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const string k_TruckCargoMsg = "Please enter the volume of the cargo:";
        private string m_TruckCooledMsg = string.Format(
           @"Please choose if the truck is cooled or not:
{0}",
        EnumManager.EnumToList<eTruckCooled>());

        private bool m_IsCooled;
        private float m_ValueOfCargo;

        internal Truck(Dictionary<string, object> i_VehilceInfo) : base(i_VehilceInfo)
        {
        }

        public override void CreateUserQuestions(List<ExtendedDictionary> i_QuestionsDictionary)
        {
            base.CreateUserQuestions(i_QuestionsDictionary);
            ExtendedDictionary cargoVolume = new ExtendedDictionary("CargoVolume", k_TruckCargoMsg, eAnswerTypes.Float);
            int firstValidIndex = EnumManager.FirstEnumIndex<eTruckCooled>();
            int lastValidIndex = EnumManager.LastEnumIndex<eTruckCooled>();
            ExtendedDictionary isCooled = new ExtendedDictionary("IsCooled", m_TruckCooledMsg, eAnswerTypes.Enum, firstValidIndex, lastValidIndex);
            i_QuestionsDictionary.Add(cargoVolume);
            i_QuestionsDictionary.Add(isCooled);
        }

        public override void SetVehicleDetails(Dictionary<string, object> i_VehilceInfo)
        {
            base.SetVehicleDetails(i_VehilceInfo);
            float maxEnergy = (float)i_VehilceInfo["MaximumEnergyCapacity"];
            m_IsCooled = Convert.ToBoolean(i_VehilceInfo["IsCooled"]);
            m_ValueOfCargo = (float)i_VehilceInfo["CargoVolume"];
            m_VehicleEnergy = new Fuel((float)i_VehilceInfo["RemainingEnergy"], maxEnergy, (eEnergyTypes)i_VehilceInfo["EnergyType"]);
        }

        internal override string PrintVehicleDetails()
        {
            string cooled = string.Empty;
            if (m_IsCooled)
            {
                cooled = "Is Cooled";
            }
            else
            {
                cooled = "Not Cooled";
            }

            string truckDetails = string.Format(
                @"{0}
Value Of Cargo: {1}
{2}
",
            base.PrintVehicleDetails(),
            m_ValueOfCargo,
            cooled);

            return truckDetails;
        }
    }
}
