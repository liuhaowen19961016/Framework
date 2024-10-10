using System;
using System.Collections.Generic;

public class ConfigMgr : ManagerBase
{
    private ConfigMgr()
    {
    }

    private const string CONFIG_JSON_DIR = "Assets/Res/Configs/";
    private Dictionary<Type, Dictionary<int, ConfigBase>> configCache = new Dictionary<Type, Dictionary<int, ConfigBase>>(); //缓存

    //public FieldType GetConstConfig<ConfigType, FieldType>(string cfgKey)
    //    where ConfigType : ConfigBase
    //{
    // try
    // {
    //     var obj = GetConfigs<ConfigType>()[0];
    //     var props = obj.GetType().GetProperties();
    //     foreach (var prop in props)
    //     {
    //         if (prop.Name == cfgKey)
    //         {
    //             return (FieldType)prop.GetValue(obj);
    //         }
    //     }
    //     Debug.LogError($"读取{typeof(ConfigType)}表的key：{cfgKey}失败");
    //     return default(FieldType);
    // }
    // catch (Exception e)
    // {
    //     Debug.LogError($"读取{typeof(ConfigType)}表的key：{cfgKey}失败，{e}");
    //     return default(FieldType);
    // }
    //}

    public T GetConfig<T>(int id, bool showError = true)
        where T : ConfigBase
    {
        // Type type = typeof(T);
        // if (!configCache.TryGetValue(type, out var rowDict))
        // {
        //     rowDict = new Dictionary<int, ConfigBase>();
        //     List<T> configs = GetConfigs<T>();
        //     if (configs == null)
        //     {
        //         if (showError)
        //         {
        //             Debug.LogError($"读取{type}表的id：{id}失败");
        //         }
        //         return null;
        //     }
        //     foreach (var config in configs)
        //     {
        //         if (rowDict.ContainsKey(config.GetId()))
        //         {
        //             Debug.LogError($"{type}表的id：{config.GetId()}重复");
        //             continue;
        //         }
        //         rowDict.Add(config.GetId(), config);
        //     }
        //     configCache.Add(type, rowDict);
        // }
        //
        // if (rowDict.TryGetValue(id, out var rowCfg))
        // {
        //     return rowCfg as T;
        // }
        // if (showError)
        // {
        //     Debug.LogError($"读取{type}表的id：{id}失败");
        // }
        return null;
    }

    public List<T> GetConfigs<T>()
        where T : ConfigBase
    {
        // string fileName = typeof(T).Name.Replace("Table_", "").ToLower();
        // string configPath = CONFIG_JSON_DIR + fileName;
        // TextAsset jsonFile = GameGlobal.GetMgr<ResMgr>().GetRes<TextAsset>(fileName).GetInstance(GameGlobal.DontDestoryRoot);
        // if (jsonFile == null)
        // {
        //     return null;
        // }
        // List<T> configs = JsonConvert.DeserializeObject<List<T>>(jsonFile.text);
        //return configs;
        return null;
    }

    public override void Dispose()
    {
        configCache?.Clear();
    }
}