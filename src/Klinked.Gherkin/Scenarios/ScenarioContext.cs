using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Klinked.Gherkin.Scenarios
{
    public class ScenarioContext
    {
        private readonly ConcurrentDictionary<string, object> _data;

        internal ScenarioContext()
        {
            _data = new ConcurrentDictionary<string, object>();
        }
        
        public void Set<T>(string key, T value)
        {
            if (_data.TryGetValue(key, out var existingValue))
                _data.TryUpdate(key, value, existingValue);
            else
                _data.TryAdd(key, value);
        }

        public T Get<T>(string key)
        {
            return _data.TryGetValue(key, out var value)
                ? (T) value
                : default(T);
        }
    }
}