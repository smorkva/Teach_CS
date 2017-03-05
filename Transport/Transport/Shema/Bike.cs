using System.Runtime.Serialization;

namespace Transport.Shema
{
    public class Bike : DataTransport
    {
        public Bike(string name, string number, byte wheelCount, int maxSpeed, string colorOfPedal, bool isElectric) : 
            base(name, number, wheelCount, maxSpeed)
        {
            IsElectric = isElectric;
            ColorOfPedal = colorOfPedal;
        }

        [DataMember]
        public string ColorOfPedal { get; set; }
        [DataMember]
        public bool IsElectric { get; set; }
    }
}
