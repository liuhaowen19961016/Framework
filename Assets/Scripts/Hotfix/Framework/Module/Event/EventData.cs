using System;
using System.Collections.Generic;

namespace Framework
{
    public class EventData
    {
        private EEventType eventType;
        private Dictionary<int, List<Delegate>> handlers = new Dictionary<int, List<Delegate>>();
        private List<Delegate> executedHandlers = new List<Delegate>();

        public EventData(EEventType eventType)
        {
            this.eventType = eventType;
        }

        public void AddListener(Delegate callback, int subId = -1)
        {
            if (!handlers.TryGetValue(subId, out List<Delegate> list))
            {
                list = new List<Delegate>();
                handlers.Add(subId, list);
            }
            list.Add(callback);
        }

        public void RemoveListener(Delegate callback, int subId = -1)
        {
            if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
                return;

            callbacks.Remove(callback);
        }

        public void Dispatch<T>(T arg1, int subId = -1)
            where T : EventBase
        {
            if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
                return;

            for (int i = 0; i < callbacks.Count; i++)
            {
                executedHandlers.Add(callbacks[i]);
                var callback = callbacks[i] as Action<T>;
                callback?.Invoke(arg1);
            }

            // for (int i = 0; i < callbacks.Count; i++)
            // {
            //     if (executedHandlers.Contains(callbacks[i]))
            //         continue;
            //     var callback = callbacks[i] as Action<T>;
            //     callback?.Invoke(arg1);
            // }
            // executedHandlers.Clear();
        }
    }
}