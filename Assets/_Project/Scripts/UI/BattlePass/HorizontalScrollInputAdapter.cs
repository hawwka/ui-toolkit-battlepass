using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project.UI.BattlePass
{
    public sealed class HorizontalScrollInputAdapter : IDisposable
    {
        private readonly ScrollView _scrollView;
        private bool _isDragging;
        private int _activePointerId = -1;
        private Vector2 _pointerStart;
        private Vector2 _scrollStart;

        public HorizontalScrollInputAdapter(ScrollView scrollView)
        {
            _scrollView = scrollView ?? throw new ArgumentNullException(nameof(scrollView));

            _scrollView.RegisterCallback<WheelEvent>(OnWheel);
            _scrollView.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _scrollView.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _scrollView.RegisterCallback<PointerUpEvent>(OnPointerUp);
            _scrollView.RegisterCallback<PointerCancelEvent>(OnPointerCancel);
        }

        public void Dispose()
        {
            _scrollView.UnregisterCallback<WheelEvent>(OnWheel);
            _scrollView.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            _scrollView.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            _scrollView.UnregisterCallback<PointerUpEvent>(OnPointerUp);
            _scrollView.UnregisterCallback<PointerCancelEvent>(OnPointerCancel);

            if (_activePointerId >= 0)
            {
                _scrollView.ReleasePointer(_activePointerId);
                _activePointerId = -1;
            }
        }

        private void OnWheel(WheelEvent evt)
        {
            var offset = _scrollView.scrollOffset;
            offset.x += evt.delta.y;
            _scrollView.scrollOffset = ClampOffset(offset);
            evt.StopPropagation();
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            if (_isDragging || evt.button != (int)MouseButton.LeftMouse)
                return;

            _isDragging = true;
            _activePointerId = evt.pointerId;
            _pointerStart = evt.position;
            _scrollStart = _scrollView.scrollOffset;
            _scrollView.CapturePointer(evt.pointerId);
            evt.StopPropagation();
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            if (!_isDragging || evt.pointerId != _activePointerId)
                return;

            var delta = (Vector2)evt.position - _pointerStart;
            var offset = _scrollStart;
            offset.x -= delta.x;
            _scrollView.scrollOffset = ClampOffset(offset);
            evt.StopPropagation();
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            if (!_isDragging || evt.pointerId != _activePointerId)
                return;

            EndDrag();
            evt.StopPropagation();
        }

        private void OnPointerCancel(PointerCancelEvent evt)
        {
            if (!_isDragging || evt.pointerId != _activePointerId)
                return;

            EndDrag();
            evt.StopPropagation();
        }

        private void EndDrag()
        {
            if (_activePointerId >= 0)
                _scrollView.ReleasePointer(_activePointerId);

            _isDragging = false;
            _activePointerId = -1;
        }

        private Vector2 ClampOffset(Vector2 offset)
        {
            var maxOffset = _scrollView.horizontalScroller.highValue;
            offset.x = Mathf.Clamp(offset.x, 0f, maxOffset);
            offset.y = 0f;
            return offset;
        }
    }
}
