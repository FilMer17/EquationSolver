using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class Program
    {
        static List<int> nums = new List<int>();
        static List<char> operators = new List<char>();
        static List<char> variables = new List<char>();
        static char[] suppSigns = new char[] { '*', '/', '+', '-' };
        static string numTemp = string.Empty;

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            if (CreateEquation(input))
                Console.WriteLine(SolveEquation());

            Console.ReadKey();
        }

        static bool CreateEquation(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
                    continue;

                if (char.IsNumber(input[i]))
                {
                    numTemp += input[i];
                    continue;
                }

                if (input[i] == '!')
                {
                    nums.Add(Factorial(Convert.ToInt32(numTemp)));
                    continue;
                }

                if (char.IsLetter(input[i]))
                {
                    variables.Add(input[i]);
                    continue;
                }

                var isOK = false;
                foreach (var item in suppSigns)
                {
                    if (item == input[i])
                    {
                        operators.Add(item);
                        nums.Add(Convert.ToInt32(numTemp));
                        numTemp = string.Empty;
                        isOK = true;
                        break;
                    }
                }

                if (!isOK)
                {
                    Console.WriteLine($"Neplatný výraz: {input[i]}");
                    return false;
                }
            }

            return true;
        }

        static int SolveEquation()
        {
            List<int> intVars = new List<int>();
            for (int i = 0; i < variables.Count; i++)
            {
                Console.Write(Convert.ToString(variables[i]) + " = ");
                try
                {
                    intVars.Add(Convert.ToInt32(Console.ReadLine()));
                }
                catch (Exception)
                {
                    Console.WriteLine("Neplatné číslo");
                    return -1;
                }
            }

            List<int> outputNums = new List<int>();
            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == suppSigns[0] || operators[i] == suppSigns[1])
                {
                    switch (operators[i])
                    {
                        case '*':
                            outputNums.Add(nums[i] * nums[i+1]);
                            break;
                        case '/':
                            break;
                    }
                }
            }

            int output = 0;
            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == suppSigns[2] || operators[i] == suppSigns[3])
                {
                    switch (operators[i])
                    {
                        case '+':
                            output += outputNums[i];
                            break;
                        case '-':
                            output -= outputNums[i];
                            break;
                    }
                }
            }

            return output;
        }

        static int Factorial(int number)
        {
            if (number == 1)
                return 1;
            else
                return number * Factorial(number - 1);
        }
    }
}
