using System;

namespace MathExtension
{
    public static class Math
    {
        /// <summary>
        /// Całkowanie metodą trapezów
        /// </summary>
        /// <returns>Obliczona całka ograniczona</returns>
        public static double Integral(Func<double, double> f, uint n, double a, double b)
        {
            if (n == 0) throw new ArgumentException("n must be greater than 0.");
            double h = (b - a) / Convert.ToDouble(n);
            double S = 0;
            for (uint i = 1; i <= n; i++)
                S += f(a + Convert.ToDouble(i) * h);

            return (2d * S + f(a) + f(b)) * h / 2d;
        }

        /// <summary>
        /// Silnia
        /// </summary>
        /// <returns>Maksymalna wartość to n < 13</returns>
        public static uint Factorial(uint n)
        {
            if (n > 12) throw new ArgumentException("Należy użyć metody z wartościami ulong.");

            switch(n)
            {
                case 0:
                case 1:
                    return 1;
                default:
                    return n * Factorial(n - 1);
            }
        }

        public static ulong Factorial(ulong n)
        {
            if (n > 20) throw new ArgumentOutOfRangeException("Przekroczono maksymalną wartość wyjściową typu UInt64");

            switch(n)
            {
                case 0:
                case 1:
                    return 1L;
                default:
                    return n * Factorial(n - 1L);
            }
        }

        public static double Sin(double x)
        {
            double oldvalue = 0, newvalue = 0;

            ulong n = 1;
            while(Math.Abs(newvalue - oldvalue) > 0.000000001)
            {
                oldvalue = newvalue;
                newvalue += (Math.Pow(-1,n) * Math.Pow(x,2*n+1)) / Factorial(2*n+1);
                if (n == ulong.MaxValue) break;
                n++;
            }

            return newvalue;
        }

        public static double Cos(double x)
        {
            double oldvalue = 0, newvalue = 0;

            ulong n = 1;
            while(Math.Abs(newvalue - oldvalue) > 0.00000001)
            {
                oldvalue = newvalue;
                newvalue += (Math.Pow(-1,n)*Math.Pow(x,2*n)) / Factorial(2*n);
            }

            return newvalue;
        }

        public static double Exp(double x)
        {
            double oldvalue = 0, newvalue = 0;

            ulong n = 1;
            while(Math.Abs(newvalue - oldvalue) > 0.0000001)
            {
                oldvalue = newvalue;
                newvalue += Math.Pow (x,n) / Factorial(n);
                if (n == ulong.MaxValue) break;
                n++;
            }
            return newvalue;
        }
    }
}
