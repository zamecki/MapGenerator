using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace LMZMapGenerator.Core
{
    public class ThreadDataRequest : MonoBehaviour
    {
        static ThreadDataRequest instance;
        Queue<ThreadInfo> ThreadInfoQueue = new Queue<ThreadInfo>();


        void Awake()
        {
            instance = FindObjectOfType<ThreadDataRequest>();
        }
        public static void RequestData(Func<object> generateData, Action<object> callback)
        {
            ThreadStart threadStart = delegate
            {
                instance.DataThread(generateData, callback);
            };
            new Thread(threadStart).Start();
        }
        void DataThread(Func<object> generateData, Action<object> callback)
        {
            object data = generateData();
            lock (ThreadInfoQueue)
            {
                ThreadInfoQueue.Enqueue(new ThreadInfo(callback, data));
            }
        }

        void Update()
        {
            if (ThreadInfoQueue.Count > 0)
            {
                for (int i = 0; i < ThreadInfoQueue.Count; i++)
                {
                    ThreadInfo mapThreadInfo = ThreadInfoQueue.Dequeue();
                    mapThreadInfo.callback(mapThreadInfo.parameter);
                }
            }
        }

        struct ThreadInfo
        {
            public readonly Action<object> callback;
            public readonly object parameter;

            public ThreadInfo(Action<object> callback, object parameter)
            {
                this.callback = callback;
                this.parameter = parameter;
            }
        }
    }
}
