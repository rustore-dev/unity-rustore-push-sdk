using System.Collections.Generic;

namespace RuStore.PushClient {

    /// <summary>
    /// Тестовое push-сообщение.
    /// </summary>
    public class TestNotificationPayload  {

        /// <summary>
        /// Заголовок сообщения.
        /// </summary>
        public string title;

        /// <summary>
        /// Тело сообщения.
        /// </summary>
        public string body;

        /// <summary>
        /// Ссылка на картинку.
        /// </summary>
        public string imgUrl;

        /// <summary>
        /// Коллекция ключ-значение.
        /// </summary>
        public Dictionary<string, string> data;
    }
}