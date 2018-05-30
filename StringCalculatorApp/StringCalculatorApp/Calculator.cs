using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorApp
{
    public class Calculator
    {
        private readonly List<string> _defaultDelimiters = new List<string> {",", ";", "\n"};
        private const int MaxNumberLimit = 1000;
        private const string CustomDelimiterIdentifier = "//";

        public int Add(string numbers)
        {
            try
            {
                if (string.IsNullOrEmpty(numbers)) return 0;
                string[] numberArray;

                //Check for custum delimiters
                if (numbers.StartsWith(CustomDelimiterIdentifier))
                {
                    //Get the 1st custom line
                    var customLine = numbers.Split('\n')[0];
                    var customDilemtierList = GetCustomDelimiters(customLine);
                    //Remove 1st custom Line
                    numbers = numbers.Replace(customLine, "");
                    numberArray = numbers.Split(customDilemtierList.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    numberArray = numbers.Split(_defaultDelimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                }

                var numberList = GetNumbers(numberArray);
                NegativeNumberValidation(numberList);
                return GetSum(numberList);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }
        }

        private static int GetSum(IEnumerable<int> numberList)
        {
            return numberList.Where(number => number < MaxNumberLimit).Sum();
        }

        private static void NegativeNumberValidation(IEnumerable<int> numberList)
        {
            var errorMessage = numberList.Where(number => number < 0).Aggregate("", (current, number) => $"{current} {number},");
            if (!string.IsNullOrEmpty(errorMessage))
                throw new Exception($"Negative numbers exist: {errorMessage}");
        }

        private IEnumerable<int> GetNumbers(IEnumerable<string> numberArray)
        {
            var numberList = new List<int>();
            foreach (var stringNumber in numberArray)
            {
                if (string.IsNullOrEmpty(stringNumber)) continue;
                if (!int.TryParse(stringNumber, out var number)) continue;
                numberList.Add(number);
            }

            return numberList;
        }

        private static List<string> GetCustomDelimiters(string customLine)
        {
            var delimiterList = new List<string>();
            customLine = customLine.Replace(CustomDelimiterIdentifier, "");
            if (customLine.StartsWith("["))
            {
                //Pattern is either [***]  OR [*][%]
                var delimterArray = customLine.Split('[', ']');
                delimiterList.AddRange(delimterArray);
            }
            else
            {
                //Pattern is //;
                delimiterList.Add(customLine);
            }

            return delimiterList;
        }
    }
}