using UnityEngine;

namespace RuStore.PushClient {

    public class RuStorePushClientConfig {

        public bool allowNativeErrorHandling;
        public IMessagingServiceListener messagingServiceListener;
        public ILogListener logListener;
    }
}
