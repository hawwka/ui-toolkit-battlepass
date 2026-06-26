using System;
using System.Collections.Generic;

namespace Project.UI.Core
{
    public sealed class ObservableProperty<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;

                _value = value;
                Changed?.Invoke(_value);
            }
        }

        public event Action<T> Changed;
    }
}
