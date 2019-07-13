using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const string k_CarDoorsMsg = "Please enter the number of car doors {2,3,4,5}:";
        private string m_CarColorMsg = string.Format(
    @"Please choose one of the following car colors:
{0}",
EnumManager.EnumToList<eCarColor>());

        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;

        internal Car(Dictionary<string, object> i_VehilceInfo) : base(i_VehilceInfo)
        {
        }

        public override void CreateUserQuestions(List<ExtendedDictionary> i_QuestionsDictionary)
        {
            base.CreateUserQuestions(i_QuestionsDictionary);
            int firstValidIndex = EnumManager.FirstEnumIndex<eNumberOfDoors>();
            int lastValidIndex = EnumManager.LastEnumIndex<eNumberOfDoors>();
            ExtendedDictionary carDoors = new ExtendedDictionary("CarDoors", k_CarDoorsMsg, eAnswerTypes.Enum, firstValidIndex, lastValidIndex);

            firstValidIndex = EnumManager.FirstEnumIndex<eCarColor>();
            lastValidIndex = EnumManager.LastEnumIndex<eCarColor>();
            ExtendedDictionary carColor = new ExtendedDictionary("CarColor", m_CarColorMsg, eAnswerTypes.Enum, firstValidIndex, lastValidIndex);
            i_QuestionsDictionary.Add(carDoors);
            i_QuestionsDictionary.Add(carColor);
        }

        public override void SetVehicleDetails(Dictionary<string, object> i_VehilceInfo)
        {
            base.SetVehicleDetails(i_VehilceInfo);
            m_CarColor = (eCarColor)i_VehilceInfo["CarColor"];
            m_NumberOfDoors = (eNumberOfDoors)i_VehilceInfo["CarDoors"];

            if (EnergyType == eEnergyTypes.Electric)
            {
                m_VehicleEnergy = new Electric((float)i_VehilceInfo["RemainingEnergy"], (float)i_VehilceInfo["MaximumEnergyCapacity"]);
            }
            else
            {
                m_VehicleEnergy = new Fuel((float)i_VehilceInfo["RemainingEnergy"], (float)i_VehilceInfo["MaximumEnergyCapacity"], (eEnergyTypes)i_VehilceInfo["EnergyType"]);
            }
        }

        internal override string PrintVehicleDetails()
        {
            string carDetails = string.Format(
                @"{0}
Color: {1}
Number Of Doors: {2}
",
            base.PrintVehicleDetails(),
            m_CarColor,
            (int)m_NumberOfDoors);

            return carDetails;
        }
    }
}
