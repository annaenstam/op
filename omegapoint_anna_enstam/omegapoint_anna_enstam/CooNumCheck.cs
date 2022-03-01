using System;
using System.Globalization;

namespace omegapoint_anna_enstam
{
    public class CooNumCheck : IValidityCheck
    {
        string NumToCheck;
        public string ValidityCheckMessage;

        public CooNumCheck(string numToCheck)
        {
            NumToCheck = CleanInput(numToCheck);
            ValidityCheckMessage = "";
        }

        /// <summary>
        /// Method to check if the number is a valid coordination number.
        /// Checks the length, date (+60), and control number.
        /// </summary>
        /// <returns>True if the coordination number is valid, false if not.</returns>
        public bool IsValid()
        {
            if (CheckLength() && CheckDate() && CheckLuhn())
            {
                ValidityCheckMessage = "Valid coordination number";
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to extract the numbers from the input,
        /// ignoring all other characters.
        /// </summary>
        /// <param name="input">A string representing the number to check</param>
        /// <returns>A string with only the numbers.</returns>
        private string CleanInput(string input)
        {
            string cleanInput = "";

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    cleanInput += c;
                }
            }

            return cleanInput;
        }

        /// <summary>
        /// Method to check if the number has a valid amount of digits.
        /// </summary>
        /// <returns>True if the length is valid, false if not.</returns>
        private bool CheckLength()
        {
            return NumToCheck.Length == 12 || NumToCheck.Length == 10;
        }

        /// <summary>
        /// Method to check if the "date" has a value between 61 and 91,
        /// and if the "date"-60 is a valid date.
        /// </summary>
        /// <returns>True if the "date" and date is valid, false if not.</returns>
        private bool CheckDate()
        {
            string date = "";
            int day = 0;

            if (NumToCheck.Length == 10)
            {
                day = int.Parse(NumToCheck.Substring(4, 2));
                date = NumToCheck.Substring(0, 4);
                date += (day - 60).ToString();
            }
            if (NumToCheck.Length == 12)
            {
                day = int.Parse(NumToCheck.Substring(6, 2));
                date = NumToCheck.Substring(2, 4);
                date += (day - 60).ToString();
            }

            if (day < 61 || day > 91)
            {
                ValidityCheckMessage = "Not a valid coordination number (invalid date)";
                return false;
            }

            DateTime dateTime;

            if (DateTime.TryParseExact(date, "yyMMdd", null, DateTimeStyles.None, out dateTime))
            {
                return true;
            }
            else
            {
                ValidityCheckMessage = "Not a valid coordination number (invalid date)";
                return false;
            }
        }

        /// <summary>
        /// Method to check if the control number is valid using the Luhn algorithm.
        /// </summary>
        /// <returns>True if the control number is valid, false if not.</returns>
        private bool CheckLuhn()
        {
            string cooNum = NumToCheck;
            if (cooNum.Length == 12)
            {
                cooNum = NumToCheck.Substring(2, 10);
            }

            int numOfDigits = cooNum.Length;
            int sum = 0;
            bool toDouble = true;

            for (int i = 0; i < numOfDigits; i++)
            {
                int num = (int)char.GetNumericValue(cooNum[i]);

                if (toDouble)
                {
                    num *= 2;

                    if (num > 9)
                    {
                        num = num - 9;
                    }
                }

                sum += num;
                toDouble = !toDouble;
            }

            if (!(sum % 10 == 0))
            {
                ValidityCheckMessage = "Not a valid coordination number (invalid control number)";
            }
            return sum % 10 == 0;
        }
    }
}
