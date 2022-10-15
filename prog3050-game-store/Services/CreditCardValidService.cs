using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Services
{
    public class CreditCardValidService
    {
        /// <summary>
        /// This is Lunh's Algorithm to validate numbers
        /// </summary>
        /// <param name="number">Card number</param>
        /// <returns>True if card is valid,false otherwise</returns>
        public  bool isCardValid(string number)
        {
            string ccNumber = null;
            for (int i = 0; i < number.Length; i++)
            {
                if (char.IsDigit(number[i]))
                {
                    ccNumber += number[i];
                }
            }
            int sum = 0;
            bool alternate = false;
            for (int i = ccNumber.Length - 1; i >= 0; i--)
            {
                char[] nx = ccNumber.ToArray();
                int n = int.Parse(nx[i].ToString());

                if (alternate)
                {
                    n *= 2;

                    if (n > 9)
                    {
                        n = (n % 10) + 1;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }
    }
}
