using System;

namespace RuStore.PushClient {

    /// <summary>
    /// Интерфейс для получения и обработки событий SDK push-уведомлений.
    /// Обеспечивает методы для логирования различных уровней сообщений.
    /// </summary>
    public interface ILogListener {

        /// <summary>
        /// Объединяет сообщения событий LogInfo, LogDebug и LogVerbose.
        /// </summary>
        /// <param name="logString">Текст сообщения для логирования.</param>
        [Obsolete("This method is deprecated. Use the LogInfo, LogDebug and LogVerbose methods.")]
        void Log(string logString);

        /// <summary>
        /// Логирует информационное сообщение.
        /// Используется для записи стандартных логов, которые помогают отслеживать
        /// нормальное выполнение программы.
        /// </summary>
        /// <param name="logString">Текст сообщения для логирования.</param>
        void LogInfo(string logString);

        /// <summary>
        /// Логирует предупреждающее сообщение.
        /// Используется для записи логов, которые сигнализируют о потенциальных проблемах,
        /// которые не мешают выполнению программы, но могут потребовать внимания.
        /// </summary>
        /// <param name="logString">Текст предупреждающего сообщения для логирования.</param>
        void LogWarning(string logString);

        /// <summary>
        /// Логирует сообщение об ошибке.
        /// Используется для записи логов, которые сигнализируют о возникновении ошибок,
        /// требующих вмешательства или исправления.
        /// </summary>
        /// <param name="logString">Текст сообщения об ошибке для логирования.</param>
        void LogError(string logString);

        /// <summary>
        /// Логирует отладочное сообщение.
        /// Используется для записи детальных логов,
        /// которые фиксируют диагностическую информацию, полезную для разработчиков.
        /// </summary>
        /// <param name="logString">Текст отладочного сообщения для логирования.</param>
        void LogDebug(string logString);

        /// <summary>
        /// Логирует подробное сообщение.
        /// Используется для записи максимально детальных логов, которые включают
        /// максимум информации о работе программы и её внутреннем состоянии.
        /// </summary>
        /// <param name="logString">Текст подробного сообщения для логирования.</param>
        void LogVerbose(string logString);
    }
}
