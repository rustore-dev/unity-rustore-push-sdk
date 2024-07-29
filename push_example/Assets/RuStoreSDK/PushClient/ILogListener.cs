
namespace RuStore.PushClient {

    public interface ILogListener {

        public void Log(string logString);
        public void LogWarning(string logString);
        public void LogError(string logString);
    }
}