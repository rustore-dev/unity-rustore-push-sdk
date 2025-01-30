using UnityEngine;
using System;
using RuStore.Internal;
using RuStore.PushClient.Internal;

namespace RuStore.PushClient {

    public class RuStorePushClient {

        public static string PluginVersion = "6.7.0";

        private static RuStorePushClient _instance;
        private static bool _isInstanceInitialized;

        private bool _isInitialized;
        public bool IsInitialized => _isInitialized;
        private AndroidJavaObject _clientWrapper;

        private bool _allowNativeErrorHandling;

        public static RuStorePushClient Instance {
            get {
                if (!_isInstanceInitialized) {
                    _isInstanceInitialized = true;
                    _instance = new RuStorePushClient();
                }
                return _instance;
            }
        }

        public bool AllowNativeErrorHandling {
            get {
                return _allowNativeErrorHandling;
            }
            set {
                _allowNativeErrorHandling = value;

                if (_isInitialized) {
                    _clientWrapper.Call("setErrorHandling", value);
                }
            }
        }

        private RuStorePushClient() {
        }

        public bool Init(RuStorePushClientConfig config) {
            if (_isInitialized) {
                Debug.LogError("Error: RuStore Push Client is already initialized");
                return false;
            }

            if (Application.platform != RuntimePlatform.Android) {
                return false;
            }

            CallbackHandler.InitInstance();

            using (var pushClientClass = new AndroidJavaClass("ru.rustore.unitysdk.pushclient.RuStoreUnityPushClient")) {
                _clientWrapper = pushClientClass.GetStatic<AndroidJavaObject>("INSTANCE");
            }

            var serviceListener = new MessagingServiceListener(config.messagingServiceListener);
            var logListener = new LogListener(config.logListener);

            _clientWrapper.Call("init", config.allowNativeErrorHandling, serviceListener, logListener);
            _isInitialized = true;

            _allowNativeErrorHandling = _clientWrapper.Call<bool>("getErrorHandling");

            return true;
        }

        public void CheckPushAvailability(Action<RuStoreError> onFailure, Action<FeatureAvailabilityResult> onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new FeatureAvailabilityListener(onFailure, onSuccess);
            _clientWrapper.Call("checkPushAvailability", listener);

        }

        public void GetToken(Action<RuStoreError> onFailure, Action<string> onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new GetTokenListener(onFailure, onSuccess);
            _clientWrapper.Call("getToken", listener);
        }

        public void DeleteToken(Action<RuStoreError> onFailure, Action onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new DeleteTokenListener(onFailure, onSuccess);
            _clientWrapper.Call("deleteToken", listener);
        }

        public void SubscribeToTopic(string topicName, Action<RuStoreError> onFailure, Action onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new SubscribeTopicListener(onFailure, onSuccess);
            _clientWrapper.Call("subscribeToTopic", topicName, listener);
        }

        public void UnsubscribeFromTopic(string topicName, Action<RuStoreError> onFailure, Action onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new SubscribeTopicListener(onFailure, onSuccess);
            _clientWrapper.Call("unsubscribeFromTopic", topicName, listener);
        }

        public void SendTestNotification(TestNotificationPayload payload, Action<RuStoreError> onFailure, Action onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            using (var helper = new AndroidJavaObject("ru.rustore.unitysdk.pushclient.TestNotificationHelper")) {
                if (payload.data != null) {
                    foreach (var d in payload.data) {
                        if (!string.IsNullOrEmpty(d.Key) && !string.IsNullOrEmpty(d.Value)) {
                            helper.Call("addData", d.Key, d.Value);
                        }
                    }
                }
                var listener = new SendTestNotificationListener(onFailure, onSuccess);
                helper.Call("sendTestNotification", payload.title ?? "", payload.body ?? "", payload.imgUrl ?? "", listener);
            }
        }

        private bool IsPlatformSupported(Action<RuStoreError> onFailure) {
            if(Application.platform != RuntimePlatform.Android) {
                onFailure?.Invoke(new RuStoreError() {
                    name = "RuStorePushClientError",
                    description = "Unsupported platform"
                });
                return false;
            }

            return true;
        }
    }
}
