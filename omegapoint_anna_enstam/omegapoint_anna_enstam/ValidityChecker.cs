using System;

namespace omegapoint_anna_enstam
{
    public class ValidityChecker
    {
        string NumToCheck;
        public string ValidityCheckMessage;

        public ValidityChecker(string input)
        {
            NumToCheck = input;
            ValidityCheckMessage = "";
        }

        /// <summary>
        /// Method to check if the inputed number is a valid personal-,
        /// coordination-, or organizational number.
        /// </summary>
        /// <returns>True if the number is either a valid personal-,
        /// coordination-, or organizational number, false if not.</returns>
        public bool CheckValidity()
        {
            PerNumCheck perNumCheck = new PerNumCheck(NumToCheck);
            CooNumCheck cooNumCheck = new CooNumCheck(NumToCheck);
            OrgNumCheck orgNumCheck = new OrgNumCheck(NumToCheck);

            if (perNumCheck.IsValid() || cooNumCheck.IsValid() || orgNumCheck.IsValid())
            {
                if (perNumCheck.IsValid())
                {
                    ValidityCheckMessage = perNumCheck.ValidityCheckMessage + " ";
                }
                if (cooNumCheck.IsValid())
                {
                    ValidityCheckMessage = cooNumCheck.ValidityCheckMessage + " ";
                }
                if (orgNumCheck.IsValid())
                {
                    ValidityCheckMessage = orgNumCheck.ValidityCheckMessage + " ";
                }
                return true;
            }
            else
            {
                ValidityCheckMessage = "Not a valid personal-, coordination-, or organizational number";
                return false;
            }
        }
    }
}
