using System;

namespace ConsoleFractale
{
    class Program
    {
        
        private static string chars = ".,_-+^*&$%";

        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Console Fractale Generator v1.23");
            Console.WriteLine("\nControls:");
            Console.WriteLine("W, A, S, D to move up, left, down, right");
            Console.WriteLine("Up arrow to zoom in");
            Console.WriteLine("Down arrow to zoom out");
            Console.WriteLine("Left arrow to reduce iteration count by 10");
            Console.WriteLine("Right arrow to increment iteration count by 10");
            Console.WriteLine("J reduce iteration by 1");
            Console.WriteLine("L increment iteration by 1");
            Console.WriteLine("I to enter iteration count. [Press I then enter count, then press enter]");
            Console.WriteLine("\n[Press any button to start use this awesome application]");

            Console.ReadKey();

            Start();


        }


        private static void Start()
        {
            decimal windowSizeX = 150;
            decimal windowSizeY = 50;

            decimal zoom = 2;

            decimal posX = 0;
            decimal posY = 0;

            
            int iterations = 20;

            while (true)
            {
                decimal pixelSizeX = 3 * zoom / windowSizeX;
                decimal pixelSizeY = 2 * zoom / windowSizeY;

                Console.Clear();

                for (int y = 0; y < windowSizeY; y++)
                {
                    for (int x = 0; x < windowSizeX; x++)
                    {
                        Point point = new Point(posX - zoom - zoom + x * pixelSizeX, posY - zoom + y * pixelSizeY);
                        int infinityIteration = IsInMandelbrotSet(point, iterations);
                        if (infinityIteration == -1)
                        {
                            Console.Write('#');
                        }
                        else
                        {
                            //char sym = chars[(int)Math.Floor((double)infinityIteration / iterations * 10)];
                            //Console.Write(sym);
                            Console.Write('.');
                            /*if((int)Math.Floor((double)infinityIteration / iterations * 10) >= 10)
                            {
                                Console.WriteLine(infinityIteration);
                                
                            }*/
                        }
                    }
                    Console.WriteLine();
                }

                while (true)
                {
                    Console.WriteLine("Iterations: " + iterations);

                    ConsoleKey key = Console.ReadKey().Key;

                    if (key == ConsoleKey.W)
                    {
                        posY -= zoom / 2;
                    }
                    else if (key == ConsoleKey.A)
                    {
                        posX -= zoom / 2;
                    }
                    else if (key == ConsoleKey.S)
                    {
                        posY += zoom / 2;
                    }
                    else if (key == ConsoleKey.D)
                    {
                        posX += zoom / 2;
                    }
                    else if(key == ConsoleKey.UpArrow)
                    {
                        zoom -= zoom / new decimal(2);
                        Console.WriteLine(GC.GetTotalMemory(false));

                    }
                    else if(key == ConsoleKey.DownArrow)
                    {
                        zoom += zoom / new decimal(2);
                        Console.WriteLine(GC.GetTotalMemory(false));
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        iterations -= 10;
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        iterations += 10;
                    }
                    else if (key == ConsoleKey.J)
                    {
                        iterations--;
                    }
                    else if (key == ConsoleKey.L)
                    {
                        iterations++;
                    }
                    else if(key == ConsoleKey.I)
                    {
                        Console.WriteLine("Enter number of iterations: ");
                        int outIterations = 0;

                        if(Int32.TryParse(Console.ReadLine(), out outIterations))
                        {
                            if(outIterations > 0)
                            {
                                iterations = outIterations;
                            }
                            else
                            {
                                Console.WriteLine("Iterations can not be less than 1");
                            }
                        }
                        else
                        {
                            Console.WriteLine("It is not number");
                        }
                    }
                    else
                    {
                        continue;
                    }
                    break;
                }
            }
        }

        private static Point ComplexToThirdExtend(Point point)
        {
            // (A+B)^3 = A^3 + 3A^2 B + 3AB^2 + B^3

            Point newPoint = new Point
                (
                    X: point.X * point.X * point.X - 3 * point.X * (point.Y * point.Y),
                    Y: 3 * (point.X * point.X) * point.Y - point.Y * point.Y * point.Y
                );

            return newPoint;
        }

        private static Point ComplexToSecondExtend(Point point)
        {
            // (A+B)^2 = A^2 + 2ab + B^2

            Point newPoint = new Point
                    (
                        X: point.X * point.X - point.Y * point.Y,
                        Y: 2 * point.X * point.Y
                    );

            return newPoint;
        }

        private static Point ComlexAnotherAction(Point point, Point firstPoint)
        {

            Point newPoint = new Point
                    (
                        X: point.X * point.X - point.Y * point.Y,
                        Y: 2 * point.X * point.Y
                    );

            newPoint.X += firstPoint.X * Sin(firstPoint.X);
            newPoint.Y += firstPoint.Y * Sin(firstPoint.X);

            return newPoint;
        }

        private static decimal Sin(decimal x)
        {
            const int iterations = 30;
            decimal res = 0;
            decimal pow = x;
            decimal sign = 1;

            for (int i = 1; i < iterations; i++)
            {
                if (i % 2 == 1)
                {
                    res += pow * sign;
                    sign *= -1;
                }
                pow *= x / (i + 1);
            }

            return res;
        }

        static decimal Cos(decimal x)
        {
            const int iterations = 30;
            decimal res = 0;
            decimal pow = 1;
            decimal sign = 1;

            for (int i = 0; i < iterations; i++)
            {
                if (i % 2 == 0)
                {
                    res += pow * sign;
                    sign *= -1;
                }
                pow *= x / (i + 1);
            }

            return res;
        }

        private static int IsInMandelbrotSet(Point point, int iterations)
        {
            Point z = new Point(0, 0);

            for(int i = 0; i < iterations; i++)
            {
                z = ComplexToSecondExtend(z);

                z.X += point.X;
                z.Y += point.Y;

                if(Math.Abs(z.X - z.Y) > 2)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
