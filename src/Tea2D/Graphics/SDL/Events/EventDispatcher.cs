using System;
using Silk.NET.SDL;

namespace Tea2D.Graphics.SDL.Events
{
    public readonly ref struct EventDispatcher
    {
        private readonly Event _event;
        
        public EventDispatcher(Event @event)
        {
            _event = @event;
        }

        public bool Dispatch<TEvent>(Action<TEvent> action)
            where TEvent : struct, IWindowEvent
        {
            var windowEvent = new TEvent
            {
                Event = _event
            };

            if ((EventType) _event.Type != windowEvent.GetEventType) 
                return false;

            action(windowEvent);
            return true;
        }
    }
}