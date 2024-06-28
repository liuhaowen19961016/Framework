using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 引用池管理器
    /// </summary>
    public class ReferencePool
    {
        public const int DefaultCapacity = 50;

        private static Dictionary<Type, ReferenceCollection> referencelCollections = new Dictionary<Type, ReferenceCollection>();

        public static void PreLoad<T>(int count, int capacity = DefaultCapacity)
            where T : class
        {
            var pool = GetReferenceCollection(typeof(T));
            pool.SetCapacity(capacity);
            pool.Add(count);
        }

        public static object Allocate(Type type)
        {
            var pool = GetReferenceCollection(type);
            return pool.Allocate(type);
        }

        public static T Allocate<T>()
            where T : class
        {
            var obj = Allocate(typeof(T));
            return obj as T;
        }

        public static bool Recycle(object obj)
        {
            var pool = GetReferenceCollection(obj.GetType());
            return pool.Recycle(obj);
        }

        public static bool Recycle<T>(T obj)
            where T : class
        {
            var pool = GetReferenceCollection(typeof(T));
            return pool.Recycle<T>(obj);
        }

        public static bool Add<T>(int count)
            where T : class
        {
            var pool = GetReferenceCollection(typeof(T));
            return pool.Add(count);
        }

        public static void Remove<T>(int count)
            where T : class
        {
            var pool = GetReferenceCollection(typeof(T));
            pool.Remove(count);
        }

        public static void Dispose<T>()
        {
            var pool = GetReferenceCollection(typeof(T));
            pool.Dispose();
        }

        public static void Dispose(Type type)
        {
            var pool = GetReferenceCollection(type);
            pool.Dispose();
        }

        public static void DisposeAll()
        {
            foreach (var referencelCollection in referencelCollections.Values)
            {
                referencelCollection.Dispose();
            }
            referencelCollections.Clear();
        }

        public static ReferenceCollection GetReferenceCollection(Type referenceType)
        {
            if (referenceType == null)
            {
                Debug.LogError($"类型错误，不能为null");
                return null;
            }

            if (!referencelCollections.TryGetValue(referenceType, out ReferenceCollection referenceCollection))
            {
                referenceCollection = new ReferenceCollection(referenceType);
                referencelCollections.Add(referenceType, referenceCollection);
            }
            return referenceCollection;
        }
    }
}