using System.Collections.Generic;
using System;

public class GameEvent
{
    private static Dictionary<EGameEventType, Dictionary<int, List<Delegate>>> gameEvents = new();

    public static void Register<T>(EGameEventType gameEventType, Action<T> callback, int subId = -1)
        where T : GameEventDataBase
    {
        if (!gameEvents.TryGetValue(gameEventType, out Dictionary<int, List<Delegate>> callbackDict))
        {
            callbackDict = new Dictionary<int, List<Delegate>>();
            gameEvents[gameEventType] = callbackDict;
        }
        if (!callbackDict.TryGetValue(subId, out List<Delegate> callbackList))
        {
            callbackList = new List<Delegate>();
            callbackDict[subId] = callbackList;
        }
        callbackList.Add(callback);
    }

    public static void UnRegister<T>(EGameEventType gameEventType, Action<T> callback, int subId = -1)
        where T : GameEventDataBase
    {
        if (!gameEvents.TryGetValue(gameEventType, out Dictionary<int, List<Delegate>> callbackDict))
            return;
        if (!callbackDict.TryGetValue(subId, out List<Delegate> callbackList))
            return;

        callbackList.Remove(callback);

        if (callbackList.Count <= 0)
        {
            callbackDict.Remove(subId);
            if (callbackDict.Count <= 0)
            {
                gameEvents.Remove(gameEventType);
            }
        }
    }

    public static void Dispatch<T>(T gameEventData, int subId = -1)
        where T : GameEventDataBase, new()
    {
        if (!gameEvents.TryGetValue(gameEventData.gameEventType, out Dictionary<int, List<Delegate>> callbackDict))
            return;
        if (!callbackDict.TryGetValue(subId, out List<Delegate> callbackList))
            return;

        foreach (var callback in callbackList)
        {
            var cb = callback as Action<T>;
            cb?.Invoke(gameEventData);
        }

        GameEventDataPool.Recycle<T>(gameEventData);
    }
}
