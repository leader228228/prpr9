using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace pr_pr_9
{
    class Program
    {
        delegate void MyDelegate(double[][] matrix);

        static void Main(string[] args)
        {
            task_5();
            Console.Read();
        }

        private static void ChangeMatrix(double [][] matrix, MyDelegate myDelegate) {
            myDelegate(matrix);
        }

        private static void task_1()
        {
            Random random = new Random(System.DateTime.Now.Millisecond);
            double [][] matrix = new double[3][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new double[3];
            }
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    matrix[i][j] = Math.Pow(-1, i + j) * random.Next() % 100;
                }
            }
            //1
            MyDelegate myDelegate = printMatrix;
            ChangeMatrix(matrix, myDelegate);
            Console.WriteLine();
            //2
            myDelegate = printMatrixPosElems;
            ChangeMatrix(matrix, myDelegate);
            //3
            Console.WriteLine();
            myDelegate = multBy3;
            ChangeMatrix(matrix, myDelegate);
            myDelegate = printMatrix;
            ChangeMatrix(matrix, myDelegate);
        }

        private static void printMatrix(double [] [] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    Console.Write(matrix[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        private static void printMatrixPosElems(double[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (matrix[i][j] >= 0)
                    {
                        Console.Write(matrix[i][j]);
                    }
                    else
                    {
                        Console.Write("negative");
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        private static void multBy3(double[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    matrix[i][j] *= 3;
                }
            }
        }

        delegate void MyDelegate2(String filepath);
        private static void task_2()
        {
            MyDelegate2 myDelegate2;
            //1
            myDelegate2 = printContent;
            myDelegate2("test.txt");
            //2
            myDelegate2 = printAllNumbers;
            myDelegate2("test.txt");
            //3
            myDelegate2 = replaceAllText;
            myDelegate2("test.txt");
            myDelegate2 = printContent;
            myDelegate2("test.txt");
        }

        private static void ReadFiles(String filepath, MyDelegate2 myDelegate2)
        {
            myDelegate2(filepath);
        }

        private static void printContent(String filepath)
        {
            Console.WriteLine(File.ReadAllText(filepath));
        }

        private static void printAllNumbers(String filepath)
        {
            Regex regex = new Regex("\\d+");
            foreach (Match match in regex.Matches(File.ReadAllText(filepath)))
            {
                Console.WriteLine("'{0}' found in the source code at position {1}.",
                           match.Value, match.Index);
            }
        }

        private static void replaceAllText(String filepath)
        {
            String text = File.ReadAllText(filepath);
            File.WriteAllText(filepath, new Regex("[,.*()]").Replace(text, " "));
        }

        delegate double MyDelegate3(double d);
        private static void task_3()
        {
            MyDelegate3 myDelegate3;
            double radius = 1;
            myDelegate3 = new CircleLengthCounter().count;
            Console.WriteLine("The length of the circle of radius" + radius + " is " + myDelegate3(radius));

            myDelegate3 = new CircleAreaCounter().count;
            Console.WriteLine("The area of the circle of radius" + radius + " is " + myDelegate3(radius));

            myDelegate3 = new SphereVolCounter().count;
            Console.WriteLine("The length of the circle of radius" + radius + " is " + myDelegate3(radius));
        }

        delegate int Operation(int a, int b);
        private static void task_4()
        {
            Dictionary<String, Operation> op = new Dictionary<String, Operation>();
            op["add"] = (a, b) => a + b;
            op["mult"] = (a, b) => a * b;
            op["substr"] = (a, b) => a - b;
            op["div"] = (a, b) => a / b;
            Console.WriteLine(op["add"](5, 3));
            Console.WriteLine(op["mult"](6, 7));
            Console.WriteLine(op["substr"](10, 20));
            Console.WriteLine(op["div"](100, 25));
        }

        delegate int UseOperation(int a, int b);
        delegate void GetGreeting();
        delegate double Oprtn(double a, double b);
        private static void task_5()
        {
            GetGreeting getGreeting;
            //1
            int hour = System.DateTime.Now.Hour;
            if (hour >= 0 && hour < 6)
            {
                getGreeting = GoodNight;
            } else if (hour >= 6 && hour < 13)
            {
                getGreeting = GoodMorning;
            } else if (hour >= 13 && hour < 18)
            {
                getGreeting = GoodDay;
            } else if (hour >= 18 && hour < 24)
            {
                getGreeting = GoodEvening;
            } else
            {
                throw new ArgumentException("Time must be 0 <= time <= 23");
            }
            getGreeting();

            //2
            Console.WriteLine("Please, enter two numbers (separated by a space)");
            String input = Console.ReadLine();
            double first;
            double second;
            try
            {
                String[] numbers = input.Split(new char[] {' '});
                first = double.Parse(numbers[0].Trim());
                second = double.Parse(numbers[1].Trim());
                Console.WriteLine("Please, enter the operation to perform [+ - / *]");
                char operation = Console.ReadLine().ElementAt(0);
                if (operation != '+' && operation != '-' && operation != '*' && operation != '/')
                {
                    throw new FormatException("Wrong input operation");
                }
                Dictionary<String, Oprtn> op = new Dictionary<String, Oprtn>();
                Oprtn oprtn;
                op["add"] = (a, b) => a + b;
                op["mult"] = (a, b) => a * b;
                op["substr"] = (a, b) => a - b;
                op["div"] = (a, b) => a / b;
                switch (operation)
                {
                    case '+':
                        oprtn = op["add"];
                        break;
                    case '-':
                        oprtn = op["substr"];
                        break;
                    case '*':
                        oprtn = op["mult"];
                        break;
                    case '/':
                        oprtn = op["div"];
                        break;
                    default:
                        throw new Exception("Unknown error");
                }
                if (operation == '/' && second == 0)
                {
                    throw new ArithmeticException("It is impossible to divide by zero");
                }
                Console.WriteLine(first + " " + operation + " " + second + " = " + oprtn(first, second));

            } catch(FormatException)
            {
                Console.WriteLine("Wrong format");
            } catch (ArithmeticException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        
        private static void GoodMorning()
        {
            Console.WriteLine("Good morning");
        }

        private static void GoodDay()
        {
            Console.WriteLine("Good day");
        }

        private static void GoodEvening()
        {
            Console.WriteLine("Good evening");
        }

        private static void GoodNight()
        {
            Console.WriteLine("Good night");
        }
    }

    class CircleLengthCounter
    {
        public double count(double r)
        {
            return 2 * Math.PI * r;
        }
    }

    class CircleAreaCounter
    {
        public double count(double r)
        {
            return Math.PI * r * r;
        }
    }

    class SphereVolCounter
    {
        public double count(double r)
        {
            return (4.0/3.0) * Math.PI * r * r * r;
        }
    }
}