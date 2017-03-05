using System;
using System.Runtime.Serialization;

namespace Transport.Shema
{
    public class Car : DataTransport
    {
        public Car(string name, string number, byte wheelCount, int maxSpeed, bool installedGas) : 
            base(name, number, wheelCount, maxSpeed)
        {
            InstalledGas = installedGas;
        }

        [DataMember]
        public bool InstalledGas { get; set; }
    }
}
