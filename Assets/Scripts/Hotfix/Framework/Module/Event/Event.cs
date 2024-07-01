using System.Collections.Generic;
using System;

namespace Framework
{
    public class Event
    {
        private Dictionary<EEventType, EventData> eventDatas = new Dictionary<EEventType, EventData>();

        public void AddListener<T>(EEventType eventType, Action<T> handler, int subId = -1)
            where T : EventBase
        {
            if (handler == null)
                return;

            if (!eventDatas.TryGetValue(eventType, out EventData eventData))
            {
                eventData = new EventData(eventType);
                eventDatas.Add(eventType, eventData);
            }
            eventData.AddListener(handler, subId);
        }

        public void RemoveListener<T>(EEventType eventType, Action<T> handler, int subId = -1)
            where T : EventBase
        {
            if (handler == null)
                return;
            if (!eventDatas.TryGetValue(eventType, out EventData eventData))
                return;

            eventData.RemoveListener(handler, subId);
        }

        public void RemoveAllListener()
        {
            eventDatas.Clear();
        }

        public void Dispatch<T>(T data, int subId = -1)
            where T : EventBase, new()
        {
            if (!eventDatas.TryGetValue(data.EventType, out EventData eventData))
                return;

            eventData.Dispatch<T>(data, subId);

            EventPool.Recycle(data);
        }
    }
}