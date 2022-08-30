using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class NegativesNotAllowedException : Exception
    {
        public List<int> NegativeNumbers { get; set; }

        public NegativesNotAllowedException(List<int> negativeNumbers)
        {
            NegativeNumbers = negativeNumbers;
        }

        public override string Message
        {
            get
            {
                return "Those numbers were negative: " + string.Join(',', NegativeNumbers);
            }
        }
    }
}
