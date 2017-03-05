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
            var car = new Car("Test car", "as1230F", 5, 140, false);
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
            var car = new Car("Car", "as1230F", 5, 140, false);
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
            var car = new Car("Car", "as1230F", 5, 140, false);
            var anotherCar = new Car("Another car", "ae5622F", 4, 120, false);
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

            Assert.AreEqual(carName, transport.Name);
        }
        [TestMethod]
        public void GarageFindByIdTest()
        {
            var carName = "Bus 1";
            var garage = new Garage();
            _fillGarage(garage);
            var transport = garage.GetTransport(5);

            Assert.AreEqual(carName, transport.Name);
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
        public void GarageEnumerateTest()
        {
            var garage = new Garage();
            _fillGarage(garage);

            var enumerator = garage.GetEnumerator(t => t.MaxSpeed < 90).Select(t => t.Value).ToArray();
            var filtred = _carList.Where(t => t.MaxSpeed < 90).ToArray();

            CollectionAssert.AreEqual(filtred, enumerator);
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
            var result = new uint[] { 1, 2 };
            var garage = new Garage();
            var car = new Car("One car", "et2314fd", 4, 123, false);
            garage.Store(car);
            garage.Save(pathToFile);

            var newGarage = new Garage(pathToFile);
            var car2 = new Car("Two car", "wr6346fg", 3, 98, false);
            newGarage.Store(car2);
            var ids = newGarage.GetIds();

            CollectionAssert.AreEqual(result, ids);
        }

        private DataTransport[] _carList = new DataTransport[] 
        {
            new Car("Car 1", "ed3456V", 4, 120, true),
            new Car("Car 2", "sd3465J", 5, 230, false),
            new Car("Car 3", "bv4563B", 4, 150, false),
            new Car("Car 4", "xc2345N", 4, 110, true),
            new Bus("Bus 1", "nb2345M", 4, 90, "A-B"),
            new Bus("Bus 2", "xc2456E", 8, 140, "A-C"),
            new Bus("Bus 3", "qw3456T", 6, 120, "C-B"),
            new Bus("Bus 4", "ab3567Y", 4, 100, "A-D"),
            new Bike("Bike 1", "re7257W", 2, 60, "Red", true),
            new Bike("Bike 2", "bf4562D", 2, 28, "Green", false),
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
