using System;

namespace Ex03.GarageLogic
{
    public class InputValidation
    {
        private const string k_ArgumentException = "Argument Exception!";
        private const string k_FormatException = "Format Exception!";

        // Validates that an input string is not empty
        public static string ValidateString(string i_Input)
        {
            if (string.IsNullOrEmpty(i_Input))
            {
                throw new ArgumentException(k_ArgumentException);
            }

            return i_Input;
        }

        public static float ValidatePositiveFloat(string i_Input)
        {
            float maybeFloat;

            if (!float.TryParse(i_Input, out maybeFloat))
            {
                throw new FormatException(k_FormatException);
            }
            else if (maybeFloat < 0)
            {
                throw new ArgumentException(k_ArgumentException);
            }

            return maybeFloat;
        }

        public static void ValiatePercentage(float i_Input)
        {
            if (i_Input > 100 || i_Input < 0)
            {
                throw new ValueOutOfRangeException(100);
            }
        }

        public static int ValidatePositiveInt(string i_Input)
        {
            int maybeInt;

            if (!int.TryParse(i_Input, out maybeInt))
            {
                throw new FormatException(k_FormatException);
            }
            else if (maybeInt < 0)
            {
                throw new ArgumentException(k_ArgumentException);
            }

            return maybeInt;
        }

        public static int ValidateChoiceFromList(string i_Input, int i_First, int i_Last)
        {
            bool v_ValidInput = false;
            int userChoice;

            if (!int.TryParse(i_Input, out userChoice))
            {
                throw new FormatException(k_FormatException);
            }

            for (int i = i_First; i <= i_Last; i++)
            {
                if (userChoice == i)
                {
                    v_ValidInput = true;
                    break;
                }
            }

            if (!v_ValidInput)
            {
                throw new ArgumentException(k_ArgumentException);
            }

            return userChoice;
        }

        public static void validateLessThanMax(float i_CurrentValue, float i_MaxValue)
        {
            if (i_CurrentValue > i_MaxValue)
            {
                throw new ValueOutOfRangeException(i_MaxValue);
            }
        }
    }
}