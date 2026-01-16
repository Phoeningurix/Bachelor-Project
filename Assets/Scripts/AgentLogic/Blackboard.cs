using System;
using System.Collections.Generic;

namespace AgentLogic
{
    public class Blackboard
    {
        private readonly Dictionary<string, object> _blackboard = new();

        public void Set(string key, object value)
        {
            _blackboard[key] = value;
        }

        public T Get<T>(string key)
        {
            if (!_blackboard.TryGetValue(key, out var value))
            {
                throw new KeyNotFoundException(
                    $"Blackboard key '{key}' was not found."
                );
            }

            if (value is not T typedValue)
            {
                throw new InvalidCastException(
                    $"Blackboard key '{key}' contains value of type '{value.GetType().Name}', " +
                    $"but was requested as '{typeof(T).Name}'."
                );
            }

            return typedValue;
        }

        public Type GetType(string key)
        {
            if (!_blackboard.TryGetValue(key, out var value))
            {
                throw new KeyNotFoundException(
                    $"Blackboard key '{key}' was not found."
                );
            }
            return value.GetType();
        }

        public void Remove(string key)
        {
            _blackboard.Remove(key);
        }

        public bool Contains(string key)
        {
            return _blackboard.ContainsKey(key);
        }
        
    }
}