using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class MotorCycle : Vehicle
    {
        private const string k_EngineMsg = "Please enter the engine volume:";
        private string m_LicenseMsg = string.Format(
            @"Please choose one of the following licenses:
{0}",
        EnumManager.EnumToList<eLicenseTypes>());

        private eLicenseTypes m_LicenseType;
        private int m_EngineVolume;

        internal MotorCycle(Dictionary<string, object> i_VehilceInfo) : base(i_VehilceInfo)
        {
        }

        public override void CreateUserQuestions(List<ExtendedDictionary> i_QuestionsDictionary)
        {
            base.CreateUserQuestions(i_QuestionsDictionary);
            int firstValidIndex = EnumManager.FirstEnumIndex<eLicenseTypes>();
            int lastValidIndex = EnumManager.LastEnumIndex<eLicenseTypes>();
            ExtendedDictionary licenseType = new ExtendedDictionary("LicenseType", m_LicenseMsg, eAnswerTypes.Enum, firstValidIndex, lastValidIndex);
            ExtendedDictionary engineVolume = new ExtendedDictionary("EngineVolume", k_EngineMsg, eAnswerTypes.Integer);
            i_QuestionsDictionary.Add(licenseType);
            i_QuestionsDictionary.Add(engineVolume);
        }

        public override void SetVehicleDetails(Dictionary<string, object> i_VehilceInfo)
        {
            base.SetVehicleDetails(i_VehilceInfo);
            m_LicenseType = (eLicenseTypes)i_VehilceInfo["LicenseType"];
            m_EngineVolume = (int)i_VehilceInfo["EngineVolume"];

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
            string motorCyleDetails = string.Format(
                @"{0}
Licence Type: {1}
Enginge Volume: {2}
",
            base.PrintVehicleDetails(),
            m_LicenseType,
            m_EngineVolume);

            return motorCyleDetails;
        }
    }
}
