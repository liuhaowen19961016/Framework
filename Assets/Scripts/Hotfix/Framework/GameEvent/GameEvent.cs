using System.Collections.Generic;
using System;

public class GameEvent
{
    private static Dictionary<EGameEventType, Dictionary<int, List<Action<GameEventData>>>> gameEvents = new();

    public static void Register(EGameEventType gameEventType, Action<GameEventData> callback, int subId = -1)
    {
        if (!gameEvents.TryGetValue(gameEventType, out Dictionary<int, List<Action<GameEventData>>> gameEventDataDict))
        {
            gameEventDataDict = new Dictionary<int, List<Action<GameEventData>>>();
            gameEvents[gameEventType] = gameEventDataDict;
        }
        if (!gameEventDataDict.TryGetValue(subId, out List<Action<GameEventData>> gameEventDataList))
        {
            gameEventDataList = new List<Action<GameEventData>>();
            gameEventDataDict[subId] = gameEventDataList;
        }
        gameEventDataList.Add(callback);
    }

    public static void UnRegister(EGameEventType gameEventType, Action<GameEventData> callback, int subId = -1)
    {
        if (gameEvents.TryGetValue(gameEventType, out Dictionary<int, List<Action<GameEventData>>> gameEventDataDict))
        {
            if (gameEventDataDict.TryGetValue(subId, out List<Action<GameEventData>> gameEventDataList))
            {
                gameEventDataList.Remove(callback);
                if (gameEventDataList.Count <= 0)
                {
                    gameEventDataDict.Remove(subId);
                    if (gameEventDataDict.Count <= 0)
                    {
                        gameEvents.Remove(gameEventType);
                    }
                }
            }
        }
    }

    public static void Dispatch<T>(GameEventData gameEventData, int subId = -1)
        where T : GameEventData, new()
    {
        if (gameEvents.TryGetValue(gameEventData.gameEventType, out Dictionary<int, List<Action<GameEventData>>> gameEventDataDict))
        {
            if (gameEventDataDict.TryGetValue(subId, out List<Action<GameEventData>> gameEventDataList))
            {
                foreach (var callback in gameEventDataList)
                {
                    callback?.Invoke(gameEventData);
                }
            }
        }

        GameEventDataPool.Recycle<T>(gameEventData);
    }
}
