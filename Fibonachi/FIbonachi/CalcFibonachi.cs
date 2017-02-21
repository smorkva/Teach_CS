using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fibonachi
{
    public static class CalcFibonachi
    {
        private static long _iterate(int n, long current, long prev)
        {
            if (n > 1)
            {
                return _iterate(n - 1, current + prev, current);
            }
            else
            {
                return current;
            }
        }
        private static void _iterate(int n, ref long current, ref long prev)
        {
            if (n > 1)
            {
                _iterate(n - 1, ref current, ref prev);
                current += prev;
                prev = current - prev;
            }
        }
        public static long MethodOne(int n)
        {
            return _iterate(50, 1, 0);
        }
        public static long MethodTwo(int n)
        {
            long current = 1;
            long prev = 0;

            _iterate(n, ref current, ref prev);

            return current;
        }
        public static long MethodThree(int n)
        {
            long current = 1;
            long prev = 0;
            long result = 0;

            for (var i = 1; i <= n-1; i++)
            {
                result = prev + current;
                prev = current;
                current = result;
            }

            return result;
        }
        public static long TestMethodFour(int n)
        {
            var _operand = Matrix.Fibonachi;

            for (int i = 0; i < n; i++)
            {
                _operand *= Matrix.Fibonachi;
            }

            return _operand.d;
        }
    }
}
