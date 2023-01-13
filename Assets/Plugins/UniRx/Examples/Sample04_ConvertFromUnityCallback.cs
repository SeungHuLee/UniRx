using System;
using UnityEngine;

namespace UniRx.Examples
{
    public class Sample04_ConvertFromUnityCallback : MonoBehaviour
    {
        private class LogCallback
        {
            public string Condition;
            public string StackTrace;
            public UnityEngine.LogType LogType;
        }

        static class LogHelper
        {
            // If static register callback, use Subject for event branching.
            // If standard events, you can use Observable.FromEvent.

            public static IObservable<LogCallback> LogCallbackAsObservable()
            {
                return Observable.FromEvent<Application.LogCallback, LogCallback>(
                    h => (condition, stackTrace, type) => h(new LogCallback { Condition = condition, StackTrace = stackTrace, LogType = type }),
                    h => Application.logMessageReceived += h, h => Application.logMessageReceived -= h);
            }
        }

        private void Awake()
        {
            // method is separatable and composable
            LogHelper.LogCallbackAsObservable()
                .Where(x => x.LogType == LogType.Warning)
                .Subscribe(x => Debug.Log(x));

            LogHelper.LogCallbackAsObservable()
                .Where(x => x.LogType == LogType.Error)
                .Subscribe(x => Debug.Log(x));
        }
    }
}