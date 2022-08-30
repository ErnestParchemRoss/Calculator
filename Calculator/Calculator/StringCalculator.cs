using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {

            if (string.IsNullOrEmpty(numbers))
                return 0;

            var delimiters = GetDelimiters(numbers);
            //string[] delimiters = new string[] { };

            List<int> numbersInt;

            string numbersWithoutFirstLine = GetNumbersWithoutFirstLine(numbers);

            numbersInt = numbersWithoutFirstLine.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToList();

            var negativeNumbers = numbersInt.Where(n => n < 0).ToList();
            if (negativeNumbers.Any())
            {
                throw new NegativesNotAllowedException(negativeNumbers);
            }

            numbersInt = numbersInt.Where(n => n <= 1000).ToList();

            return numbersInt.Sum();
        }

        private string GetNumbersWithoutFirstLine(string numbers)
        {
            if(numbers.Length > 2 && numbers.Substring(0, 2) == "//")
            {
                return numbers.Substring(numbers.IndexOf('\n') + 1);
            }
            else
            {
                return numbers;
            }
        }

        private string[] GetDelimiters(string numbers)
        {
            string[] result;

            bool areDelimitersSpecified = numbers.Length > 5 && numbers.Substring(0, 2) == "//";

            if(areDelimitersSpecified)
            {
                string delimitersString = numbers.Substring(2, numbers.IndexOf('\n') - 2);

                bool areMultipleDelimitersSpecified = numbers[2] == '[';

                if(areMultipleDelimitersSpecified)
                {
                    //przykładowo: [asa][vvv][ddd] == asa, vvv, ddd
                    result = delimitersString.Split(new char[] { '[', ']' });
                }
                else
                {
                    result = new string[] { delimitersString };
                }
            }
            else
            {
                result = new string[] { ",", "\n" };
            }

            return result;
        }
    }
}
