using System;
using System.Globalization;

namespace omegapoint_anna_enstam
{
    public class PerNumCheck : IValidityCheck
    {
        string NumToCheck;
        public string ValidityCheckMessage;

        public PerNumCheck(string numToCheck)
        {
            NumToCheck = CleanInput(numToCheck);
            ValidityCheckMessage = "";
        }

        /// <summary>
        /// Method to check if the number is a valid personal number.
        /// Checks the length, date, and control number.
        /// </summary>
        /// <returns>True if the personal number is valid, false if not.</returns>
        public bool IsValid()
        {
            if (CheckLength() && CheckDate() && CheckLuhn())
            {
                ValidityCheckMessage = "Valid personal number";
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
        /// Method to check if the date is valid.
        /// </summary>
        /// <returns>True if the date is valid, false if not.</returns>
        private bool CheckDate()
        {
            string date = "";

            if (NumToCheck.Length == 10)
            {
                date = NumToCheck.Substring(0, 6);
            }
            if (NumToCheck.Length == 12)
            {
                date = NumToCheck.Substring(2, 6);
            }

            DateTime dateTime;

            if (DateTime.TryParseExact(date, "yyMMdd", null, DateTimeStyles.None, out dateTime))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method to check if the control number is valid using the Luhn algorithm.
        /// </summary>
        /// <returns>True if the control number is valid, false if not.</returns>
        private bool CheckLuhn()
        {
            string perNum = NumToCheck;
            if (perNum.Length == 12)
            {
                perNum = NumToCheck.Substring(2, 10);
            }

            int numOfDigits = perNum.Length;
            int sum = 0;
            bool toDouble = true;

            for (int i = 0; i < numOfDigits; i++)
            {
                int num = (int)char.GetNumericValue(perNum[i]);

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
            return sum % 10 == 0;
        }
    }
}
