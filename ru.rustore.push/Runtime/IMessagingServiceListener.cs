
using System.Collections.Generic;

namespace RuStore.PushClient {

    /// <summary>
    /// Интерфейс для получения и обработки данных push-уведомлений.
    /// </summary>
    public interface IMessagingServiceListener {

        /// <summary>
        /// Метод вызывается при получении нового push-токена.
        /// После вызова этого метода приложение становится ответственно за передачу нового push-токена на свой сервер.
        /// </summary>
        /// <param name="token">Значение нового токена.</param>
        public void OnNewToken(string token);

        /// <summary>
        /// Метод вызывается при получении нового push-уведомления.
        /// Если в объекте notification есть данные, SDK самостоятельно отображает уведомление.
        /// Если вы не хотите этого, используйте объект data, а notification оставьте пустым.
        /// Метод вызывается в любом случае.
        /// </summary>
        /// <param name="message">Информация о push-уведомлении.</param>
        public void OnMessageReceived(RemoteMessage message);

        /// <summary>
        /// Метод вызывается, если один или несколько push-уведомлений не доставлены на устройство.
        /// Например, если время жизни уведомления истекло до момента доставки.
        /// При вызове этого метода рекомендуется синхронизироваться со своим сервером, чтобы не пропустить данные.
        /// </summary>
        public void OnDeletedMessages();

        /// <summary>
        /// Метод вызывается, если в момент инициализации возникает ошибка.
        /// </summary>
        /// <param name="errors">Список объектов с ошибками.</param>
        public void OnError(List<RuStoreError> errors);
    }
}