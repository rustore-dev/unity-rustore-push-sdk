using UnityEngine;

namespace RuStore.PushClient {

    /// <summary>
    /// Параметры инициализации push-клиента.
    /// </summary>
    public class RuStorePushClientConfig {

        /// <summary>
        /// Обработка ошибок в нативном SDK.
        /// true — разрешает обработку ошибок, false — запрещает.
        /// </summary>
        public bool allowNativeErrorHandling;

        /// <summary>
        /// Объект, реализующий интерфейс для получения и обработки данных push-уведомлений.
        /// </summary>
        public IMessagingServiceListener messagingServiceListener;

        /// <summary>
        /// Объект, реализующий интерфейс для получения и обработки событий SDK push-уведомлений.
        /// </summary>
        public ILogListener logListener;
    }
}
