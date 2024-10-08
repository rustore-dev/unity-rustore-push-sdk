using System.Collections.Generic;

namespace RuStore.PushClient {

    public class RemoteMessage  {

        public string collapseKey;
        public Dictionary<string, string> data;
        public string messageId;
        public Notification notification;
        public int priority;
        public sbyte[] rawData;
        public int ttl;
        public string from;
    }
}