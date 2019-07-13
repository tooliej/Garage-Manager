using System;
using System.Collections.Generic;
using Ex03;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private const string k_OwnerName = "Please enter the owners name:";
        private const string k_OwnerPhoneNumber = "Please enter the owners phone number:";
        private const string k_LicenseMsg = "Please enter the license number:";
        private const string k_InvalidInput = "Invalid input! ";
        private const int k_FirstMainMenuIndex = 0;
        private const int k_LastMainMenuIndex = 7;
        private string m_CarMenu = string.Format(
            @"Please choose one of the following vehicles to add to the garage:
{0}",
        GarageLogic.EnumManager.EnumToList<GarageLogic.eVehicleType>());

        private string m_FuelType = string.Format(
          @"Please choose one of the following fuel types:
{0}",
      GarageLogic.EnumManager.EnumToList<GarageLogic.eFuelTypes>());

        private string m_VehicleStatusMsg = string.Format(
        @"Please choose one of the following states to change the current vehicle:
{0}",
    GarageLogic.EnumManager.EnumToList<GarageLogic.eVehicleStatus>());

        private string m_FilterLicenseMsg = string.Format(
    @"Please choose one of the following filtering options:
{0}",
    GarageLogic.EnumManager.EnumToList<GarageLogic.eFilteringOptions>());

        private string m_MainMenu = string.Format(
                @"Please choose one of the following options:
0 - Quit
1 - Add a new vehicle to the garage
2 - Display a list of license numbers currently in the garage
3 - Change a certain vehicles status
4 - Inflate a vehicles tires to the maximum
5 - Re-fuel a fuel based vehicle
6 - Charge an electric bases vehicle
7 - Display vehicle information");

        private Dictionary<string, object> m_VehicleInformation;
        private List<GarageLogic.ExtendedDictionary> m_QuestionsDictionary;
        private GarageLogic.VehicleGarage m_Garage;

        internal GarageUI()
        {
            m_Garage = new GarageLogic.VehicleGarage();
            m_VehicleInformation = new Dictionary<string, object>();
            m_QuestionsDictionary = new List<GarageLogic.ExtendedDictionary>();
        }

        internal void ManageGarage()
        {
            int chosenOption = 0;
      
            do
            {
                chosenOption = getChoiceFromList(m_MainMenu, k_FirstMainMenuIndex, k_LastMainMenuIndex);
                switch (chosenOption)
                {
                    case 1:
                        addVehicle();
                        break;
                    case 2:
                        displayLicenses();
                        break;
                    case 3:
                        changeVehicleStatus();
                        break;
                    case 4:
                        inflateTires();
                        break;
                    case 5:
                        refuel();
                        break;
                    case 6:
                        recharge();
                        break;
                    case 7:
                        displayVehicleInfo();
                        break;
                }

                Console.WriteLine(Environment.NewLine + "Please press Enter to go back to the main menu");
                Console.ReadLine();
                Console.Clear();
            }
            while (chosenOption != 0);
        }
     
        private void addVehicle()
        {
            m_VehicleInformation.Clear();
            m_QuestionsDictionary.Clear();
            addClientDetails();
            string licenseNumber = (string)m_VehicleInformation["LicenseNumber"];

            if (m_Garage.VehicleFound(licenseNumber))
            {
                m_Garage.ChangeVehicleStatus(licenseNumber, GarageLogic.eVehicleStatus.InRepair);
                Console.WriteLine("The vehicle is already in the garage, vehicle status changed to in-repair.");
            }
            else
            {
                int chosenOption = getChoiceFromEnum<GarageLogic.eVehicleType>(m_CarMenu);
                GarageLogic.eVehicleType vehicleType = (GarageLogic.eVehicleType)chosenOption;

                GarageLogic.Vehicle newVehicle = m_Garage.AddClient(vehicleType, m_VehicleInformation);
                newVehicle.CreateUserQuestions(m_QuestionsDictionary);
                foreach (GarageLogic.ExtendedDictionary questions in m_QuestionsDictionary)
                {
                    setVehicleInformation(questions);
                }

                newVehicle.SetVehicleDetails(m_VehicleInformation);
                Console.WriteLine("Vehicle added to the garage and is in repair.");
            }
        }

        private void displayLicenses()
        {
            int chosenOption = getChoiceFromEnum<GarageLogic.eFilteringOptions>(m_FilterLicenseMsg);
            GarageLogic.eVehicleStatus filteringOption = (GarageLogic.eVehicleStatus)chosenOption;
            string outputString = m_Garage.GetLicensesByFilter(filteringOption);
            if (outputString.Equals(string.Empty))
            {
                Console.WriteLine("No vehicles to be displayed.");
            }
            else
            {
                Console.WriteLine(outputString);
            }
        }

        private void changeVehicleStatus()
        {
            string licenceNumber = getStringFromUser(k_LicenseMsg);
            int choice = getChoiceFromEnum<GarageLogic.eVehicleStatus>(m_VehicleStatusMsg);

            if (m_Garage.ChangeVehicleStatus(licenceNumber, (GarageLogic.eVehicleStatus)choice))
            {
                Console.WriteLine("Vehicle status changed.");
            }
            else
            {
                Console.WriteLine("Vehicle not found!");
            }
        }

        private void inflateTires()
        {
            string licenceNumber = getStringFromUser(k_LicenseMsg);
            if (!m_Garage.VehicleFound(licenceNumber))
            {
                Console.WriteLine("Vehicle not found!");
            }
            else
            {
                m_Garage.InflateTiers(licenceNumber);
                Console.WriteLine("Tires inlfated.");
            }
        }

        private void refuel()
        {
            string licenceNumber = getStringFromUser(k_LicenseMsg);
            if (!m_Garage.VehicleFound(licenceNumber))
            {
                Console.WriteLine("Vehicle not found!");
            }
            else
            {
                int choice = getChoiceFromEnum<GarageLogic.eFuelTypes>(m_FuelType);
                float ammountOfFuel = getFloatFromUser("Please enter the ammount of fuel you would like to add");

                try
                {
                    m_Garage.Refuel(licenceNumber, (GarageLogic.eEnergyTypes)choice, ammountOfFuel);
                    Console.WriteLine("Vehicle refueled.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void recharge()
        {
            string licenceNumber = getStringFromUser(k_LicenseMsg);
            if (!m_Garage.VehicleFound(licenceNumber))
            {
                Console.WriteLine("Vehicle not found!");
            }
            else
            {
                float ammountToCharge = getFloatFromUser("Please enter the number of minutes to charge:");

                try
                {
                    m_Garage.Recharge(licenceNumber, ammountToCharge);
                    Console.WriteLine("Vehicle recharged.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void displayVehicleInfo()
        {
            string licenceNumber = getStringFromUser(k_LicenseMsg);
            Console.WriteLine(m_Garage.DisplayVehicleInfo(licenceNumber));
        }

        private void addClientDetails()
        {
            // Gets owners name
            string inputString = getStringFromUser(k_OwnerName);
            m_VehicleInformation.Add("OwnerName", inputString);

            // Gets owners phone number
            inputString = getStringFromUser(k_OwnerPhoneNumber);
            m_VehicleInformation.Add("OwnerPhoneNumber", inputString);

            // Gets license number
            inputString = getStringFromUser(k_LicenseMsg);
            m_VehicleInformation.Add("LicenseNumber", inputString);
        }

        private void setVehicleInformation(GarageLogic.ExtendedDictionary i_Questions)
        {
            switch (i_Questions.AnswerType)
            {
                case GarageLogic.eAnswerTypes.String:
                    string inputString = getStringFromUser(i_Questions.Question);
                    m_VehicleInformation.Add(i_Questions.Tag, inputString);
                    break;
                case GarageLogic.eAnswerTypes.Float:
                    float inputFloat = getFloatFromUser(i_Questions.Question);
                    m_VehicleInformation.Add(i_Questions.Tag, inputFloat);
                    break;
                case GarageLogic.eAnswerTypes.FloatHasMax:
                    float inputValue = getFloatWithMaxFromUser(i_Questions.Question, i_Questions.MaxValue);
                    m_VehicleInformation.Add(i_Questions.Tag, inputValue);
                    break;
                case GarageLogic.eAnswerTypes.Integer:
                    int inputInt = getIntFromUser(i_Questions.Question);
                    m_VehicleInformation.Add(i_Questions.Tag, inputInt);
                    break;
                case GarageLogic.eAnswerTypes.Enum:
                    int choice = getChoiceFromList(i_Questions.Question, i_Questions.FirstValidIndex, i_Questions.LastValidIndex);
                    m_VehicleInformation.Add(i_Questions.Tag, choice);
                    break;
            }
        }

        private string getStringFromUser(string i_ConsoleMsg)
        {
            bool v_TryAgain = true;
            string inputString = string.Empty;
            while (v_TryAgain)
            {
                Console.WriteLine(i_ConsoleMsg);
                try
                {
                    inputString = GarageLogic.InputValidation.ValidateString(Console.ReadLine());
                    v_TryAgain = false;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
            }

            return inputString;
        }

        private float getFloatFromUser(string i_ConsoleMsg)
        {
           // Console.Clear();
            bool v_TryAgain = true;
            float inputValue = 0;
            while (v_TryAgain)
            {
                Console.WriteLine(i_ConsoleMsg);
                try
                {
                    inputValue = GarageLogic.InputValidation.ValidatePositiveFloat(Console.ReadLine());
                    v_TryAgain = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
            }

            return inputValue;
        }

        private float getFloatWithMaxFromUser(string i_ConsoleMsg, float i_MaxValue)
        {
            bool v_TryAgain = true;
            float inputFloat = 0;
            while (v_TryAgain)
            {
                Console.WriteLine(i_ConsoleMsg);
                try
                {
                    inputFloat = GarageLogic.InputValidation.ValidatePositiveFloat(Console.ReadLine());
                    GarageLogic.InputValidation.validateLessThanMax(inputFloat, i_MaxValue);
                    v_TryAgain = false;
                }
                catch (GarageLogic.ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(ArgumentException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
                catch(FormatException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
            }

            return inputFloat;
        }

        private int getIntFromUser(string i_ConsoleMsg)
        {
            bool v_TryAgain = true;
            int inputValue = 0;
            while (v_TryAgain)
            {
                Console.WriteLine(i_ConsoleMsg);
                try
                {
                    inputValue = GarageLogic.InputValidation.ValidatePositiveInt(Console.ReadLine());
                    v_TryAgain = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
            }

            return inputValue;
        }

        private int getChoiceFromList(string i_ConsoleMsg, int o_FirstListIndex, int o_LastListIndex)
        {
            bool v_TryAgain = true;
            int choice = 0;
            while (v_TryAgain)
            {
                Console.WriteLine(i_ConsoleMsg);
                try
                {
                    choice = GarageLogic.InputValidation.ValidateChoiceFromList(Console.ReadLine(), o_FirstListIndex, o_LastListIndex);
                    v_TryAgain = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(k_InvalidInput);
                }
            }

            return choice;
        }

        private int getChoiceFromEnum<TEnum>(string o_ConsoleMsg)
        {
            int firstValidIndex = GarageLogic.EnumManager.FirstEnumIndex<TEnum>();
            int lastValidIndex = GarageLogic.EnumManager.LastEnumIndex<TEnum>();

            return getChoiceFromList(o_ConsoleMsg, firstValidIndex, lastValidIndex);
        }
    }
}
