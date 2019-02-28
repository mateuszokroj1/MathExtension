using System;
using System.Collections.Generic;

namespace MathExtension
{
    public static class Math
    {
        /// <summary>
        /// Całkowanie metodą trapezów
        /// </summary>
        /// <param name="f">Funkcja do całkowania</param>
        /// <param name="n">Podział pola na n elementów</param>
        /// <param name="a">Granica dolna</param>
        /// <param name="b">Granica górna</param>
        /// <returns>Obliczona całka ograniczona</returns>
        public static double Integral(Func<double, double> f, uint n, double a, double b)
        {
            if (n == 0) throw new ArgumentException("n must be greater than 0.");
            double h = (b - a) / Convert.ToDouble(n);
            double S = 0;
            for (uint i = 1; i <= n; i++)
                S += f(a + Convert.ToDouble(i) * h);

            return (2d * S + f(a) + f(b)) * h / 2.0;
        }

        public static double Factorial(uint n)
        {
            switch(n)
            {
                case 0:
                case 1:
                    return 1.0;
                default:
                    return n * Factorial(n - 1);
            }
        }

        public static double Sin(double x)
        {
            double oldvalue = 0, newvalue = 0;

            uint n = 1;
            do
            {
                oldvalue = newvalue;
                newvalue += (System.Math.Pow(-1,n) * System.Math.Pow(x,2*n+1)) / Factorial(2*n+1);
                if (n == uint.MaxValue) break;
                n++;
            } while (System.Math.Abs(newvalue - oldvalue) > 0.00000000001);
            return newvalue;
        }

        public static double Cos(double x)
        {
            double oldvalue = 0, newvalue = 0;

            uint n = 1;
            do
            {
                oldvalue = newvalue;
                newvalue += (System.Math.Pow(-1,n)*System.Math.Pow(x,2*n)) / Factorial(2*n);
                if (n == uint.MaxValue) break;
                n++;
            } while (System.Math.Abs(newvalue - oldvalue) > 0.00000000001);
            return newvalue;
        }

        /// <summary>
        /// Exponential
        /// </summary>
        /// <returns>e^x</returns>
        public static double Exp(double x)
        {
            double oldvalue = 0, newvalue = 0;

            uint n = 1;
            do
            {
                oldvalue = newvalue;
                newvalue += System.Math.Pow (x,n) / Factorial(n);
                if (n == uint.MaxValue) break;
                n++;
            } while (System.Math.Abs(newvalue - oldvalue) > 0.00000000001);
            return newvalue;
        }

        public static double SquareRoot(double x)
        {
            if (x < 0) throw new ArgumentOutOfRangeException("x");

            if (x == 0) return 0;

            double y = 0;
            double newy = x / 2.0;
            while (System.Math.Abs(newy - y) > 0.00000000001)
            {
                y = newy;
                newy = 0.5 * (y + x / y);
            }
            return newy;
        }

        public static uint Mod(int a, uint b)
        {
            int r = 0;
            uint q = 1;
            do
            {
                r = a - (int)(q * b);
                q++;
            }
            while (r > 0);
            return (uint)(r < 0 ? a - (int)(--q)*(int)b : 0);
        }

        /// <summary>
        /// Euclides algorithm
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            while (a != b)
                if (a < b) b -= a;
                else a -= b;
            return a;
        }
        
        /// <summary>
        /// Euclides algorithm (recursive version)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GCD(long a, long b)
        {
            if (a != b)
                return GCD(a > b ? a-b : a, b > a ? b-a : b);
            else
                return a;
        }
    }
}
