using System;
using UnityEngine;

namespace Utils.Observables
{
    [Serializable]
    public class ObservableValue<T>
    {
        [SerializeField] private T value;

        private Action<T> _onChanged;

        public event Action<T> OnChanged
        {
            add
            {
                _onChanged += value;
                value?.Invoke(this.value);
            }
            remove => _onChanged -= value;
        }

        public T Value
        {
            get => value;
            set
            {
                if (!Equals(this.value, value))
                {
                    this.value = value;
                    _onChanged?.Invoke(this.value);
                }
            }
        }
        
        public void ForceInvoke() => _onChanged?.Invoke(value);
    }
}