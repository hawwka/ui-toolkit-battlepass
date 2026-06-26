using System;
using System.Collections.Generic;

namespace Project.UI.Core
{
    public sealed class DisposableBinding : IDisposable
    {
        private readonly List<Action> _unbindActions = new();

        public void Bind<T>(ObservableProperty<T> property, Action<T> onChanged)
        {
            onChanged(property.Value);
            property.Changed += onChanged;
            _unbindActions.Add(() => property.Changed -= onChanged);
        }

        public void Add(Action subscribe, Action unsubscribe)
        {
            subscribe();
            _unbindActions.Add(unsubscribe);
        }

        public void Dispose()
        {
            for (var i = _unbindActions.Count - 1; i >= 0; i--)
                _unbindActions[i]();

            _unbindActions.Clear();
        }
    }
}
