using RuStore.Internal;
using System;

namespace RuStore.PushClient.Internal {

    public class GetTokenListener : SimpleResponseListener<string> {

        private const string javaClassName = "ru.rustore.unitysdk.pushclient.callbacks.GetTokenListener";

        public GetTokenListener(Action<RuStoreError> onFailure, Action<string> onSuccess) : base(javaClassName, onFailure, onSuccess) {
        }
    }
}
