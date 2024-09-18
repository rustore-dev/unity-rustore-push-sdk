using RuStore.Internal;
using System;
using UnityEngine;

namespace RuStore.PushClient.Internal {

    public class SubscribeTopicListener : SimpleResponseListener {

        private const string javaClassName = "ru.rustore.unitysdk.pushclient.callbacks.SubscribeTopicListener";

        public SubscribeTopicListener(Action<RuStoreError> onFailure, Action onSuccess) : base(javaClassName, onFailure, onSuccess) {
        }
    }
}
