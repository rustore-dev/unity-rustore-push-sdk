using System.Collections.Generic;

namespace RuStore.PushClient {

    /// <summary>
    /// Параметры push-уведомления.
    /// </summary>
    public class RemoteMessage  {

        /// <summary>
        /// Идентификатор группы уведомлений (на данный момент не учитывается).
        /// </summary>
        public string collapseKey;

        /// <summary>
        /// Словарь, в который можно передать дополнительные данные для уведомления.
        /// </summary>
        public Dictionary<string, string> data;

        /// <summary>
        /// Уникальный ID сообщения.
        /// Является идентификатором каждого сообщения.
        /// </summary>
        public string messageId;

        /// <summary>
        /// Объект уведомления.
        /// </summary>
        public Notification notification;
        
        /// <summary>
        /// Возвращает значение приоритетности (на данный момент не учитывается).
        /// В настоящее время применяются варианты: 0 — UNKNOWN; 1 — HIGH; 2 — NORMAL.
        /// </summary>
        public int priority;

        /// <summary>
        /// Словарь data в виде массива байтов.
        /// </summary>
        public sbyte[] rawData;

        /// <summary>
        /// Время жизни push-уведомления типа int в секундах.
        /// </summary>
        public int ttl;

        /// <summary>
        /// Поле, по которому можно понять, откуда пришло уведомление:
        /// для уведомлений, отправленных в топик, в поле отображается имя топика;
        /// в других случаях — часть вашего сервисного токена.
        /// </summary>
        public string from;
    }
}
