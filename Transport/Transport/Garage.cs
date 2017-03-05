using System;
using System.IO;
using Transport.Shema;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Transport
{
    using System.Collections;
    using Expression = Func<DataTransport, bool>;

    public class Garage : IEnumerable<DataTransport>
    {
        private Dictionary<int, DataTransport> _items;
        private int _lastIndex = 0;
        
        public Garage()
        {
            _items = new Dictionary<int, DataTransport>();
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
        public void Store(int key, DataTransport transport) => _items[key] = transport;
        public int Store(DataTransport transport)
        {
            var id = FindId(transport);

            if (!id.HasValue)
            {
                _items.Add(++_lastIndex, transport);
                return _lastIndex;
            }

            return id.GetValueOrDefault();
        }

        private KeyValuePair<int, DataTransport>? FindPair(int id)
        {
            var transport = default(DataTransport);
            if (_items.TryGetValue(id, out transport))
            {
                return new KeyValuePair<int, DataTransport>(id, transport);
            }

            return null;
        }
        private KeyValuePair<int, DataTransport>? FindPair(string name) => FindPair(x => x.Name == name);
        private KeyValuePair<int, DataTransport>? FindPair(Expression function)
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
        private KeyValuePair<int, DataTransport>? FindPair(DataTransport transport) => FindPair(_transport => _transport.Equals(transport));

        public int? FindId(DataTransport transport) => FindPair(transport)?.Key;
        public int? FindId(string name) => FindPair(name)?.Key;
        public int[] GetIds() => _items.Select(t => t.Key).ToArray();
        public int[] GetIds(Expression function) => GetEnumerator(function).Select(t => t.Key).ToArray();

        public DataTransport GetTransport(int id) => FindPair(id)?.Value;
        public DataTransport GetTransport(string name) => FindPair(name)?.Value;
        public DataTransport[] GetTransports() => _items.Select(t => t.Value).ToArray();
        public DataTransport[] GetTransports(Expression function) => GetEnumerator(function).Select(t => t.Value).ToArray();
        
        public void Remove(int key) => _items.Remove(key);
        public void Remove(string name)
        {
            var id = FindId(name);
            if (id.HasValue)
            {
                Remove(id.GetValueOrDefault());
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
                    _items = JsonConvert.DeserializeObject<Dictionary<int, DataTransport>>(json, new JsonSerializerSettings
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

        public IEnumerable<KeyValuePair<int, DataTransport>> GetEnumerator(Expression function)
        {
            foreach (var transport in _items)
            {
                if (function(transport.Value))
                {
                    yield return transport;
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public IEnumerator<DataTransport> GetEnumerator()
        {
            return new Enumerator(this);
        }

        public class Enumerator : IEnumerator<DataTransport>
        {
            private Garage _garage;
            private IEnumerator<int> _dictonaryKeys;

            public Enumerator(Garage garage)
            {
                _garage = garage;
                _dictonaryKeys = garage._items.Keys.GetEnumerator();
            }
            
            object IEnumerator.Current
            {
                get
                {
                    var key = _dictonaryKeys.Current;
                    return _garage._items[key];
                }
            }
            DataTransport IEnumerator<DataTransport>.Current
            {
                get
                {
                    var key = _dictonaryKeys.Current;
                    return _garage._items[key];
                }
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext() => _dictonaryKeys.MoveNext();
            public void Reset() => _dictonaryKeys.Reset();
        }
    }
}
