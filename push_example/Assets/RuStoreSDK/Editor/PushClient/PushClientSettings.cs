using UnityEditor;
using UnityEngine;

namespace RuStore.Editor {

    public class PushClientSettings : RuStoreModuleSettings {

        [Header("Push Client SDK")]
        public string VKPNSProjectId;
        public bool allowNativeErrorHandling;
        public bool testMode;

        [MenuItem("Window/RuStoreSDK/Settings/PushClient")]
        public static void EditPushClientSettings() {
            EditSettings<PushClientSettings>();
        }
    }
}