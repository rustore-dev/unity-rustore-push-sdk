using RuStore.Internal;
using System;
using UnityEngine;

namespace RuStore.PushClient.Internal {

    public class DeleteTokenListener : SimpleResponseListener {

        private const string javaClassName = "ru.rustore.unitysdk.pushclient.callbacks.DeleteTokenListener";

        public DeleteTokenListener(Action<RuStoreError> onFailure, Action onSuccess) : base(javaClassName, onFailure, onSuccess) {
        }
    }
}
