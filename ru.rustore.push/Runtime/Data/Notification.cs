namespace RuStore.PushClient {

    /// <summary>
    /// Содержание push-уведомления.
    /// </summary>
    public class Notification {

        /// <summary>
        /// Заголовок уведомления.
        /// </summary>
        public string title;

        /// <summary>
        /// Тело уведомления.
        /// </summary>
        public string body;

        /// <summary>
        /// Возможность задать канал, в который отправится уведомление.
        /// Актуально для Android 8.0 и выше.
        /// </summary>
        public string channelId;

        /// <summary>
        /// Прямая ссылка на изображение для вставки в уведомление.
        /// Изображение должно быть не более 1 Мбайт.
        /// </summary>
        public string imageUrl;

        /// <summary>
        /// Цвет уведомления в HEX-формате, строкой.
        /// Например, #0077FF.
        /// </summary>
        public string color;

        /// <summary>
        /// Значок уведомления из res/drawable в формате строки, которая совпадает с названием ресурса.
        /// Например, в res/drawable есть значок small_icon.xml, который доступен в коде через R.drawable.small_icon.
        /// Чтобы значок отображался в уведомлении, сервер должен указать в параметре icon значение small_icon.
        /// </summary>
        public string icon;

        /// <summary>
        /// Intent action, с помощью которого будет открыта активити при клике на уведомление.
        /// </summary>
        public string clickAction;

        /// <summary>
        /// Тип поля clickAction.
        /// </summary>
        public ClickActionType? clickActionType;
    }
}
