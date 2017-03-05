using System.Runtime.Serialization;

namespace Transport.Shema
{
    public class Bus : DataTransport
    {
        public Bus()
        {
        }
        
        [DataMember]
        public string Route { get; set; }
    }
}
