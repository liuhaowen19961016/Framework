using UnityEngine;

/// <summary>
/// Mono单例模版
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static GameObject root;

    private static T _Ins = null;
    public static T Ins
    {
        get
        {
            if (!Application.isPlaying)
                return null;
            if (root == null)
            {
                root = GameObject.Find("MonoSingletonRoot");
                if (root == null)
                {
                    root = new GameObject("MonoSingletonRoot");
                    DontDestroyOnLoad(root);
                }
            }
            if (_Ins == null)
            {
                _Ins = FindObjectOfType(typeof(T)) as T;
                string name = typeof(T).ToString();
                if (_Ins == null)
                {
                    _Ins = new GameObject(name, typeof(T)).GetComponent<T>();
                }
                else
                {
                    _Ins.gameObject.name = name;
                }
                _Ins.transform.parent = root.transform;
            }
            return _Ins;
        }
    }
}