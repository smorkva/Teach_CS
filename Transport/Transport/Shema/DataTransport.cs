using System;
using System.Runtime.Serialization;

namespace Transport.Shema
{
    public abstract class DataTransport : ICloneable
    {
        public DataTransport()
        {
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Number { get; set; }
        [DataMember]
        public byte WheelCount { get; set; }
        [DataMember]
        public int MaxSpeed { get; set; }
        
        protected bool Equals(DataTransport other)
        {
            return GetHashCode().Equals(other.GetHashCode());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (!obj.GetType().Equals(this.GetType()))
            {
                return false;
            }
            
            return this.Equals(obj as DataTransport);
        }
        public override int GetHashCode()
        {
            var type = this.GetType();
            var sign = default(string);

            foreach (var property in type.GetProperties())
            {
                sign += property.GetValue(this, null).ToString();
            }

            return sign.GetHashCode();
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
