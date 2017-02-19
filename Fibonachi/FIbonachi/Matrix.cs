using System;

namespace Fibonachi
{
    public class Matrix
    {
        public long a, b, c, d;
        public static Matrix operator *(Matrix leftMatrix, Matrix rightMatrix)
        {
            return new Matrix
            {
                a = leftMatrix.a * rightMatrix.a + leftMatrix.b * rightMatrix.c,
                b = leftMatrix.a * rightMatrix.b + leftMatrix.b * rightMatrix.d,
                c = leftMatrix.c * rightMatrix.a + leftMatrix.d * rightMatrix.c,
                d = leftMatrix.c * rightMatrix.b + leftMatrix.d * rightMatrix.d
            };
        }
        public static readonly Matrix Empty = new Matrix { a = 1, b = 0, c = 0, d = 1 };
        public static readonly Matrix Fibonachi = new Matrix { a = 1, b = 1, c = 1, d = 0 };
    }
}
