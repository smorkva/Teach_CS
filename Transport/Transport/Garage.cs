using System;
using System.IO;
using Transport.Shema;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Transport
{
    using Expression = Func<DataTransport, bool>;

    public class Garage
    {
        private Dictionary<uint, DataTransport> _items;
        private uint _lastIndex = 0;
        
        public Garage()
        {
            _items = new Dictionary<uint, DataTransport>();
        }
        public Garage(string path)
        {
            Load(path);
        }
        public override string ToString()
        {
            var itemlist = default(string);
            foreach (var pair in _items)
            {
                itemlist += $"{pair.Value.Name}|";
            }
            return itemlist.Remove(itemlist.Length - 1, 1);
        }
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }
        public void Store(uint key, DataTransport transport) => _items[key] = transport;
        public uint Store(DataTransport transport)
        {
            var id = FindId(transport);

            if (null == id)
            {
                _items.Add(++_lastIndex, transport);
                return _lastIndex;
            }

            return (uint)id;
        }

        private KeyValuePair<uint, DataTransport>? FindPair(uint id)
        {
            var transport = default(DataTransport);
            if (_items.TryGetValue(id, out transport))
            {
                return new KeyValuePair<uint, DataTransport>(id, transport);
            }

            return null;
        }
        private KeyValuePair<uint, DataTransport>? FindPair(string name) => FindPair(x => x.Name == name);
        private KeyValuePair<uint, DataTransport>? FindPair(Expression function)
        {
            var transports = GetEnumerator(function);

            foreach (var pair in _items)
            {
                if (function(pair.Value))
                {
                    return pair;
                }
            }

            return null;
        }
        private KeyValuePair<uint, DataTransport>? FindPair(DataTransport transport) => FindPair(_transport => _transport.Equals(transport));

        public uint? FindId(DataTransport transport) => FindPair(transport)?.Key;
        public uint? FindId(string name) => FindPair(name)?.Key;
        public uint[] GetIds() => _items.Select(t => t.Key).ToArray();
        public uint[] GetIds(Expression function) => GetEnumerator(function).Select(t => t.Key).ToArray();

        public DataTransport GetTransport(uint id) => FindPair(id)?.Value;
        public DataTransport GetTransport(string name) => FindPair(name)?.Value;
        public DataTransport[] GetTransports() => _items.Select(t => t.Value).ToArray();
        public DataTransport[] GetTransports(Expression function) => GetEnumerator(function).Select(t => t.Value).ToArray();
        
        public IEnumerable<KeyValuePair<uint, DataTransport>> GetEnumerator(Expression function)
        {
            foreach (var transport in _items)
            {
                if (function(transport.Value))
                {
                    yield return transport;
                }
            }
        }

        public void Remove(uint key) => _items.Remove(key);
        public void Remove(string name)
        {
            var id = FindId(name);
            if (null != id)
            {
                Remove((uint)id);
            }
        }
        public void Remove(Expression function)
        {
            var removedItems = GetIds(function);
            foreach (var transport in removedItems)
            {
                _items.Remove(transport);
            }
        }

        public void Load(string path)
        {
            try
            {
                using (var file = new StreamReader(path))
                {
                    var json = file.ReadToEnd();
                    _items = JsonConvert.DeserializeObject<Dictionary<uint, DataTransport>>(json, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    _lastIndex = _items.Max(t => t.Key);
                }
            }
            catch (FileNotFoundException)
            {
                Logger.Log("File not found. Creating Empty");
                new Garage();
            }
            catch (Exception exception)
            {
                Logger.Log(exception.ToString());
            }
        }
        public void Save(string path)
        {
            try
            {
                var json = JsonConvert.SerializeObject(_items, Formatting.Indented, new JsonSerializerSettings {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                using (var file = new StreamWriter(path, false))
                {
                    file.WriteLine(json);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

    }
}
