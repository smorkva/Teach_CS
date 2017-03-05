using System;
using System.Runtime.Serialization;

namespace Transport.Shema
{
    public class Car : DataTransport
    {
        public Car()
        {
        }

        [DataMember]
        public bool InstalledGas { get; set; }
    }
}
