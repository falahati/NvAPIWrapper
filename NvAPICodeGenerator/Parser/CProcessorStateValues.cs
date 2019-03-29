using System.Collections.Generic;
using System.Linq;

namespace NvAPICodeGenerator.Parser
{
    internal class CProcessorStateValues
    {
        private readonly Dictionary<string, object> _valueDictionary = new Dictionary<string, object>();

        public string[] Keys
        {
            get => _valueDictionary.Keys.ToArray();
        }

        public object[] Values
        {
            get => _valueDictionary.Values.ToArray();
        }

        public T GetValue<T>(string key)
        {
            if (_valueDictionary.ContainsKey(key))
            {
                return (T) _valueDictionary[key];
            }

            return default(T);
        }

        public void SetValue<T>(string key, T value)
        {
            if (_valueDictionary.ContainsKey(key))
            {
                _valueDictionary[key] = value;
            }
            else
            {
                _valueDictionary.Add(key, value);
            }
        }
    }
}