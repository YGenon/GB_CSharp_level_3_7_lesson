using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте вас приветствует математическая программа \r\n" +
                                "(для выхода нажмите - q) \r\n пажалуйста введите число. ");

            String inputNumberString = Console.ReadLine();
            Int32 inputNumber;

            if (inputNumberString == "q")
                {return;}
            else 
                { inputNumber = Int32.Parse(inputNumberString); }

            int factorialNumber = 1;
            int summNumbers = 0;
            int maxEvenNumber = 0;

            for (int i = 1; i <= inputNumber; i++)
            {
                factorialNumber *= i;

                summNumbers += i;

                if (i % 2 == 0)
                { if (i != inputNumber) maxEvenNumber = i; }
            }

            Console.WriteLine("Факториал равен - " + factorialNumber + "\r\n" +
                              "Сума от 1 до N равна - " + summNumbers + "\r\n" +
                              "максимальное четное число меньше N равно - " + maxEvenNumber);
            Console.ReadLine();
        }
    }
}
