using Microsoft.VisualStudio.TestTools.UnitTesting;
using CryptoMask;

namespace Crypto.Test
{
    [TestClass]
    public class CryptTest
    {
        [TestMethod]
        public void KeyMoreThanDataTest()
        {
            const int _keyLength = 25;
            const int _dataLength = 12;

            var _noiser = new PerlinNoise();
            var _secretData = _noiser.Generate(_dataLength);

            var _crypter = new CryptoProvider();
            _crypter.Key = _noiser.Generate(_keyLength);

            var _crypted = _crypter.Encode(_secretData);
            var _deCrypted = _crypter.Decode(_crypted);

            CollectionAssert.AreEqual(_secretData, _deCrypted);
        }

        [TestMethod]
        public void KeyLessThanDataTest()
        {
            const int _keyLength = 12;
            const int _dataLength = 256;

            var _noiser = new PerlinNoise();
            var _secretData = _noiser.Generate(_dataLength);

            var _crypter = new CryptoProvider();
            _crypter.Key = _noiser.Generate(_keyLength);

            var _crypted = _crypter.Encode(_secretData);
            var _deCrypted = _crypter.Decode(_crypted);

            CollectionAssert.AreEqual(_secretData, _deCrypted);
        }

        [TestMethod]
        public void CryptStringTest()
        {
            const int _keyLength = 12;

            var _noiser = new PerlinNoise();
            var _secretData = "Однажды, в студеную зимнюю пору я из лесу вышел; был сильный мороз.";

            var _crypter = new CryptoProvider();
            _crypter.Key = _noiser.Generate(_keyLength);

            var _crypted = _crypter.Encode(_secretData);
            var _deCrypted = _crypter.DecodeToString(_crypted);

            Assert.AreEqual(_secretData, _deCrypted);
        }
    }
}
