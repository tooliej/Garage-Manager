using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleGarage
    {
        // Car constants
        private const int k_CarNumOfWheels = 4;
        private const float k_CarMaxAirPressure = 32;
        private const float k_ElectricCarMaxEnergyCapacity = 3.2F;
        private const float k_FuelCarMaxCapacity = 45F;
        private const eEnergyTypes k_CarTypeOfEnergy = eEnergyTypes.Octane98;

        // Truck constants
        private const int k_TruckNumOfWheels = 12;
        private const float k_TruckMaxAirPressure = 28;
        private const float k_TruckMaxFuelCapacity = 115F;
        private const eEnergyTypes k_TruckTypeOfEnergy = eEnergyTypes.Octane96;

        // Motorcycle constants
        private const int k_MotorCycleNumOfWheels = 2;
        private const float k_MotorCycleMaxAirPressure = 30;
        private const float k_ElectricMotorbikeMaxEnergyCapacity = 1.8F;
        private const float k_MotorbikeMaxFuelCapacity = 6;
        private const eEnergyTypes k_MotorBikeTypeOfEnergy = eEnergyTypes.Octane96;
        private Dictionary<Vehicle, eVehicleStatus> m_GarageManager;

        public VehicleGarage()
        {
            m_GarageManager = new Dictionary<Vehicle, eVehicleStatus>();
        }

        public Vehicle AddClient(eVehicleType i_VehicleType, Dictionary<string, object> i_VehilceInfo)
        {
            string licenceNumber = (string)i_VehilceInfo["LicenseNumber"];

            Vehicle newClient = null;
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                    addElectricCarInfo(i_VehilceInfo);
                    newClient = new Car(i_VehilceInfo);
                    break;
                case eVehicleType.FuelCar:
                    addFuelCarInfo(i_VehilceInfo);
                    newClient = new Car(i_VehilceInfo);
                    break;
                case eVehicleType.ElectricMotorCycle:
                    addElectricMotorCycleInfo(i_VehilceInfo);
                    newClient = new MotorCycle(i_VehilceInfo);
                    break;
                case eVehicleType.FuelMotorCycle:
                    addFuelMotorCylcleInfo(i_VehilceInfo);
                    newClient = new MotorCycle(i_VehilceInfo);
                    break;
                case eVehicleType.Truck:
                    addTruckInfo(i_VehilceInfo);
                    newClient = new Truck(i_VehilceInfo);
                    break;
            }

            m_GarageManager.Add(newClient, eVehicleStatus.InRepair);

            return newClient;
        }

        public string GetLicensesByFilter(eVehicleStatus i_FilteringOption)
        {
            StringBuilder licenceList = new StringBuilder();
            foreach (Vehicle vehilcle in m_GarageManager.Keys)
            {
                // Filter option #0 = No filter
                if (i_FilteringOption == 0 || i_FilteringOption == m_GarageManager[vehilcle])
                {
                    licenceList.Append(vehilcle.LicenseNumber + Environment.NewLine);
                }
            }

            return licenceList.ToString();
        }

        public bool ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_DesiredStatus)
        {
            bool v_Found = false;

            foreach (Vehicle vehilcle in m_GarageManager.Keys)
            {
                if (vehilcle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    m_GarageManager[vehilcle] = i_DesiredStatus;
                    v_Found = true;
                    break;
                }
            }

            return v_Found;
        }

        public void InflateTiers(string i_LicenseNumber)
        {
            foreach (Vehicle vehilcle in m_GarageManager.Keys)
            {
                if (vehilcle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    vehilcle.InflateTires(vehilcle.MaxAirPressure - vehilcle.CurrentAirPressure);
                    break;
                }
            }
        }

        public void Refuel(string i_LicenseNumber, eEnergyTypes i_FuelType, float i_AmmountToFill)
        {
            foreach (Vehicle vehilcle in m_GarageManager.Keys)
            {
                if (vehilcle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    vehilcle.Refuel(i_AmmountToFill, i_FuelType);
                    break;
                }
            }
        }

        public void Recharge(string i_LicenseNumber, float i_AmmountToFill)
        {
            foreach (Vehicle vehilcle in m_GarageManager.Keys)
            {
                if (vehilcle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    vehilcle.Recharge(i_AmmountToFill);
                    break;
                }
            }
        }

        public string DisplayVehicleInfo(string i_LicenseNumber)
        {
            bool v_Found = false;
            StringBuilder vehicleDetails = new StringBuilder();

            foreach (Vehicle vehilcle in m_GarageManager.Keys)
            {
                if (vehilcle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    vehicleDetails.Append(vehilcle.PrintVehicleDetails());
                    vehicleDetails.Append("Vehicle status: " + m_GarageManager[vehilcle].ToString());
                    v_Found = true;
                    break;
                }
            }

            if (!v_Found)
            {
                vehicleDetails.Append("Vehicle not found!");
            }

            return vehicleDetails.ToString();
        }

        public bool VehicleFound(string i_LicenseNumber)
        {
            bool v_Found = false;

            foreach (Vehicle vehilcle in m_GarageManager.Keys)
            {
                if (vehilcle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    v_Found = true;
                    break;
                }
            }

            return v_Found;
        }

        private void addDefaultCarInfo(Dictionary<string, object> i_VehilceInfo)
        {
            i_VehilceInfo.Add("NumberOfWheels", k_CarNumOfWheels);
            i_VehilceInfo.Add("MaxAirPressure", k_CarMaxAirPressure);
        }

        private void addElectricCarInfo(Dictionary<string, object> i_VehilceInfo)
        {
            i_VehilceInfo.Add("EnergyType", (eEnergyTypes)eEnergyTypes.Electric);
            i_VehilceInfo.Add("MaximumEnergyCapacity", k_ElectricCarMaxEnergyCapacity);
            addDefaultCarInfo(i_VehilceInfo);
        }

        private void addFuelCarInfo(Dictionary<string, object> i_VehilceInfo)
        {
            i_VehilceInfo.Add("MaximumEnergyCapacity", k_FuelCarMaxCapacity);
            i_VehilceInfo.Add("EnergyType", (eEnergyTypes)k_CarTypeOfEnergy);
            addDefaultCarInfo(i_VehilceInfo);
        }

        private void addDefaultMotorCycleInfo(Dictionary<string, object> i_VehilceInfo)
        {
            i_VehilceInfo.Add("NumberOfWheels", k_MotorCycleNumOfWheels);
            i_VehilceInfo.Add("MaxAirPressure", k_MotorCycleMaxAirPressure);
        }

        private void addElectricMotorCycleInfo(Dictionary<string, object> i_VehilceInfo)
        {
            i_VehilceInfo.Add("EnergyType", (eEnergyTypes)eEnergyTypes.Electric);
            i_VehilceInfo.Add("MaximumEnergyCapacity", k_ElectricMotorbikeMaxEnergyCapacity);
            addDefaultMotorCycleInfo(i_VehilceInfo);
        }

        private void addFuelMotorCylcleInfo(Dictionary<string, object> i_VehilceInfo)
        {
            i_VehilceInfo.Add("MaximumEnergyCapacity", k_MotorbikeMaxFuelCapacity);
            i_VehilceInfo.Add("EnergyType", (eEnergyTypes)k_MotorBikeTypeOfEnergy);
            addDefaultMotorCycleInfo(i_VehilceInfo);
        }

        private void addTruckInfo(Dictionary<string, object> i_VehilceInfo)
        {
            i_VehilceInfo.Add("NumberOfWheels", k_TruckNumOfWheels);
            i_VehilceInfo.Add("MaxAirPressure", k_TruckMaxAirPressure);
            i_VehilceInfo.Add("MaximumEnergyCapacity", k_TruckMaxFuelCapacity);
            i_VehilceInfo.Add("EnergyType", (eEnergyTypes)k_TruckTypeOfEnergy);
        }
    }
}