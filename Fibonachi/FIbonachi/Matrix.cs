using System;

namespace Fibonachi
{
    public class Matrix
    {
        public long a;
        public long b;
        public long c;
        public long d;

        public static readonly Matrix Empty = new Matrix { a = 1, b = 0, c = 0, d = 1 };
        public static readonly Matrix Fibonachi = new Matrix { a = 1, b = 1, c = 1, d = 0 };
        public static Matrix operator *(Matrix left, Matrix right)
        {
            return new Matrix
            {
                a = left.a * right.a + left.b * right.c,
                b = left.a * right.b + left.b * right.d,
                c = left.c * right.a + left.d * right.c,
                d = left.c * right.b + left.d * right.d
            };
        }
    }
}