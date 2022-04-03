using Silk.NET.SDL;

namespace Tea2D.Graphics.SDL.Events
{
    public interface IWindowEvent
    {
        Event Event { init; }
        EventType GetEventType { get; }
    }

    public interface IWindowEvent<out TInnerEvent> : IWindowEvent
        where TInnerEvent : struct
    {
        TInnerEvent InnerEvent { get; }
    }

    public struct MouseButtonReleaseEvent : IWindowEvent<MouseButtonEvent>
    {
        public Event Event { private get; init; }
        public EventType GetEventType => EventType.Mousebuttonup;

        public MouseButtonEvent InnerEvent => Event.Button;
    }

    public struct MouseButtonPressEvent : IWindowEvent<MouseButtonEvent>
    {
        public Event Event { private get; init; }
        public EventType GetEventType => EventType.Mousebuttondown;

        public MouseButtonEvent InnerEvent => Event.Button;
    }
}