using RuStore.Internal;
using System;
using UnityEngine;

namespace RuStore.PushClient.Internal {

    public class SendTestNotificationListener : SimpleResponseListener {

        private const string javaClassName = "ru.rustore.unitysdk.pushclient.callbacks.SendTestNotificationListener";

        public SendTestNotificationListener(Action<RuStoreError> onFailure, Action onSuccess) : base(javaClassName, onFailure, onSuccess) {
        }
    }
}
