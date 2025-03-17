package ru.rustore.unitysdk.pushclient.callbacks;

public interface UnityLogListener {

    @Deprecated
    public void Log(String logString);
    public void LogInfo(String logString);
    public void LogWarning(String logString);
    public void LogError(String logString);
    public void LogException(Throwable throwable);
    public void LogDebug(String logString);
    public void LogVerbose(String logString);
}
