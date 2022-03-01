using System;
using System.Collections.Generic;

namespace omegapoint_anna_enstam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a number to check:");
            string input = Console.ReadLine();

            ValidityChecker validityChecker = new ValidityChecker(input);
            validityChecker.CheckValidity();
            Console.WriteLine(validityChecker.ValidityCheckMessage);
        }
    }
}
