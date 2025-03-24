using System;
using UnityEngine;

namespace RuStore.PushClient.Internal {

    public class LogListener : AndroidJavaProxy {

        private const string javaClassName = "ru.rustore.unitysdk.pushclient.callbacks.UnityLogListener";
        private ILogListener _listener;

        public LogListener(ILogListener listener) : base(javaClassName) {
            _listener = listener;
        }

        [Obsolete("This method is deprecated. Use the LogInfo, LogDebug and LogVerbose methods.")]
        void Log(string logString) {
            CallbackHandler.AddCallback(() => {
                _listener?.Log(logString);
            });
        }

        void LogInfo(string logString) {
            CallbackHandler.AddCallback(() => {
                _listener?.LogInfo(logString);
            });
        }

        void LogWarning(string logString) {
            CallbackHandler.AddCallback(() => {
                _listener?.LogWarning(logString);
            });
        }

        void LogError(string logString) {
            CallbackHandler.AddCallback(() => {
                _listener?.LogError(logString);
            });
        }

        void LogDebug(string logString) {
            CallbackHandler.AddCallback(() => {
                _listener?.LogDebug(logString);
            });
        }

        void LogVerbose(string logString) {
            CallbackHandler.AddCallback(() => {
                _listener?.LogVerbose(logString);
            });
        }

        void LogException(AndroidJavaObject exception) {
            using (var errorJavaClass = exception.Call<AndroidJavaObject>("getClass")) {
                var name = errorJavaClass.Call<string>("getSimpleName");
                var description = exception.Call<string>("getMessage");
                var logString = string.Format("[E] {0}: {1}", name, description);

                CallbackHandler.AddCallback(() => {
                    _listener?.LogError(logString);
                });
            }
        }
    }
}