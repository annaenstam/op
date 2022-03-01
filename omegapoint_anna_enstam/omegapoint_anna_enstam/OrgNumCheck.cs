using System;
using System.Globalization;

namespace omegapoint_anna_enstam
{
    public class OrgNumCheck : IValidityCheck
    {
        string NumToCheck;
        public string ValidityCheckMessage;

        public OrgNumCheck(string numToCheck)
        {
            NumToCheck = CleanInput(numToCheck);
            ValidityCheckMessage = "";
        }

        /// <summary>
        /// Method to check if the number is a valid organizational number.
        /// Checks the length, the third number, and control number.
        /// </summary>
        /// <returns>True if the organizational number is valid, false if not.</returns>
        public bool IsValid()
        {
            if (CheckLength() && CheckThirdNumber() && CheckLuhn())
            {
                ValidityCheckMessage = "Valid organizational number";
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
        /// Method to check if the third number (in the 10-digit form) is at least 2.
        /// </summary>
        /// <returns>True if the third number is valid, false if not.</returns>
        private bool CheckThirdNumber()
        {
            int thirdNum = 0;

            if (NumToCheck.Length == 10)
            {
                thirdNum = int.Parse(NumToCheck.Substring(2, 1));
            }
            if (NumToCheck.Length == 12)
            {
                thirdNum = int.Parse(NumToCheck.Substring(4, 1));
            }

            if (thirdNum >= 2)
            {
                return true;
            }
            else
            {
                ValidityCheckMessage = "Not a valid organizational number (invalid third digit)";
                return false;
            }
        }

        /// <summary>
        /// Method to check if the control number is valid using the Luhn algorithm.
        /// </summary>
        /// <returns>True if the control number is valid, false if not.</returns>
        private bool CheckLuhn()
        {
            string orgNum = NumToCheck;
            if (orgNum.Length == 12)
            {
                orgNum = NumToCheck.Substring(2, 10);
            }

            int numOfDigits = orgNum.Length;
            int sum = 0;
            bool toDouble = true;

            for (int i = 0; i < numOfDigits; i++)
            {
                int num = (int)char.GetNumericValue(orgNum[i]);

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
                ValidityCheckMessage = "Not a valid organizational number (invalid control number)";
            }
            return sum % 10 == 0;
        }
    }
}
