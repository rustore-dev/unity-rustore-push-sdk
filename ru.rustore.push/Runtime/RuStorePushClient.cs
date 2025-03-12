using UnityEngine;
using System;
using RuStore.Internal;
using RuStore.PushClient.Internal;

namespace RuStore.PushClient {

    /// <summary>
    /// Класс реализует API для приёма push-сообщений через сервисы RuStore.
    /// </summary>
    public class RuStorePushClient {

        /// <summary>
        /// Версия плагина.
        /// </summary>
        public static string PluginVersion = "6.9.1";

        private static RuStorePushClient _instance;
        private static bool _isInstanceInitialized;

        private bool _isInitialized;

        /// <summary>
        /// Возвращает true, если синглтон был инициализирован, в противном случае — false.
        /// </summary>
        public bool IsInitialized => _isInitialized;
        private AndroidJavaObject _clientWrapper;

        private bool _allowNativeErrorHandling;

        /// <summary>
        /// Возвращает единственный экземпляр RuStorePushClient (реализация паттерна Singleton).
        /// Если экземпляр еще не создан, создает его.
        /// </summary>
        public static RuStorePushClient Instance {
            get {
                if (!_isInstanceInitialized) {
                    _isInstanceInitialized = true;
                    _instance = new RuStorePushClient();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Обработка ошибок в нативном SDK.
        /// true — разрешает обработку ошибок, false — запрещает.
        /// </summary>
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

        /// <summary>
        /// Выполняет инициализацию синглтона RuStorePushClient.
        /// </summary>
        /// <param name="config">
        /// Объект класса RuStore.PushClient.RuStorePushClientConfig.
        /// Содержит параметры инициализации push-клиента.
        /// </param>
        /// <returns>Возвращает true, если инициализация была успешно выполнена, в противном случае — false.</returns>
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

        /// <summary>
        /// Проверка доступности приёма push-сообщений.
        /// Если все условия выполняются, возвращается RuStore.FeatureAvailabilityResult.isAvailable == true.
        /// В противном случае возвращается RuStore.FeatureAvailabilityResult.isAvailable == false.
        /// </summary>
        /// <param name="onFailure">
        /// Действие, выполняемое в случае ошибки.
        /// Возвращает объект RuStore.RuStoreError с информацией об ошибке.
        /// </param>
        /// <param name="onSuccess">
        /// Действие, выполняемое при успешном завершении операции.
        /// Возвращает объект RuStore.FeatureAvailabilityResult с информацией о доступности приёма push-сообщений.
        /// </param>
        public void CheckPushAvailability(Action<RuStoreError> onFailure, Action<FeatureAvailabilityResult> onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new FeatureAvailabilityListener(onFailure, onSuccess);
            _clientWrapper.Call("checkPushAvailability", listener);

        }

        /// <summary>
        /// Получить текущий push-токен пользователя.
        /// </summary>
        /// <param name="onFailure">
        /// Действие, выполняемое в случае ошибки.
        /// Возвращает объект RuStore.RuStoreError с информацией об ошибке.
        /// </param>
        /// <param name="onSuccess">
        /// Действие, выполняемое при успешном завершении операции.
        /// Возвращает push-токен в виде строки.
        /// </param>
        public void GetToken(Action<RuStoreError> onFailure, Action<string> onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new GetTokenListener(onFailure, onSuccess);
            _clientWrapper.Call("getToken", listener);
        }

        /// <summary>
        /// Удалить текущий push-токен пользователя.
        /// </summary>
        /// <param name="onFailure">
        /// Действие, выполняемое в случае ошибки.
        /// Возвращает объект RuStore.RuStoreError с информацией об ошибке.
        /// </param>
        /// <param name="onSuccess">
        /// Действие, выполняемое при успешном завершении операции.
        /// </param>
        public void DeleteToken(Action<RuStoreError> onFailure, Action onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new DeleteTokenListener(onFailure, onSuccess);
            _clientWrapper.Call("deleteToken", listener);
        }

        /// <summary>
        /// Подписка на push-уведомления по топику.
        /// </summary>
        /// <param name="topicName">Название топика.</param>
        /// <param name="onFailure">
        /// Действие, выполняемое в случае ошибки.
        /// Возвращает объект RuStore.RuStoreError с информацией об ошибке.
        /// </param>
        /// <param name="onSuccess">
        /// Действие, выполняемое при успешном завершении операции.
        /// </param>
        public void SubscribeToTopic(string topicName, Action<RuStoreError> onFailure, Action onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new SubscribeTopicListener(onFailure, onSuccess);
            _clientWrapper.Call("subscribeToTopic", topicName, listener);
        }

        /// <summary>
        /// Отписка от топика.
        /// </summary>
        /// <param name="topicName">Название топика.</param>
        /// <param name="onFailure">
        /// Действие, выполняемое в случае ошибки.
        /// Возвращает объект RuStore.RuStoreError с информацией об ошибке.
        /// </param>
        /// <param name="onSuccess">
        /// Действие, выполняемое при успешном завершении операции.
        /// </param>
        public void UnsubscribeFromTopic(string topicName, Action<RuStoreError> onFailure, Action onSuccess) {
            if (!IsPlatformSupported(onFailure)) {
                return;
            }

            var listener = new SubscribeTopicListener(onFailure, onSuccess);
            _clientWrapper.Call("unsubscribeFromTopic", topicName, listener);
        }

        /// <summary>
        /// Доставка тестовых push-уведомлений.
        /// Тестовый режим (параметр testModeEnabled = true) должен быть активирован в момент инициализации в Application,
        /// либо в наследнике AbstractRuStorePushClientParams (при автоматической инициализации).
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="onFailure">
        /// Действие, выполняемое в случае ошибки.
        /// Возвращает объект RuStore.RuStoreError с информацией об ошибке.
        /// </param>
        /// <param name="onSuccess">
        /// Действие, выполняемое при успешном завершении операции.
        /// </param>
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
