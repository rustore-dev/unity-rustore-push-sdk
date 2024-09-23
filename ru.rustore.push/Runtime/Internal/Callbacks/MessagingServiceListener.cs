using System;
using System.Collections.Generic;
using UnityEngine;

namespace RuStore.PushClient.Internal {

    public class MessagingServiceListener : AndroidJavaProxy {

        private const string javaClassName = "ru.rustore.unitysdk.pushclient.callbacks.RuStoreUnityMessagingServiceListener";

        private IMessagingServiceListener _serviceListener;

        public MessagingServiceListener(IMessagingServiceListener serviceListener) : base(javaClassName) {
            _serviceListener = serviceListener;
        }

        public void OnNewToken(string token) {
            CallbackHandler.AddCallback(() => {
                _serviceListener.OnNewToken(token);
            });
        }

        public void OnMessageReceived(AndroidJavaObject messageObject) {
            var message = new RemoteMessage() {
                collapseKey = messageObject.Get<string>("collapseKey"),
                messageId = messageObject.Get<string>("messageId"),
                ttl = messageObject.Get<int>("ttl"),
                rawData = messageObject.Get<sbyte[]>("rawData"),
                priority = messageObject.Get<int>("priority"),
            };

            using (var data = messageObject.Get<AndroidJavaObject>("data")) {
                if (data != null) {
                    message.data = new Dictionary<string, string>();
                    using (var entrySet = data.Call<AndroidJavaObject>("entrySet")) {
                        using (var setIterator = entrySet.Call<AndroidJavaObject>("iterator")) {
                            var hasNext = setIterator.Call<bool>("hasNext");
                            while (hasNext) {
                                using (var entry = setIterator.Call<AndroidJavaObject>("next")) {
                                    var key = entry.Call<string>("getKey");
                                    var value = entry.Call<string>("getValue");
                                    message.data.Add(key, value);
                                }
                                hasNext = setIterator.Call<bool>("hasNext");
                            }
                        }
                    }
                }
            }

            using (var notificationObject = messageObject.Get<AndroidJavaObject>("notification")) {
                var jnotificationObject = notificationObject.Get<AndroidJavaObject>("clickActionType");

                if (notificationObject != null) {
                    message.notification = new Notification() {
                        title = notificationObject.Get<string>("title"),
                        body = notificationObject.Get<string>("body"),
                        channelId = notificationObject.Get<string>("channelId"),
                        imageUrl = notificationObject.Get<AndroidJavaObject>("imageUrl")?.Call<string>("toString"),
                        color = notificationObject.Get<string>("color"),
                        icon = notificationObject.Get<string>("icon"),
                        clickAction = notificationObject.Get<string>("clickAction"),
                        clickActionType = jnotificationObject != null ? (ClickActionType)Enum.Parse(typeof(ClickActionType), jnotificationObject.Call<string>("toString"), true) : null
                    };
                }
            }

            CallbackHandler.AddCallback(() => {
                _serviceListener.OnMessageReceived(message);
            });
        }

        public void OnDeletedMessages() {
            CallbackHandler.AddCallback(() => {
                _serviceListener.OnDeletedMessages();
            });
        }

        public void OnError(AndroidJavaObject errorsObject) {
            var errors = new List<RuStoreError>();
            var size = errorsObject.Call<int>("size");

            for (var i = 0; i < size; i++) {
                using (var e = errorsObject.Call<AndroidJavaObject>("get", i)) {
                    if (e != null) {
                        using (var errorJavaClass = e.Call<AndroidJavaObject>("getClass")) {
                            var error = new RuStoreError() {
                                name = errorJavaClass.Call<string>("getSimpleName"),
                                description = e.Call<string>("getMessage")
                            };
                            errors.Add(error);
                        }
                    }
                }
            }

            CallbackHandler.AddCallback(() => {
                _serviceListener.OnError(errors);
            });
        }
    }
}
