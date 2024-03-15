
using System.Collections.Generic;

namespace RuStore.PushClient {

    public interface IMessagingServiceListener {

        public void OnNewToken(string token);
        public void OnMessageReceived(RemoteMessage message);
        public void OnDeletedMessages();
        public void OnError(List<RuStoreError> errors);
    }
}