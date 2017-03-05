using Transport;
using Transport.Shema;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBaseTest
{
    [TestClass]
    public class TestGarage
    {
        [TestMethod]
        public void GarageInsertTransportCloneTest()
        {
            var garage = new Garage();
            var car = new Car() { Name = "Test car", Number = "as1230F", WheelCount = 5, MaxSpeed = 140, InstalledGas = false };
            garage.Store(car);
            var copy = car.Clone();
            garage.Store(copy as Car);

            Assert.AreEqual(1, garage.Count);
        }
        [TestMethod]
        public void GarageEditTransportTest()
        {
            var garage = new Garage();
            var writedName = "New name";
            var car = new Car(){ Name = "Car", Number = "as1230F", WheelCount = 5, MaxSpeed = 140, InstalledGas = false };
            var id = garage.Store(car);

            car.Name = writedName;
            garage.Store(id, car);
            var afterStore = garage.GetTransport(id);

            Assert.AreEqual(writedName, afterStore?.Name);
        }
        [TestMethod]
        public void GarageCountTransportTest()
        {
            var garage = new Garage();
            var car = new Car() { Name = "Car", Number = "as1230F", WheelCount = 5, MaxSpeed = 140, InstalledGas = false };
            var anotherCar = new Car() { Name = "Another car", Number = "ae5622F", WheelCount = 4, MaxSpeed = 120, InstalledGas = false };
            garage.Store(car);            
            garage.Store(anotherCar);

            Assert.AreEqual(2, garage.Count);
        }
        [TestMethod]
        public void GarageFindByNameTest()
        {
            var carName = "Car 2";
            var garage = new Garage();
            _fillGarage(garage);
            var transport = garage.GetTransport(carName);

            Assert.AreEqual(carName, transport?.Name);
        }
        [TestMethod]
        public void GarageFindByIdTest()
        {
            var carName = "Bus 1";
            var garage = new Garage();
            _fillGarage(garage);
            var transport = garage.GetTransport(5);

            Assert.AreEqual(carName, transport?.Name);
        }
        [TestMethod]
        public void GarageFindByExpressionTest()
        {
            var garage = new Garage();
            _fillGarage(garage);

            var transports = garage.GetTransports(t => t.MaxSpeed < 90);
            var filtred = _carList.Where(t => t.MaxSpeed < 90).ToArray();

            CollectionAssert.AreEqual(filtred, transports);
        }
        [TestMethod]
        public void GarageFindByWrongNameTest()
        {
            var carName = "Wrong name";
            var garage = new Garage();
            _fillGarage(garage);
            var transport = garage.GetTransport(carName);

            Assert.AreNotEqual(carName, transport?.Name);
        }
        [TestMethod]
        public void GarageFindByWrongIdTest()
        {
            var carName = "Bus 1";
            var garage = new Garage();
            _fillGarage(garage);
            var transport = garage.GetTransport(50);

            Assert.AreNotEqual(carName, transport?.Name);
        }
        [TestMethod]
        public void GarageFindByWrongExpressionTest()
        {
            var garage = new Garage();
            _fillGarage(garage);

            var transports = garage.GetTransports(t => t.MaxSpeed == 13);
            var filtred = _carList.Where(t => t.MaxSpeed < 90).ToArray();

            CollectionAssert.AreNotEqual(filtred, transports);
        }
        [TestMethod]
        public void GarageEnumerateTest()
        {
            var garage = new Garage();
            _fillGarage(garage);

            var enumerator = garage.GetEnumerator(t => t.MaxSpeed < 90).Select(t => t.Value).ToArray();
            var filtred = _carList.Where(t => t.MaxSpeed < 90).ToArray();

            CollectionAssert.AreEqual(filtred, enumerator);
        }
        [TestMethod]
        public void GarageManualEnumerateTest()
        {
            var garage = new Garage();
            _fillGarage(garage);

            var fromManualEnumerator = garage.ToArray();

            CollectionAssert.AreEqual(_carList, fromManualEnumerator);
        }
        [TestMethod]
        public void GarageRemoveByNameTest()
        {
            var garage = new Garage();
            _fillGarage(garage);
            var result = "Car 2|Car 3|Car 4|Bus 1|Bus 3|Bus 4|Bike 2";

            garage.Remove("Car 1");
            garage.Remove("Bike 1");
            garage.Remove("Bus 2");

            Assert.AreEqual(result, garage.ToString());
        }
        [TestMethod]
        public void GarageRemoveByIdTest()
        {
            var garage = new Garage();
            _fillGarage(garage);
            var result = "Car 2|Car 3|Car 4|Bus 1|Bus 3|Bus 4|Bike 2";

            garage.Remove(1);
            garage.Remove(6);
            garage.Remove(9);

            Assert.AreEqual(result, garage.ToString());
        }
        [TestMethod]
        public void GarageRemoveByExpressionTest()
        {
            var garage = new Garage();
            _fillGarage(garage);
            var result = "Car 1|Car 2|Car 3|Car 4|Bus 2|Bus 3|Bus 4";

            garage.Remove(t => t.MaxSpeed <= 90);

            Assert.AreEqual(result, garage.ToString());
        }

        [TestMethod]
        public void GarageReadWriteTest()
        {
            string pathToFile = "./base.json";
            var garage = new Garage();
            var newGarage = new Garage();
            _fillGarage(garage);

            garage.Save(pathToFile);
            newGarage.Load(pathToFile);
            var transportAfterLoad = newGarage.GetTransports();

            CollectionAssert.AreEqual(_carList, transportAfterLoad);
        }
        [TestMethod]
        public void GarageInsertAfterLoadTest()
        {
            string pathToFile = "./base.json";
            var result = new int[] { 1, 2 };
            var garage = new Garage();
            var car = new Car() { Name = "One car", Number = "et2314fd", WheelCount = 4, MaxSpeed = 123, InstalledGas = false };
            garage.Store(car);
            garage.Save(pathToFile);

            var newGarage = new Garage(pathToFile);
            var car2 = new Car() { Name = "Two car", Number = "wr6346fg", WheelCount = 3, MaxSpeed = 98, InstalledGas = false };
            newGarage.Store(car2);
            var ids = newGarage.GetIds();

            CollectionAssert.AreEqual(result, ids);
        }

        private DataTransport[] _carList = new DataTransport[]
        {
            new Car() {Name = "Car 1", Number = "ed3456V", WheelCount = 4, MaxSpeed = 120, InstalledGas = true },
            new Car() {Name = "Car 2", Number = "sd3465J", WheelCount = 5, MaxSpeed = 230, InstalledGas = false },
            new Car() {Name = "Car 3", Number = "bv4563B", WheelCount = 4, MaxSpeed = 150, InstalledGas = false },
            new Car() {Name = "Car 4", Number = "xc2345N", WheelCount = 4, MaxSpeed = 110, InstalledGas = true },
            new Bus() {Name = "Bus 1", Number = "nb2345M", WheelCount = 4, MaxSpeed = 90,  Route = "A-B" },
            new Bus() {Name = "Bus 2", Number = "xc2456E", WheelCount = 8, MaxSpeed = 140, Route = "A-C" },
            new Bus() {Name = "Bus 3", Number = "qw3456T", WheelCount = 6, MaxSpeed = 120, Route = "C-B" },
            new Bus() {Name = "Bus 4", Number = "ab3567Y", WheelCount = 4, MaxSpeed = 100, Route = "A-D" },
            new Bike() {Name = "Bike 1", Number = "re7257W", WheelCount = 2, MaxSpeed = 60, ColorOfPedal = "Red", IsElectric = true },
            new Bike() {Name = "Bike 2", Number = "bf4562D", WheelCount = 2, MaxSpeed = 28, ColorOfPedal = "Green", IsElectric = false },
        };
        private void _fillGarage(Garage garage)
        {
            foreach (var transport in _carList)
            {
                garage.Store(transport);
            }
        }
    }

}
