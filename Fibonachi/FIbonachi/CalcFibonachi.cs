using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fibonachi
{
    public static class CalcFibonachi
    {
        public static long MethodOne(int n)
        {
            return n > 1 ? MethodOne(n - 1) + MethodOne(n - 2) : n;
        }
        public static long MethodTwo(int n)
        {
            long _x = 1;
            long _y = 0;
            long _result = 0;
            for (var _i = 1; _i <= n; _i++)
            {
                _result = _x + _y;
                _x = _y;
                _y = _result;
            }
            return _result;
        }
        private static Matrix _power(Matrix matrix, int n)
        {
            var _operand = Matrix.Fibonachi;
            for (int i = 0; i < n; i++)
            {
                matrix *= _operand;
            }
            return matrix;
        }
        public static long MethodThree(int n)
        {
            var _matrix = Matrix.Fibonachi;
            _matrix = _power(_matrix, n);
            return _matrix.d;
        }
    }
}
