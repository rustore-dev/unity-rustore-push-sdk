using UnityEngine;
using UnityEngine.UI;
using RuStore.PushClient;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Android;

namespace RuStore.PushExample.UI {

    public class PushesScreen : MonoBehaviour, IMessagingServiceListener, ILogListener {

        private const string CopyMessage = "Copied";

        private const string NotificationsPermission = "android.permission.POST_NOTIFICATIONS";

        [SerializeField]
        private bool _allowNativeErrorHandling;

        [SerializeField]
        private Text _log;

        [SerializeField]
        private ScrollRect _logScrollRect;

        [SerializeField]
        private MessageBox _messageBox;

        [SerializeField]
        private LoadingIndicator _loadingIndicator;

        [SerializeField]
        private InputField _inputFieldToken;

        private void Awake() {
            var pushConfig = new RuStorePushClientConfig() {
                allowNativeErrorHandling = _allowNativeErrorHandling,
                messagingServiceListener = this,
                logListener = this
            };

            RuStorePushClient.Instance.Init(pushConfig);

            //SendTestNotification();

            CheckNotificationsPermission();
        }

        private void SendTestNotification() {
            var testPayload = new TestNotificationPayload() {
                title = "Test",
                body = "Test push notification",
                data = new Dictionary<string, string>() {
                    { "key1", "value1" },
                    { "key2", "value2" },
                    { "key3", "value3" }
                }
            };

            RuStorePushClient.Instance.SendTestNotification(
                payload: testPayload,
                onFailure: OnError, 
                onSuccess: () => { Debug.Log("Send test notification success"); 
            });
        }

        private void CheckNotificationsPermission() {
            if (!Permission.HasUserAuthorizedPermission(NotificationsPermission)) {
                Permission.RequestUserPermission(NotificationsPermission);
            }
        }

        public void CopyToken() {
            GUIUtility.systemCopyBuffer = _inputFieldToken.text;
            ShowToast(CopyMessage);
        }

        public void ShowToast(string message) {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaObject utils = new AndroidJavaObject("com.plugins.pushexample.RuStorePushAndroidUtils")) {
                utils.Call("showToast", currentActivity, message);
            }
        }

        public void GetToken() {
            RuStorePushClient.Instance.GetToken(
                onFailure: OnError,
                onSuccess: (response) => {
                    _inputFieldToken.text = response;

                    var logString = string.Format("[I] Token received: {0}", response);
                    Debug.Log(logString);
                    AddLog(logString);
                });
        }

        public void RemoveToken() {
            RuStorePushClient.Instance.DeleteToken(
                onFailure: OnError,
                onSuccess: () => {
                    _inputFieldToken.text = string.Empty;

                    var logString = "[I] Delete token success";
                    Debug.Log(logString);
                    AddLog(logString);
                });
        }

        public void SubscribeToTopic() {
            var topicName = "Test";
            RuStorePushClient.Instance.SubscribeToTopic(
                topicName: topicName,
                onFailure: OnError,
                onSuccess: () => {
                    var logString = string.Format("[I] Subscribed to topic \"{0}\"", topicName);
                    Debug.Log(logString);
                    AddLog(logString);
                });
        }
        
        public void UnsubscribeFromTopic() {
            var topicName = "Test";
            RuStorePushClient.Instance.UnsubscribeFromTopic(
                topicName: topicName,
                onFailure: OnError,
                onSuccess: () => {
                    var logString = string.Format("[I] Unsubscribed from topic \"{0}\"", topicName);
                    Debug.Log(logString);
                    AddLog(logString);
                });
        }

        public void ClearLogs() {
            _log.text = "";
        }

        private void AddLog(string s) {
            var logText = _log.text;
            logText += "\n" + s;
            _log.text = logText;

            Canvas.ForceUpdateCanvases();
            _logScrollRect.verticalNormalizedPosition = 0f;
        }

        void IMessagingServiceListener.OnDeletedMessages() {
            var logString = "[I] Messages deleted";

            Debug.Log(logString);
            AddLog(logString);
        }

        void IMessagingServiceListener.OnError(List<RuStoreError> errors) {
            foreach(var e in errors) {
                var errorString = JsonConvert.SerializeObject(e);

                Debug.Log(errorString);
                AddLog("[E] " + errorString);
            }
        }

        void IMessagingServiceListener.OnMessageReceived(RemoteMessage message) {
            var logString = string.Format("[I] Message received: {0}", JsonConvert.SerializeObject(message));

            Debug.Log(logString);
            AddLog(logString);
        }

        void IMessagingServiceListener.OnNewToken(string token) {
            var logString = string.Format("[I] New token received: {0}", token);

            Debug.Log(logString);
            AddLog(logString);
        }

        void ILogListener.Log(string logString) {
            AddLog(logString);
        }

        void ILogListener.LogWarning(string logString) {
            AddLog(logString);
        }

        void ILogListener.LogError(string logString) {
            AddLog(logString);
        }

        private void OnError(RuStoreError error) {
            HideLoadingIndicator();

            Debug.LogErrorFormat("{0}: {1}", error.name, error.description);
            AddLog(string.Format("[E] {0}: {1}", error.name, error.description));

            if (!RuStorePushClient.Instance.AllowNativeErrorHandling || error.name.IndexOf("RuStore") == -1) {
                ShowMessage("Error: " + error.name, error.description);
            }
        }

        protected void ShowLoadingIndicator() {
            _loadingIndicator.Show();
        }
        protected void HideLoadingIndicator() {
            _loadingIndicator.Hide();
        }

        private void ShowMessage(string title, string message, System.Action onClose = null) {
            _messageBox.Show(
                title: title,
                message: message,
                onClose: onClose);
        }

    }
}
