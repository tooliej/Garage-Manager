using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private const string k_ModelMsg = "Please enter the vehicle model:";
        private const string k_EnergyMsg = "Please enter the remaining energy percentage:";
        private const string k_WheelMsg = "Please enter the wheel manufacturers name:";
        private const string k_CurrentAirMsg = "Please enter the current air pressure:";
        private readonly string r_LicenceNumber;
        private readonly string r_OwnerName;
        private readonly float r_MaxAirPressure;
        private readonly float r_MaxEnergyCapacity;
        private eEnergyTypes m_TypeOfEnergy;
        private string m_ModelName;
        protected EnergySource m_VehicleEnergy;
        private List<Wheel> m_Wheels;

        internal Vehicle(Dictionary<string, object> i_VehilceInfo)
        {
            this.r_LicenceNumber = (string)i_VehilceInfo["LicenseNumber"];
            this.r_OwnerName = (string)i_VehilceInfo["OwnerName"];
            this.r_MaxAirPressure = (float)i_VehilceInfo["MaxAirPressure"];
            this.r_MaxEnergyCapacity = (float)i_VehilceInfo["MaximumEnergyCapacity"];
        }

        public virtual void CreateUserQuestions(List<ExtendedDictionary> i_QuestionsDictionary)
        {
            ExtendedDictionary model = new ExtendedDictionary("Model", k_ModelMsg, eAnswerTypes.String);
            ExtendedDictionary remainingEnergy = new ExtendedDictionary("RemainingEnergy", k_EnergyMsg, eAnswerTypes.FloatHasMax, r_MaxEnergyCapacity);
            ExtendedDictionary wheelManufacturer = new ExtendedDictionary("WheelManufacturer", k_WheelMsg, eAnswerTypes.String);
            ExtendedDictionary airPressure = new ExtendedDictionary("CurrentAirPressure", k_CurrentAirMsg, eAnswerTypes.FloatHasMax, r_MaxAirPressure);
            i_QuestionsDictionary.Add(model);
            i_QuestionsDictionary.Add(remainingEnergy);
            i_QuestionsDictionary.Add(wheelManufacturer);
            i_QuestionsDictionary.Add(airPressure);
        }

        public virtual void SetVehicleDetails(Dictionary<string, object> i_VehilceInfo)
        {
            this.m_ModelName = (string)i_VehilceInfo["Model"];
            this.m_TypeOfEnergy = (eEnergyTypes)i_VehilceInfo["EnergyType"];
            createWheels((int)i_VehilceInfo["NumberOfWheels"], (string)i_VehilceInfo["WheelManufacturer"], (float)i_VehilceInfo["CurrentAirPressure"], (float)i_VehilceInfo["MaxAirPressure"]);
        }

        internal void InflateTires(float i_AmmountToAdd)
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateAction(i_AmmountToAdd);
            }
        }

        internal void Recharge(float i_Ammount)
        {
            if (m_TypeOfEnergy != eEnergyTypes.Electric)
            {
                throw new ArgumentException("Cannot charge fuel vehicle!");
            }
            else
            {
                m_VehicleEnergy.FillUp(i_Ammount);
            }
        }

        internal void Refuel(float i_Ammount, eEnergyTypes i_FuelType)
        {
            if (m_TypeOfEnergy == eEnergyTypes.Electric)
            {
                throw new ArgumentException("Cannot fuel an electric vehicle!");
            }
            else if (m_TypeOfEnergy != i_FuelType)
            {
                throw new ArgumentException(string.Format(
                    @"Invalid fuel type! 
Expected fuel type: {0}
Received fuel type: {1}",
                m_TypeOfEnergy.ToString(),
                i_FuelType.ToString()));
            }
            else
            {
                m_VehicleEnergy.FillUp(i_Ammount);
            }
        }

        internal string LicenseNumber
        {
            get
            {
                return r_LicenceNumber;
            }
        }

        internal eEnergyTypes EnergyType
        {
            get
            {
                return m_TypeOfEnergy;
            }
        }

        internal float RemainingEnergy
        {
            get
            {
                return m_VehicleEnergy.RemainingEnergyPercentage;
            }
        }

        internal float CurrentAirPressure
        {
            get
            {
                return m_Wheels.First().CurrentAirPressure;
            }
        }

        internal float MaxAirPressure
        {
            get
            {
                return m_Wheels.First().MaxAirPressure;
            }
        }

        private void createWheels(int i_NumOfWheels, string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_Wheels = new List<Wheel>();

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                Wheel wheel = new Wheel(i_ManufacturerName, i_CurrentAirPressure, i_MaxAirPressure);
                m_Wheels.Add(wheel);
            }
        }

        internal virtual string PrintVehicleDetails()
        {
            string vehicleDetails = string.Format(
                @"Licence Number: {0}
Model Name: {1}
Owners Name: {2}
Tire Specifications:
{3}
{4}",
            r_LicenceNumber,
            m_ModelName,
            r_OwnerName,
            m_Wheels.First().ToString(),
            m_VehicleEnergy.ToString());

            return vehicleDetails;
        }
    }
}
