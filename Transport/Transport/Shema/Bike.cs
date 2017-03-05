using System.Runtime.Serialization;

namespace Transport.Shema
{
    public class Bike : DataTransport
    {
        public Bike()
        {
        }

        [DataMember]
        public string ColorOfPedal { get; set; }
        [DataMember]
        public bool IsElectric { get; set; }
    }
}
