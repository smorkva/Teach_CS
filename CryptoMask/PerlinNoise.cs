using System;

namespace CryptoMask
{
    /// <summary>
    /// Class for generate pseudo-ramdom numbers
    /// </summary>
    public class PerlinNoise
    {
        private int _offset = 0;
        private int _noise()
        {
            int _x = (_offset << 13) ^ _offset;
            _offset = (int)((1.0 - ((_x * (_x * _x * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0) * int.MaxValue);
            return Math.Abs(_offset);
        }
        public PerlinNoise()
        {
            _offset = 0;
        }
        public PerlinNoise(int seek)
        {
            _offset = seek;
        }
        /// <summary>
        /// Get next ramdom number
        /// </summary>
        /// <returns></returns>
        public byte Next()
        {
            return (byte)_noise();
        }
        /// <summary>
        /// Generate array with random data
        /// </summary>
        /// <param name="length">Array length</param>
        /// <returns>Data array</returns>
        public byte[] Generate(int length)
        {
            var _result = new byte[length];
            for (var _i = 0; _i < length; _i++)
            {
                _result[_i] = Next();
            }
            return _result;
        }
    }
}
