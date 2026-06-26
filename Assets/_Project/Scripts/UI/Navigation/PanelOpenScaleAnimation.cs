using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UI.Navigation
{
    public sealed class PanelOpenScaleAnimation : IDisposable
    {
        private static readonly float[] ScaleSteps = { 1f, 1.008f, 1.018f, 1.025f, 1.018f, 1.008f, 1f };

        private readonly VisualElement _target;
        private readonly int _stepDelayMs;
        private int _stepIndex;
        private IVisualElementScheduledItem _scheduledItem;
        private bool _disposed;

        public static PanelOpenScaleAnimation Play(VisualElement panelRoot, int stepDelayMs = 50)
        {
            var target = panelRoot?.Q(className: "playrix-panel") ?? panelRoot;
            var animation = new PanelOpenScaleAnimation(target, stepDelayMs);
            animation.Start();
            return animation;
        }

        private PanelOpenScaleAnimation(VisualElement target, int stepDelayMs)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _stepDelayMs = stepDelayMs;
        }

        private void Start()
        {
            _target.style.transformOrigin = new TransformOrigin(Length.Percent(50), Length.Percent(50));
            _stepIndex = 0;
            _scheduledItem = _target.schedule.Execute(Tick).Every(_stepDelayMs);
        }

        private void Tick()
        {
            if (_disposed)
                return;

            if (_stepIndex >= ScaleSteps.Length)
            {
                Dispose();
                return;
            }

            var scale = ScaleSteps[_stepIndex];
            _target.style.scale = new Scale(new Vector2(scale, scale));
            _stepIndex++;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _scheduledItem?.Pause();
            _scheduledItem = null;
            ResetTransform();
        }

        private void ResetTransform()
        {
            if (_target == null)
                return;

            _target.style.scale = StyleKeyword.Null;
            _target.style.transformOrigin = StyleKeyword.Null;
        }
    }
}
