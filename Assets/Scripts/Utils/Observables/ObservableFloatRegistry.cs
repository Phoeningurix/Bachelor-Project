using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Observables
{
    [Serializable]
    public struct FloatEntry
    {
        public string key;
        public ObservableValue<float> value;
    }
    
    [Serializable]
    public class ObservableFloatRegistry : IEnumerable<KeyValuePair<string, ObservableValue<float>>>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<FloatEntry> entries = new();

        private Dictionary<string, ObservableValue<float>> _registry = new();

        public event Action<string, float> OnChanged;

        public ObservableValue<float> this[string key]
        {
            get => _registry[key];
            set
            {
                if (_registry.TryGetValue(key, out var existing))
                {
                    // key already exists - this shouldn't happen normally
                    //UnityEngine.Debug.LogWarning($"Key '{key}' already exist in ObservableFloatRegistry!");
                    existing.Value = value.Value;
                }
                else
                {
                    value.OnChanged += v => OnChanged?.Invoke(key, v);
                    _registry[key] = value;
                }
            }
        }

        public float GetBetween01(string key) => Mathf.Clamp01((this[key].Value + 1f) / 2f);

        public float GetMaxValue()
        {
            return _registry.Values.Max(v => v.Value);
        }

        public Dictionary<string, float> GetSoftMaxDictValues()
        {
            var exp = _registry.ToDictionary(
                kv => kv.Key,
                kv => MathF.Exp(kv.Value.Value)
            );

            var sum = exp.Values.Sum();

            return exp.ToDictionary(
                kv => kv.Key,
                kv => kv.Value / sum
            );
        }

        public IEnumerator<KeyValuePair<string, ObservableValue<float>>> GetEnumerator() => _registry.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            foreach (FloatEntry e in entries)
            {
                this[e.key] = e.value;
            }
        }
    }
}
