using System.Text;

namespace CryptoMask
{
    public class CryptoProvider
    {
        private byte[] _key;
        /// <summary>
        /// Secret key
        /// </summary>
        public byte[] Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }
        /// <summary>
        /// Encode incomming data
        /// </summary>
        /// <param name="data">Data array</param>
        /// <returns>Encrypted data</returns>
        public byte[] Encode(byte[] data)
        {
            var _result = new byte[data.Length];

            for (var _i = 0; _i < data.Length; _i++)
            {
                _result[_i] = (byte)(data[_i] ^ _key[_i % _key.Length]);
            }
            return _result;
        }
        /// <summary>
        /// Encode incomming data
        /// </summary>
        /// <param name="data">Encrypted array</param>
        /// <returns>Decoded data</returns>
        public byte[] Decode(byte[] data)
        {
            return Encode(data);
        }
        /// <summary>
        /// Encode information
        /// </summary>
        /// <param name="data">Information string</param>
        /// <returns>Encrypted data</returns>
        public byte[] Encode(string data)
        {
            return Encode(UTF8Encoding.GetEncoding("utf-8").GetBytes(data));
        }
        /// <summary>
        /// Decode information
        /// </summary>
        /// <param name="data">Encrypted data</param>
        /// <returns>Decoded string</returns>
        public string DecodeToString(byte[] data)
        {
            return UTF8Encoding.GetEncoding("utf-8").GetString(Encode(data));
        }
    }
}
