package ru.rustore.unitysdk.pushclient.callbacks;

public interface SendTestNotificationListener {

    public void OnFailure(Throwable throwable);
    public void OnSuccess();
}
