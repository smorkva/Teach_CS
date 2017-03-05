using System.Runtime.Serialization;

namespace Transport.Shema
{
    public class Bus : DataTransport
    {
        public Bus(string name, string number, byte wheelCount, int maxSpeed, string route) :
            base(name, number, wheelCount, maxSpeed)
        {
            this.Route = route;
        }
        
        [DataMember]
        public string Route { get; set; }
    }
}
